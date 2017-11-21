using Symbioz.Core;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Fights.Marks;
using Symbioz.World.Models.Fights.Results;
using Symbioz.World.Models.Fights.Spells;
using Symbioz.World.Models.Items;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Providers;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Providers.Fights.Effects;
using Symbioz.World.Providers.Fights.Effects.Others;
using Symbioz.World.Providers.Fights.Spells;
using Symbioz.World.Providers.Maps.Path;
using Symbioz.World.Records.Items;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Fighters
{
    public abstract class Fighter
    {
        public const short CARRY_STATE_ID = 3;

        public const short CARRIED_STATE_ID = 8;

        public int Id
        {
            get;
            set;
        }

        public Fight Fight
        {
            get;
            set;
        }
        public FightTeam Team
        {
            get;
            set;
        }

        public ContextActorLook Look
        {
            get;
            set;
        }
        public ContextActorLook RealLook
        {
            get;
            private set;
        }

        public short m_cellId;

        public short CellId
        {
            get
            {
                return m_cellId;
            }
            set
            {
                m_cellId = value;
            }
        }



        public short TurnStartCell
        {
            get;
            set;
        }
        public short FightStartCell
        {
            get;
            protected set;
        }

        public DateTime? TurnStartTime
        {
            get;
            set;
        }
        public DateTime? DeathTime
        {
            get;
            set;
        }

        public MapPoint Point
        {
            get
            {
                return new MapPoint((short)CellId);
            }
        }

        public ushort MapCellId
        {
            get;
            set;
        }

        public DirectionsEnum Direction
        {
            get;
            set;
        }
        public bool IsSummon
        {
            get
            {
                return Stats.Summoned;
            }
        }
        // Le combatant peut t-il être ciblé dans la timeline (CastOnTarget)
        public bool CanBeTargeted(Character source)
        {
            return this.GetInvisibilityStateFor(source) != GameActionFightInvisibilityStateEnum.INVISIBLE;
        }

        public abstract bool Sex
        {
            get;
        }
        public Abilities Abilities
        {
            get;
            private set;
        }

        protected List<SpellStateRecord> States
        {
            get;
            set;
        }
        public Fighter Carried
        {
            get;
            set;
        }
        public Fighter Carrying
        {
            get;
            set;
        }
        public bool CarryFighter
        {
            get
            {
                return Carried != null;
            }
        }
        public bool IsCarried
        {
            get
            {
                return Carrying != null;
            }
        }
        public bool CantSwitchPosition
        {
            get
            {
                return this.HasState(x => x.CantSwitchPosition);
            }
        }
        public bool IsInvulnerable
        {
            get
            {
                return HasState(x => x.Invulnerable);
            }
        }
        public bool InvulnerableMelee
        {
            get
            {
                return HasState(x => x.InvulnerableMelee);
            }
        }
        public bool InvunlerableRange
        {
            get
            {
                return HasState(x => x.InvulnerableRange);
            }
        }
        public bool Incurable
        {
            get
            {
                return HasState(x => x.Incurable);
            }
        }
        // *** Action<Fighter,...> Fighter is alway this *** //

        public event Action<Fighter> OnTurnStartEvt;

        public event Action<Fighter> OnTurnEndEvt;

        public event Action<Fighter> BeforeDeadEvt;

        public event Action<Fighter, bool> AfterDeadEvt;

        public event Action<Fighter, Damage> BeforeTakeDamagesEvt;

        public event Action<Fighter, Damage> OnAttemptToInflictEvt;

        public event Action<Fighter, Damage> OnDamageTaken;

        public delegate void TargetSourceDelegate(Fighter target, Fighter source);

        public delegate void OnSlideDelegate(Fighter target, Fighter source, short startCellId, short endCellId);

        public delegate void OnPushDelegate(Fighter target, Fighter source, short delta, sbyte cellsCount, bool headOn);
        /// <summary>
        /// Fighter Source, short delta
        /// </summary>
        public event OnPushDelegate OnPushDamages;

        public event OnSlideDelegate BeforeSlideEvt;

        public event OnSlideDelegate AfterSlideEvt;

        public event TargetSourceDelegate OnTeleportEvt;

        public event TargetSourceDelegate OnHealEvt;

        /// <summary>
        /// Action<short>amount</short>
        /// </summary>
        public event Action<Fighter, short> OnMpUsedEvt;

        public event Action<Fighter, short> OnApUsedEvt;

        public bool Left = false;

        public bool Alive = true;

        public bool IsReady = false;

        public abstract string Name
        {
            get;
        }
        public abstract ushort Level
        {
            get;
        }
        public FighterStats Stats
        {
            get;
            set;
        }
        public bool IsTeamLeader
        {
            get
            {
                return Team.Leader == this;
            }
        }
        public Loot Loot
        {
            get;
            set;
        }
        public bool IsFighterTurn
        {
            get
            {
                return this.Fight.TimeLine.Current == this;
            }
        }
        public void AddLife(int amount, bool showFighter = true)
        {
            Stats.MaxLifePoints += amount;
            Stats.LifePoints += amount;
            Stats.CurrentLifePoints = Stats.LifePoints;
            Stats.CurrentMaxLifePoints = Stats.MaxLifePoints;

            if (showFighter)
                ShowFighter();
        }

        public void SetLife(int amount, bool showFighter = true)
        {
            Stats.MaxLifePoints = amount;
            Stats.LifePoints = amount;
            Stats.CurrentLifePoints = Stats.LifePoints;
            Stats.CurrentMaxLifePoints = Stats.MaxLifePoints;

            if (showFighter)
                ShowFighter();
        }
        public void SubLife(int amount, bool showFighter = true)
        {
            AddLife(-amount, showFighter);
        }


        public virtual void OnFightStarted()
        {
            this.FightStartCell = this.CellId;
        }
        public UniqueIdProvider BuffIdProvider
        {
            get;
            private set;
        }
        public SpellHistory SpellHistory
        {
            get;
            private set;
        }


        public List<Buff> Buffs
        {
            get;
            set;
        }
        public int SummonCount
        {
            get
            {
                return Team.GetFighters<IOwnable>(true).FindAll(x => x.IsOwner(this)).Count;
            }
        }
        public bool CanSummon
        {
            get
            {
                return Stats.SummonableCreaturesBoost.TotalInContext() > SummonCount;
            }
        }

        private Dictionary<ushort, short> BuffedSpells
        {
            get;
            set;
        }
        public bool BlockSight
        {
            get
            {
                return Alive && Stats.InvisibilityState != GameActionFightInvisibilityStateEnum.INVISIBLE;
            }
        }

        public bool AddBuff(Buff buff, bool freeIdIfFail = true)
        {
            bool result;
            if (this.BuffMaxStackReached(buff))
            {
                if (freeIdIfFail)
                {
                    this.FreeBuffId(buff.Id);
                }
                result = false;
            }
            else
            {
                this.Buffs.Add(buff);
                Fight.Send(new GameActionFightDispellableEffectMessage((ushort)buff.GetActionId(), (uint)buff.Caster.Id, buff.GetAbstractFightDispellableEffect()));
                result = true;
            }
            return result;
        }
        public bool AddAndApplyBuff(Buff buff, bool freeIdIfFail = true)
        {
            bool result;
            if (this.BuffMaxStackReached(buff))
            {
                if (freeIdIfFail)
                {
                    this.FreeBuffId(buff.Id);
                }
                result = false;
            }
            else
            {
                this.AddBuff(buff, true);
                if (!(buff is TriggerBuff) || ((buff as TriggerBuff).Trigger & TriggerType.ON_CAST) == TriggerType.ON_CAST)
                {
                    buff.Apply();
                }
                result = true;
            }
            return result;
        }
        public void FreeBuffId(int id)
        {
            this.BuffIdProvider.Push(id);
        }
        public bool BuffMaxStackReached(Buff buff)
        {
            return buff.Level.MaxStacks > 0 && buff.Level.MaxStacks <= this.Buffs.Count((Buff entry) => entry.SpellId == buff.SpellId &&
                entry.Effect.EffectId == buff.Effect.EffectId && entry.Effect.Delay == buff.Effect.Delay
                && entry.IsTrigger() == buff.IsTrigger());
        }
        public T[] GetBuffs<T>(Predicate<T> predicate) where T : Buff
        {
            return Array.FindAll(Buffs.OfType<T>().ToArray(), predicate);
        }
        public T[] GetBuffs<T>()
        {
            return Buffs.OfType<T>().ToArray();
        }
        public T GetBuff<T>(Func<T, bool> predicate) where T : Buff
        {
            return Buffs.OfType<T>().FirstOrDefault(predicate);
        }
        public bool HasBuff(EffectsEnum effect)
        {
            return Buffs.Exists(x => x.Effect.EffectEnum == effect);
        }
        public Fighter(FightTeam team, ushort mapCellId)
        {
            this.Team = team;
            this.MapCellId = mapCellId;
            this.Loot = new Loot();
            this.BuffIdProvider = new UniqueIdProvider();
            this.States = new List<SpellStateRecord>();
            this.BuffedSpells = new Dictionary<ushort, short>();
            this.Buffs = new List<Buff>();
            this.SpellHistory = new SpellHistory(this);
            this.Abilities = new Abilities(this);
        }


        public void ShowFighter()
        {
            Fight.Send(new GameFightShowFighterMessage(GetFightFighterInformations()));
        }
        public void ShowFighter(CharacterFighter fighter)
        {
            fighter.Character.Client.Send(new GameFightShowFighterMessage(GetFightFighterInformations()));
        }
        /// <summary>
        /// Request?
        /// </summary>
        /// <param name="fighter"></param>
        internal void PlacementSwap(Fighter fighter)
        {
            if (!(this is CharacterFighter))
            {
                short cellId = fighter.CellId;

                fighter.ChangePlacementPosition(this.CellId, false);
                this.ChangePlacementPosition(cellId, false);
                this.Fight.UpdateFightersPlacementDirection();
                this.Fight.UpdateEntitiesPositions();
            }
        }
        public short NearFreeCell()
        {
            var points = Point.GetNearPoints();

            if (points.Count() > 0)
            {
                var pt = points.FirstOrDefault(x => Fight.IsCellFree(x.CellId));

                if (pt != null)
                {
                    return pt.CellId;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }
        public T[] GetNearFighters<T>() where T : Fighter
        {
            List<Fighter> fighters = new List<Fighter>();
            var points = Point.GetNearPoints();

            foreach (var point in points)
            {
                var target = Fight.GetFighter(point);

                if (target != null)
                    fighters.Add(target);
            }
            return fighters.OfType<T>().ToArray();
        }
        public void ChangePlacementPosition(short cellId, bool update = true)
        {
            if (!Fight.Started)
            {
                lock (Fight)
                {
                    this.CellId = cellId;

                    if (update)
                    {
                        this.Fight.UpdateFightersPlacementDirection();
                        this.Fight.UpdateEntitiesPositions();
                    }

                }
            }
        }
        public virtual void OnJoined()
        {
            this.ShowFighter();
            this.Fight.UpdateFightersPlacementDirection();
            this.Fight.UpdateEntitiesPositions();
            Fight.UpdateTeams();
        }
        public virtual void Kick()
        {

        }
        public virtual void Initialize()
        {
            this.CellId = Team.GetPlacementCell();
            this.TurnStartCell = this.CellId;
            this.Direction = Team.FindPlacementDirection(this);
            this.RealLook = Look.Clone();
        }
        public virtual void OnFightEndedWhilePlaying()
        {
            if (OnTurnEndEvt != null)
                OnTurnEndEvt(this);
        }
        public virtual void OnTurnStarted()
        {
            if (OnTurnStartEvt != null)
                OnTurnStartEvt(this);


        }
        public void ChangeLook(ContextActorLook newLook, Fighter source)
        {
            this.Look = newLook;
            this.Fight.Send(new GameActionFightChangeLookMessage((ushort)ActionsEnum.ACTION_CHARACTER_CHANGE_LOOK, source.Id, Id, newLook.ToEntityLook()));
        }
        public virtual void Move(List<short> path)
        {
            if (Fight.Ended || !Fight.Started)
                return;
            if (Fight.IsCellsFree(path.Where(x => x != path.First()).ToArray()))
            {
                for (int i = 1; i < path.Count; i++)
                {
                    if (Fight.ShouldTriggerOnMove(path[i]))
                    {
                        if (i + 1 <= path.Count)
                        {
                            path.RemoveRange(i + 1, path.Count - i - 1);
                        }
                        break;
                    }
                }

                DirectionsEnum direction = (DirectionsEnum)PathParser.GetDirection(path.Last());

                short mpCost = PathParser.GetDistanceBetween((short)CellId, path.Last());

                if (TriggerBuffs(TriggerType.BEFORE_MOVE, mpCost))
                    return;

                if (mpCost <= Stats.MovementPoints.TotalInContext() && mpCost > 0)
                {
                    short previousCellId = this.CellId;
                    if (IsCarried)
                    {
                        Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_TRIGGERED);
                        Throw(path[1]);
                        path.Remove(path[0]);
                        Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_TRIGGERED);
                    }
                    if (CarryFighter)
                    {
                        Carried.CellId = path.Last();
                    }

                    Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_MOVE);
                    this.CellId = path.Last();
                    this.Direction = direction;

                    if (Stats.InvisibilityState == GameActionFightInvisibilityStateEnum.INVISIBLE)
                    {
                        Team.Send(new GameMapMovementMessage(path.ToArray(), Id));
                    }
                    else
                    {
                        Fight.Send(new GameMapMovementMessage(path.ToArray(), Id));
                    }
                    this.UseMp(mpCost);
                    TriggerBuffs(TriggerType.AFTER_MOVE, mpCost);
                    TriggerMarks(this, MarkTriggerTypeEnum.AFTER_MOVE, mpCost);

                    Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_MOVE);
                    this.Fight.CheckDeads();
                    this.Fight.CheckFightEnd();
                }
                else
                {
                    this.OnMoveFailed();
                }
            }
            else
            {
                this.OnMoveFailed();
            }

        }
        public void DebuffState(short stateId)
        {
            var buffs = this.GetBuffs<StateBuff>(x => x.StateRecord.Id == stateId);
            foreach (var buff in this.GetBuffs<StateBuff>())
            {
                this.RemoveAndDispellBuff(buff);
            }
        }
        public void TriggerMarks(Fighter source, MarkTriggerTypeEnum type, object token = null)
        {
            // Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_TRIGGERED);
            foreach (var mark in Fight.Marks.FindAll(x => x.TriggerType.HasFlag(type) && x.ContainsCell(CellId)))
            {
                mark.Trigger(source, type, token);
            }
            //   Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_TRIGGERED);
        }
        public virtual void OnMoveFailed()
        {

        }

        /// <summary>
        /// Do Stuff here will SpellHistory
        /// </summary>
        public virtual void AddCooldownOnSpell(Fighter source, ushort spellId, short value)
        {

        }
        public SpellCastResultEnum CanCastSpell(SpellLevelRecord level, short sourceCellId, short cellId)
        {
            Fighter target = Fight.GetFighter(cellId);
            if (Fight.Ended)
            {
                return SpellCastResultEnum.FightEnded;
            }
            if (!IsFighterTurn || !Alive)
            {
                return SpellCastResultEnum.NotPlaying;
            }
            if (Stats.ActionPoints.TotalInContext() < level.ApCost)
            {
                return SpellCastResultEnum.NotEnoughAp;
            }
            if (level.StatesForbidden.Any(this.HasState))
            {
                return SpellCastResultEnum.StateForbidden;
            }
            if (level.StatesRequired.Any((short state) => !this.HasState(state)))
            {
                return SpellCastResultEnum.StateRequired;
            }
            if (!this.SpellHistory.CanCastSpell(level, cellId))
            {
                return SpellCastResultEnum.HistoryError;
            }
            if (level.NeedFreeCell && target != null)
            {
                return SpellCastResultEnum.NoRange;
            }
            if (level.NeedTakenCell && target == null)
            {
                return SpellCastResultEnum.NoRange;
            }
            if (level.NeedFreeTrapCell)
            {
                var marks = Fight.GetMarks<Mark>(x => x.CenterPoint.CellId == cellId);
                if (marks.Count() > 0)
                    return SpellCastResultEnum.NoRange;
            }
            if (level.CastTestLos && !this.Fight.CanBeSeen(cellId, CellId, false))
            {
                return SpellCastResultEnum.CantBeSeen;
            }
            short[] castZone = this.GetCastZone(sourceCellId, level);
            if (!castZone.Contains(cellId))
            {
                return SpellCastResultEnum.NoRange;
            }
            return SpellCastResultEnum.Ok;
        }
        public virtual short[] GetCastZone(short sourceCellId, SpellLevelRecord spellLevel)
        {
            uint num = (uint)spellLevel.MaxRange;
            if (spellLevel.RangeCanBeBoosted)
            {
                num += (uint)this.Stats.Range.TotalInContext();
                if (num < spellLevel.MinRange)
                {
                    num = (uint)spellLevel.MinRange;
                }
                num = System.Math.Min(num, 280u);
            }
            IShape shape;
            if (spellLevel.CastInDiagonal && spellLevel.CastInLine)
            {
                shape = new Cross((byte)spellLevel.MinRange, (byte)num)
                {
                    AllDirections = true
                };
            }
            else
            {
                if (spellLevel.CastInLine)
                {
                    shape = new Cross((byte)spellLevel.MinRange, (byte)num);
                }
                else
                {
                    if (spellLevel.CastInDiagonal)
                    {
                        shape = new Cross((byte)spellLevel.MinRange, (byte)num)
                        {
                            Diagonal = true
                        };
                    }
                    else
                    {
                        shape = new Lozenge((byte)spellLevel.MinRange, (byte)num);
                    }
                }
            }
            return shape.GetCells(sourceCellId, Fight.Map);
        }
        public virtual void OnSpellCastFailed(SpellCastResultEnum result, SpellLevelRecord level)
        {

        }
        public FightSpellCastCriticalEnum RollCriticalDice(SpellLevelRecord spell)
        {
            AsyncRandom asyncRandom = new AsyncRandom();

            FightSpellCastCriticalEnum result = FightSpellCastCriticalEnum.NORMAL;

            int percentage = spell.CriticalHitProbability + Stats.CriticalHit.TotalInContext();

            if (spell.CriticalHitProbability > 0 && asyncRandom.TriggerAleat(percentage))
            {
                result = FightSpellCastCriticalEnum.CRITICAL_HIT;
            }
            return result;
        }
        public void ForceSpellCast(SpellRecord spell, sbyte grade, short cellId)
        {
            SpellLevelRecord level = spell.GetLevel(grade);
            ForceSpellCast(level, cellId);

        }
        public void ForceSpellCast(SpellLevelRecord level, short cellId)
        {
            Fight.Send(new GameActionFightSpellCastMessage(0, Id, false, false, 0,
                cellId, (sbyte)FightSpellCastCriticalEnum.NORMAL,
                  level.SpellId, level.Grade, new short[0]));

            if (CustomSpellHandlerProvider.IsHandled(level.SpellId))
            {
                CustomSpellHandlerProvider.Handle(this, level, new MapPoint(cellId), false);
            }
            else
            {
                SpellEffectsManager.Instance.HandleEffects(this, level, new MapPoint(cellId), false);
            }
        }

        public virtual bool CastSpell(SpellRecord spell, sbyte grade, short cellId, int targetId = 0, bool verif = true)
        {
            SpellLevelRecord level = spell.GetLevel(grade);

            short sourceCellId = this.CellId;

            if (level != null)
            {

                Marks.Portal end = null;
                Marks.Portal start = null;

                if (!PortalProvider.Instance.SPELLS_NOPORTAL.Contains(spell.Id))
                {
                    if (PortalProvider.Instance.HasPortail(Fight, cellId))
                    {
                        if (Fight.IsCellFree(cellId))
                        {
                            var two = PortalProvider.Instance.GetPortalsTuple(Fight, cellId);

                            if (two != null && two.Item2 != null)
                            {
                                start = two.Item1;
                                end = two.Item2;

                                var syme = Point.GetCellSymetrieByPortail(new MapPoint(cellId), new MapPoint(end.CenterPoint.CellId));

                                cellId = syme.CellId;
                                sourceCellId = end.CenterPoint.CellId;

                                this.Stats.FinalDamageCoefficient = (start != null ? (1d + ((double)start.SpellLevel.Effects.First().Value +
                                    2d * (double)PortalProvider.Instance.GetCasesCount(Fight, (short)start.CenterPoint.CellId)) / 100d) : 0);

                            }
                        }
                    }
                }


                SpellCastResultEnum canCast = SpellCastResultEnum.Ok;

                if (verif)
                {
                    canCast = CanCastSpell(level, sourceCellId, cellId);
                }



                if (canCast != SpellCastResultEnum.Ok)
                {
                    OnSpellCastFailed(canCast, level);
                    return false;
                }

                Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_MOVE);
                Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);


                if (Stats.InvisibilityState == GameActionFightInvisibilityStateEnum.INVISIBLE)
                {
                    if (!level.Silent)
                    {
                        SetInvisiblityState(GameActionFightInvisibilityStateEnum.VISIBLE, this);
                    }
                    else
                        OnDetected();

                }

                FightSpellCastCriticalEnum fightSpellCastCriticalEnum = this.RollCriticalDice(level);

                var portals = (end != null ? new short[] { start.Id, end.Id } : new short[0]);


                   Fight.Send(new GameActionFightSpellCastMessage((ushort)ActionsEnum.ACTION_FIGHT_CAST_SPELL, Id, false, true, targetId, cellId, (sbyte)fightSpellCastCriticalEnum,
                      spell.Id, grade, portals));
                try
                {
                    UseAp(level.ApCost);

                    if (CustomSpellHandlerProvider.IsHandled(spell.Id))
                    {
                        CustomSpellHandlerProvider.Handle(this, level, new MapPoint(cellId), fightSpellCastCriticalEnum == FightSpellCastCriticalEnum.CRITICAL_HIT);
                    }
                    else
                    {
                            SpellEffectsManager.Instance.HandleEffects(this, level, new MapPoint(cellId), fightSpellCastCriticalEnum == FightSpellCastCriticalEnum.CRITICAL_HIT);
                       

                    }
                }
                catch
                {
                    Fight.SequencesManager.EndAllSequences();
                }
                finally
                {


                    OnSpellCasted(level, cellId, fightSpellCastCriticalEnum);
                    Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);
                    Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_MOVE);
                    Fight.CheckDeads();
                    Fight.CheckFightEnd();

                }
                return true;
            }
            else
                return false;


        }

        public virtual bool InsertInTimeline()
        {
            return true;
        }

        public void OnSpellCasted(SpellLevelRecord level, short cellId, FightSpellCastCriticalEnum result)
        {
            Stats.FinalDamageCoefficient = 1;
            this.SpellHistory.RegisterCastedSpell(level, this.Fight.GetFighter(cellId));

        }
        public bool IsThereEnemy(MapPoint[] points)
        {
            foreach (var point in points)
            {
                if (OposedTeam().GetFighter(x => x.CellId == point.CellId) != null)
                    return true;
            }
            return false;
        }
        public void DecrementAllCastedBuffsDuration()
        {
            foreach (Fighter current in this.Fight.GetAllFighters())
            {
                current.DecrementBuffsDuration(this);
            }
        }

        public void DecrementBuffsDuration(Fighter caster)
        {
            List<Buff> list = (
                from buff in this.Buffs
                where buff.Caster == caster && buff.Active
                where buff.DecrementDuration()
                select buff).ToList<Buff>();

            foreach (Buff current in list)
            {
                if (current is TriggerBuff && (current as TriggerBuff).Trigger.HasFlag(TriggerType.BUFF_ENDED))
                {
                    (current as TriggerBuff).Apply(TriggerType.BUFF_ENDED);
                }
                // if (!(current is TriggerBuff) || !(current as TriggerBuff).Trigger.HasFlag(BuffTriggerType.BUFF_ENDED_TURNEND))
                {
                    this.RemoveAndDispellBuff(current);
                }
            }
        }
        public short GetMPDistanceBetwenn(Fighter source)
        {
            return PathParser.GetDistanceBetween(source.CellId, this.CellId);
        }

        public bool RollMpLose(Fighter from)
        {
            return FormulasProvider.Instance.RollMpLose(from, this);
        }
        public bool RollApLose(Fighter from)
        {
            return FormulasProvider.Instance.RollApLose(from, this);
        }
        public void DecrementBuffsDelay()
        {
            List<TriggerBuff> list = (
                from buff in this.Buffs.OfType<TriggerBuff>()
                where buff.IsDelayed() && buff.DecrementDelay()
                select buff).ToList<TriggerBuff>();

            foreach (TriggerBuff current in list)
            {
                RemoveBuff(current, true);
                current.Apply(TriggerType.BUFF_DELAY_ENDED);
            }
        }
        public void RemoveBuff(Buff buff, bool sendMessage = true)
        {
            this.Buffs.Remove(buff);

            if (sendMessage)
                Fight.Send(new GameActionFightDispellEffectMessage(514, this.Id, this.Id, buff.Id));

            this.FreeBuffId(buff.Id);
        }
        public void DispellSpellBuffs(Fighter source, ushort spellId)
        {
            List<Buff> buffs = this.Buffs.FindAll(x => x.SpellId == spellId);

            foreach (var buff in buffs)
            {
                RemoveAndDispellBuff(buff, false);
            }

            Fight.Send(new GameActionFightDispellSpellMessage(0, source.Id, this.Id, spellId));
        }
        public void RemoveAndDispellBuff(Buff buff, bool sendMessage = true)
        {
            this.RemoveBuff(buff, sendMessage);
            buff.Dispell();
        }
        public void RemoveAndDispellAllBuffs()
        {
            Buff[] array = this.Buffs.ToArray();
            Buff[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                Buff buff = array2[i];
                this.RemoveAndDispellBuff(buff);
            }
        }
        public void RemoveAndDispellAllBuffs(Fighter caster)
        {
            Buff[] array = this.Buffs.ToArray();
            Buff[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                Buff buff = array2[i];
                if (buff.Caster == caster)
                {
                    this.RemoveAndDispellBuff(buff);
                }
            }
        }
        public void RemoveAllCastedBuffs()
        {
            foreach (Fighter current in this.Fight.GetAllFighters())
            {
                current.RemoveAndDispellAllBuffs(this);
            }
        }
        public bool TriggerBuffs(TriggerType trigger, object token = null)
        {
            bool result = false;
            Buff[] source = this.Buffs.ToArray();
            foreach (TriggerBuff current in
                from triggerBuff in source.OfType<TriggerBuff>()
                where (triggerBuff.Trigger & trigger) == trigger
                select triggerBuff)
            {
                this.Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_TRIGGERED);
                if (current.Apply(trigger, token))
                    result = true;

                Fight.Send(new GameActionFightTriggerEffectMessage(0, this.Id, this.Id, current.Id));
                this.Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_TRIGGERED);
            }
            return result;
        }
        public void AddState(SpellStateRecord state)
        {
            this.States.Add(state);
        }
        public void RemoveState(SpellStateRecord state)
        {
            this.States.Remove(state);
        }
        public bool HasState(short id)
        {
            return States.Find(x => x.Id == id) != null;
        }
        public bool HasState(Predicate<SpellStateRecord> action)
        {
            return States.Find(action) != null;
        }
        public void Slide(Fighter source, short cellId)
        {
            short previousCellId = this.CellId;
            MapPoint point = new MapPoint(cellId);

            DirectionsEnum direction = Point.OrientationTo(point);

            List<short> cells = new List<short>() { point.CellId };

            MapPoint point2 = new MapPoint(CellId);
            short i = 1;

            while (point2.CellId != point.CellId)
            {
                point2 = Point.GetCellInDirection(direction, i);
                i++;
                cells.Add(point2.CellId);

            }



            if (cells != null && cells.Count() > 0)
            {
                if (Fight.IsCellsFree(cells.ToArray()))
                {


                    bool sequence = Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_MOVE);

                    BeforeSlideEvt?.Invoke(this, source, previousCellId, cells.Last());

                    var msg = new GameActionFightSlideMessage(6, source.Id, this.Id, this.CellId, cells.Last());
                    if (Stats.InvisibilityState == GameActionFightInvisibilityStateEnum.INVISIBLE)
                    {
                        Team.Send(msg);
                    }
                    else
                    {
                        Fight.Send(msg);
                    }
                    this.CellId = cells.Last();

                    AfterSlideEvt?.Invoke(this, source, previousCellId, cells.Last());

                    this.TriggerMarks(this, MarkTriggerTypeEnum.AFTER_MOVE);

                    if (sequence)
                        Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_MOVE);


                }
            }
        }
        public bool IsFriendly(Fighter fighter)
        {
            return fighter.Team == Team;
        }
        public Mark[] GetMarks()
        {
            return Fight.Marks.FindAll(x => x.Source == this).ToArray();
        }

        public void DecrementMarkDurations()
        {
            foreach (var mark in GetMarks())
            {
                if (mark is IDurationMark)
                {
                    if (((IDurationMark)mark).DecrementDuration())
                    {
                        Fight.RemoveMark(this, mark);
                    }
                }
            }
        }
        public GameActionFightInvisibilityStateEnum GetInvisibilityStateFor(Character character)
        {
            if (character.Fighter.IsFriendly(this) && this.Stats.InvisibilityState != GameActionFightInvisibilityStateEnum.VISIBLE)
            {
                return GameActionFightInvisibilityStateEnum.DETECTED;
            }
            else
            {
                return Stats.InvisibilityState;
            }
        }
        public void SetInvisiblityState(GameActionFightInvisibilityStateEnum state, Fighter source)
        {
            GameActionFightInvisibilityStateEnum oldState = this.Stats.InvisibilityState;
            this.Stats.InvisibilityState = state;
            OnInvisibilityStateChanged(state, oldState, source);
        }

        private void OnInvisibilityStateChanged(GameActionFightInvisibilityStateEnum state, GameActionFightInvisibilityStateEnum oldState, Fighter source)
        {
            foreach (var fighter in Fight.GetFighters<CharacterFighter>(false))
            {
                fighter.Send(new GameActionFightInvisibilityMessage((ushort)ActionsEnum.ACTION_CHARACTER_MAKE_INVISIBLE, source.Id, this.Id, (sbyte)GetInvisibilityStateFor(fighter.Character)));
            }

            if (oldState == GameActionFightInvisibilityStateEnum.INVISIBLE)
            {
                RefreshFighter();
            }
        }
        private void OnDetected()
        {
            this.OposedTeam().Send(new GameActionFightInvisibleDetectedMessage(0, this.Id, this.Id, this.CellId));
        }
        private void RefreshFighter()
        {
            Fight.Send(new GameFightRefreshFighterMessage(GetFightFighterInformations()));
        }
        public short GetSpellBoost(ushort spellId)
        {
            short result;
            if (!this.BuffedSpells.ContainsKey(spellId))
            {
                result = 0;
            }
            else
            {
                result = this.BuffedSpells[spellId];
            }
            return result;
        }
        public void BuffSpell(ushort spellId, short boost)
        {
            if (!BuffedSpells.ContainsKey(spellId))
            {
                BuffedSpells.Add(spellId, boost);
            }
            else
            {
                BuffedSpells[spellId] += boost;
            }
        }
        public void UnBuffSpell(ushort spellId, short boost)
        {
            if (BuffedSpells.ContainsKey(spellId))
            {
                BuffedSpells[spellId] -= boost;

                if (BuffedSpells[spellId] == 0)
                {
                    BuffedSpells.Remove(spellId);
                }
            }
        }
        public void Teleport(Fighter source, MapPoint point)
        {
            var msg = new GameActionFightTeleportOnSameMapMessage(1, this.Id, this.Id, point.CellId);
            if (Stats.InvisibilityState == GameActionFightInvisibilityStateEnum.INVISIBLE)
            {
                Team.Send(msg);
            }
            else
            {
                Fight.Send(msg);
            }

            this.CellId = point.CellId;

            if (OnTeleportEvt != null)
                OnTeleportEvt(this, source);


        }
        public void SwitchPosition(Fighter source)
        {
            if (CantSwitchPosition)
                return;

            short cellId = this.Point.CellId;

            bool seq = Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_MOVE);


            Fight.Send(new GameActionFightExchangePositionsMessage(0, source.Id, this.Id, cellId, source.CellId));

            this.CellId = source.CellId;
            source.CellId = cellId;

            if (OnTeleportEvt != null)
                OnTeleportEvt(this, source);

            if (source.OnTeleportEvt != null)
                source.OnTeleportEvt(this, source);

            if (seq)
                Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_MOVE);
        }
        public virtual void Heal(Fighter source, short delta)
        {
            if (delta <= 0 || Incurable)
            {
                return;
            }

            if (Stats.CurrentLifePoints + delta > Stats.CurrentMaxLifePoints)
            {
                Fight.Send(new GameActionFightLifePointsGainMessage(0, source.Id, Id, (uint)(Stats.CurrentMaxLifePoints - Stats.CurrentLifePoints)));
                Stats.CurrentLifePoints = Stats.CurrentMaxLifePoints;
            }
            else
            {
                Fight.Send(new GameActionFightLifePointsGainMessage(0, source.Id, Id, (uint)delta));
                Stats.CurrentLifePoints += delta;
            }

            if (OnHealEvt != null)
            {
                OnHealEvt(this, source);
            }

        }
        public short InflictDamages(Damage damage, bool triggerBuffs = true)
        {
            if (OnAttemptToInflictEvt != null)
                OnAttemptToInflictEvt(this, damage);

            if (IsInvulnerable)
            {
                OnDamageReduced(damage.Delta);
                return 0;
            }
            if (InvulnerableMelee && Abilities.MeleeWith(damage.Source))
            {
                return 0;
            }
            if (InvunlerableRange && !Abilities.MeleeWith(damage.Source))
            {
                return 0;
            }

            if (BeforeTakeDamagesEvt != null)
                BeforeTakeDamagesEvt(this, damage);

            if (triggerBuffs && this.TriggerBuffs(TriggerType.BEFORE_ATTACKED, damage))
                return 0;


            damage.Delta -= Stats.Reflect.TotalInContext();// les dommages retournées sont ils soustrait a la somme des dommages?



            short result = this.LoseLife(damage, triggerBuffs);

            if (triggerBuffs) // prevent StackOverflow.
            {
                if (Stats.Reflect.TotalInContext() > 0)
                    this.ReflectDamages(damage);
            }

            return result;

        }
        public void OnDamageReduced(short amount)
        {
            Fight.Send(new GameActionFightReduceDamagesMessage(0, Id, Id, (uint)amount));
        }

        private void ReflectDamages(Damage damage)
        {
            damage.Source.InflictDamages(Stats.Reflect.TotalInContext(), this);
        }
        public void InflictPushDamages(Fighter source, sbyte cellsCount, bool headOn)
        {
            short num = FormulasProvider.Instance.GetPushDamages(source, this, cellsCount, headOn);
            this.InflictDamages(num, source);

            if (OnPushDamages != null)
                OnPushDamages(this, source, num, cellsCount, headOn);
        }
        public short InflictDamages(short delta, Fighter source)
        {
            return this.InflictDamages(new Damage(source, this, delta), false);
        }
        private short LoseLife(Damage damage, bool triggerBuffs = true)
        {
            damage.CalculateDamageResistance();
            ushort erosionDelta = (ushort)CalculateErosionDamage(damage.Delta);
            damage.CalculateDamageReduction(erosionDelta);
            damage.ApplyFinalBoost();
            if (damage.Delta <= 0)
                return 0;

            if (Stats.ShieldPoints > 0)
            {
                if (Stats.ShieldPoints - damage.Delta <= 0)
                {
                    short rest = (short)(damage.Delta - Stats.ShieldPoints);
                    Fight.Send(new GameActionFightLifeAndShieldPointsLostMessage(0, damage.Source.Id, Id, (uint)rest, 0, (ushort)Stats.ShieldPoints));
                    Stats.SetShield(0);
                    if (Stats.CurrentLifePoints - rest <= 0)
                    {
                        Stats.CurrentLifePoints = 0;
                    }
                    else
                    {
                        ShowFighter();
                        Stats.CurrentLifePoints -= rest;

                        if (OnDamageTaken != null)
                            OnDamageTaken(this, damage);
                        if (triggerBuffs)
                            TriggerBuffs(TriggerType.AFTER_ATTACKED, damage);
                    }
                    return rest;
                }
                else
                {
                    Fight.Send(new GameActionFightLifeAndShieldPointsLostMessage(0, damage.Source.Id, Id, 0, 0, (ushort)damage.Delta));
                    Stats.RemoveShield(damage.Delta);
                    ShowFighter();

                    if (OnDamageTaken != null)
                        OnDamageTaken(this, damage);
                    if (triggerBuffs)
                        TriggerBuffs(TriggerType.AFTER_ATTACKED, damage);
                    return 0;
                }
            }
            if (Stats.CurrentLifePoints - damage.Delta <= 0)
            {
                int delta = Stats.CurrentLifePoints;
                Fight.Send(new GameActionFightLifePointsLostMessage(0, damage.Source.Id, Id, (ushort)Stats.CurrentLifePoints, 0));
                Stats.CurrentLifePoints = 0;
                return (short)delta;
            }
            else
            {

                Stats.CurrentMaxLifePoints -= erosionDelta;
                Stats.CurrentLifePoints -= damage.Delta;

                Fight.Send(new GameActionFightLifePointsLostMessage(0, damage.Source.Id, Id, (ushort)damage.Delta, erosionDelta)); // 0 => erosion delta

                if (OnDamageTaken != null)
                    OnDamageTaken(this, damage);

                if (triggerBuffs)
                    this.TriggerBuffs(TriggerType.AFTER_ATTACKED, damage);

                return damage.Delta;
            }

        }
        public virtual int CalculateErosionDamage(int damages)
        {
            var num = Stats.Erosion;

            if (num > 50)
            {
                num = 50;
            }
            return (int)(damages * (num / 100.0));
        }
        public virtual int CalculateArmorValue(int reduction)
        {
            return (int)((double)(reduction * (int)(100 + 5 * this.Level)) / 100.0);
        }
        public void RegainAp(int sourceId, short delta)
        {
            Stats.ApUsed -= delta;
            Stats.ActionPoints.Context += delta;
            Fight.PointsVariation(sourceId, Id, ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_WIN, delta);
        }
        public void RegainMp(int sourceId, short delta)
        {
            Stats.MpUsed -= delta;
            Stats.MovementPoints.Context += delta;
            Fight.PointsVariation(sourceId, Id, ActionsEnum.ACTION_CHARACTER_MOVEMENT_POINTS_WIN, delta);
        }
        public void LostAp(int sourceId, short delta)
        {
            Stats.ApUsed += delta;
            Stats.ActionPoints.Context -= delta;
            Fight.PointsVariation(sourceId, Id, ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_LOST, (short)(-delta));
        }
        public void LostMp(int sourceId, short delta)
        {
            Stats.MpUsed += delta;
            Stats.MovementPoints.Context -= delta;
            Fight.PointsVariation(sourceId, Id, ActionsEnum.ACTION_CHARACTER_MOVEMENT_POINTS_LOST, (short)(-delta));
        }
        public void UseAp(short amount)
        {
            Stats.UseAp(amount);
            Fight.PointsVariation(Id, Id, ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_USE, (short)(-amount));

            if (OnApUsedEvt != null)
                OnApUsedEvt(this, amount);
        }
        public void UseMp(short amount)
        {
            Stats.UseMp(amount);
            Fight.PointsVariation(Id, Id, ActionsEnum.ACTION_CHARACTER_MOVEMENT_POINTS_USE, (short)(-amount));

            if (OnMpUsedEvt != null)
                OnMpUsedEvt(this, amount);
        }

        /// <summary>
        /// Kill Fighter
        /// </summary>
        /// <param name="killedBy">Source</param>
        /// <param name="recusiveCall">Is The method is called from KillSummons and can then create infinityLoop (stackoverflow)</param>
        public void Die(Fighter killedBy, bool recusiveCall = false)
        {
            if (Alive)
            {
                TriggerBuffs(TriggerType.BEFORE_DEATH);
                this.KillSummons(killedBy);
                this.RemoveAndDispellAllBuffs();
                this.DropCarried();
                this.RemoveAllCastedBuffs();
                this.RemoveMarks();
                this.DeathTime = DateTime.Now;

                BeforeDeadEvt?.Invoke(this);

                Fight.Send(new GameActionFightDeathMessage((ushort)ActionsEnum.ACTION_CHARACTER_DEATH, killedBy.Id, Id));

                TriggerBuffs(TriggerType.AFTER_DEATH);

                this.Alive = false;

                AfterDeadEvt?.Invoke(this, recusiveCall);
            }
            else
            {
                Fight.Reply("Cannot kill " + this + ", he is already dead!");
            }

        }
        public void Carry(Fighter source, SpellEffectHandler handler)
        {
            handler.AddStateBuff(source, SpellStateRecord.GetState(CARRY_STATE_ID), -1, FightDispellableEnum.DISPELLABLE);
            handler.AddStateBuff(this, SpellStateRecord.GetState(CARRIED_STATE_ID), -1, FightDispellableEnum.DISPELLABLE);
            source.Carried = this;
            this.Carrying = source;
            Fight.Send(new GameActionFightCarryCharacterMessage((ushort)ActionsEnum.ACTION_CARRY_CHARACTER, source.Id,
                this.Id, this.CellId));
            this.CellId = source.CellId;
        }
        public void Throw(short cellId)
        {
            Fight.Send(new GameActionFightThrowCharacterMessage((ushort)ActionsEnum.ACTION_THROW_CARRIED_CHARACTER,
                     Carrying.Id, Id, cellId));
            Carrying.DebuffState(CARRY_STATE_ID);
            this.DebuffState(CARRIED_STATE_ID);
            this.CellId = cellId;
            this.Carrying.Carried = null;
            this.Carrying = null;
        }
        public void DropCarried()
        {
            if (CarryFighter)
            {
                Fight.Send(new GameActionFightDropCharacterMessage((ushort)ActionsEnum.ACTION_THROW_CARRIED_CHARACTER,
                   Id, Carried.Id, CellId));
                this.DebuffState(CARRIED_STATE_ID);
                Carried.Carrying = null;
                Carried = null;

            }
        }
        private void RemoveMarks()
        {
            foreach (var mark in GetMarks())
            {
                Fight.RemoveMark(this, mark);
            }
        }
        public Fighter[] GetSummons(bool aliveOnly = true)
        {
            return Team.GetFighters<IOwnable>(aliveOnly).FindAll(x => x.IsOwner(this)).Cast<Fighter>().ToArray();
        }
        public Fighter GetLastSummon()
        {
            return GetSummons().LastOrDefault();
        }
        public void KillSummons(Fighter source)
        {
            var summons = GetSummons();

            foreach (var summon in summons)
            {
                summon.Die(source, true);
            }
        }

        public virtual void PassTurn()
        {
            if (this.IsFighterTurn)
            {
                this.Fight.StopTurn();
            }
        }
        public virtual void OnTurnEnded()
        {
            if (OnTurnEndEvt != null)
                OnTurnEndEvt(this);

            this.Stats.OnTurnEnded();

        }
        public IEnumerable<short> FindPathTo(Fighter fighter)
        {
            Pathfinder path = new Pathfinder(Fight.Map, fighter.CellId);
            path.PutEntities(Fight.GetAllFighters());
            var cells = path.FindPath(this.CellId);
            if (cells != null)
            {
                cells.Remove(this.CellId);
                cells.Insert(0, fighter.CellId);
                cells = cells.Distinct().ToList();

                for (int i = 0; i < fighter.Stats.MovementPoints.TotalInContext() + 1; i++)
                {
                    if (cells.Count > i)
                        yield return cells[i];
                }
            }
        }
        public FightTeam OposedTeam()
        {
            return Team == Fight.BlueTeam ? Fight.RedTeam : Fight.BlueTeam;
        }
        public virtual bool CanPlay()
        {
            return Alive;
        }
        public virtual IFightResult GetFightResult()
        {
            return new FightResult(this, this.GetFighterOutcome(), this.Loot);
        }
        public void OnDodgePointLoss(ActionsEnum actionEnum, Fighter source, ushort amount)
        {
            Fight.Send(new GameActionFightDodgePointLossMessage((ushort)actionEnum, source.Id, Id, amount));
        }
        public FightOutcomeEnum GetFighterOutcome()
        {
            bool flag = this.Team.Alives == 0;
            bool flag2 = this.OposedTeam().Alives == 0;
            FightOutcomeEnum result;
            if (!flag && flag2)
            {
                result = FightOutcomeEnum.RESULT_VICTORY;
            }
            else
            {
                if (flag && !flag2)
                {
                    result = FightOutcomeEnum.RESULT_LOST;
                }
                else
                {
                    result = FightOutcomeEnum.RESULT_DRAW;
                }
            }
            return result;
        }
        public void DisplaySmiley(ushort smileyId)
        {
            Fight.Send(new ChatSmileyMessage(Id, smileyId, -1));
        }
        public virtual uint GetDroppedKamas()
        {
            return 0u;
        }
        public virtual IEnumerable<DroppedItem> RollLoot(IFightResult looter, int dropBonusPercent)
        {
            return new DroppedItem[0];
        }
        public IdentifiedEntityDispositionInformations GetIdentifiedEntityDispositionInformations()
        {
            return new IdentifiedEntityDispositionInformations((short)CellId, (sbyte)Direction, (double)Id);
        }
        public abstract GameFightFighterInformations GetFightFighterInformations();

        public abstract FightTeamMemberInformations GetFightTeamMemberInformations();

        public override string ToString()
        {
            return this.Name;
        }

        public Buff[] GetDispelableBuffs(bool strongDispell = false)
        {
            if (strongDispell)
            {
                return Buffs.FindAll(x => x.Dispelable == FightDispellableEnum.DISPELLABLE || x.Dispelable == FightDispellableEnum.DISPELLABLE_BY_STRONG_DISPEL).ToArray();
            }
            else
            {
                return Buffs.FindAll(x => x.Dispelable == FightDispellableEnum.DISPELLABLE).ToArray();
            }
        }
    }
}
