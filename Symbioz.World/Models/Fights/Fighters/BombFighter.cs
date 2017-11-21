using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Fights.Marks;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Providers.Brain.Behaviors;
using Symbioz.World.Providers.Fights.Effects;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Fighters
{
    public class BombFighter : Fighter, ISummon<Fighter>, IMonster
    {
        public const short SCALE_PER_TURN = 30;

        public const short BUFF_TURN_COUNT = 3;

        public const short COMBO_DAMAGE_FACTOR = 2;

        public const short INITIAL_COMBO_DAMAGE = 20;

        public ushort MonsterId
        {
            get
            {
                return Template.Id;
            }
        }
        public Fighter Owner
        {
            get;
            set;
        }
        private MonsterRecord Template
        {
            get;
            set;
        }
        private MonsterGrade Grade
        {
            get;
            set;
        }
        private short SummonCellId
        {
            get;
            set;
        }
        private sbyte GradeId
        {
            get;
            set;
        }
        public List<Wall> Walls
        {
            get;
            private set;
        }
        public SpellLevelRecord SummonSpellLevel
        {
            get;
            private set;
        }
        public SpellBombRecord SpellBombRecord
        {
            get;
            private set;
        }
        public SpellLevelRecord WallSpellLevel
        {
            get;
            private set;
        }
        public int BuffTurnCount
        {
            get;
            private set;
        }
        public short ComboDamages
        {
            get;
            private set;
        }
        public BombFighter(MonsterRecord template, Fighter owner, FightTeam team, short summonCellId, sbyte gradeId, SpellLevelRecord summonSpellLevel)
            : base(team, 0)
        {
            this.Owner = owner;
            this.Template = template;
            this.GradeId = gradeId;
            this.Grade = Template.GetGrade(gradeId);
            this.SummonCellId = summonCellId;
            this.BeforeSlideEvt += BombFighter_BeforeSlideEvt;
            this.Owner.OnTurnStartEvt += OnOwnerTurnStart;
            this.AfterSlideEvt += BombFighter_OnSlideEvt;
            this.OnTeleportEvt += BombFighter_OnTeleportEvt;
            this.AfterDeadEvt += BombFighter_AfterDeadEvt;
            this.OnDamageTaken += BombFighter_OnDamageTaken;
            this.BeforeDeadEvt += BombFighter_OnDeadEvt;
            this.Walls = new List<Wall>();
            this.SummonSpellLevel = summonSpellLevel;
            this.SpellBombRecord = SpellBombRecord.GetSpellBombRecord(summonSpellLevel.SpellId);
            this.WallSpellLevel = SpellRecord.GetSpellRecord(SpellBombRecord.WallSpellId).GetLevel(GradeId);
            this.BuffTurnCount = BUFF_TURN_COUNT;
        }

        private void BombFighter_AfterDeadEvt(Fighter arg1, bool arg2)
        {
            UnbindEvents(arg1);
        }

        private void BombFighter_OnDamageTaken(Fighter fighter, Damage dmg)
        {

        }

        private void UnbindEvents(Fighter obj)
        {
            this.BeforeSlideEvt -= BombFighter_BeforeSlideEvt;
            this.Owner.OnTurnStartEvt -= OnOwnerTurnStart;
            this.AfterSlideEvt -= BombFighter_OnSlideEvt;
            this.OnTeleportEvt -= BombFighter_OnTeleportEvt;
            this.AfterDeadEvt -= BombFighter_AfterDeadEvt;
            this.BeforeDeadEvt -= BombFighter_OnDeadEvt;
            this.OnDamageTaken -= BombFighter_OnDamageTaken;
        }

        private void OnOwnerTurnStart(Fighter obj)
        {
            bool sequence = Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);
            Look.AddScale(SCALE_PER_TURN);
            this.ChangeLook(Look, this);

            this.BuffTurnCount--;

            if (ComboDamages == 0)
            {
                ComboDamages = INITIAL_COMBO_DAMAGE;
            }
            else
            {
                ComboDamages *= COMBO_DAMAGE_FACTOR;
            }

            if (BuffTurnCount == 0)
            {
                Owner.OnTurnStartEvt -= OnOwnerTurnStart;
            }
            if (sequence)
                Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);
        }

        private void BombFighter_BeforeSlideEvt(Fighter target, Fighter source, short startCellId, short endCellId)
        {
            foreach (var wall in new List<Wall>(this.Walls))
            {
                wall.Destroy();
            }
        }
        void BombFighter_OnDeadEvt(Fighter obj)
        {
            foreach (var wall in new List<Wall>(this.Walls))
            {
                this.Walls.Remove(wall);
                Fight.RemoveMark(Owner, wall);
            }

        }
        public void Detonate(Fighter source)
        {
            Die(this);
            var level = SpellRecord.GetSpellRecord(SpellBombRecord.CibleExplosionSpellId).GetLevel(GradeId);
            source.ForceSpellCast(level, CellId);

            foreach (var fighter in this.GetNearFighters<BombFighter>())
            {
                if (fighter.Alive && fighter.Owner == Owner)
                {
                    fighter.Detonate(source);
                }
            }
        }
        void BombFighter_OnTeleportEvt(Fighter target, Fighter source)
        {
            BombProvider.Instance.UpdateWalls(this);
        }

        void BombFighter_OnSlideEvt(Fighter target, Fighter source, short startCellId, short endCellId)
        {
            BombProvider.Instance.UpdateWalls(this);
        }



        public bool IsOwner(Fighter fighter)
        {
            return Owner == fighter;
        }

        public override bool Sex
        {
            get
            {
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return Template.Name;
            }
        }

        public override ushort Level
        {
            get
            {
                return Grade.Level;
            }

        }
        public override void Initialize()
        {
            this.Id = Fight.PopNextContextualId();
            this.Stats = new FighterStats(Grade, 0);
            this.Look = Template.Look.Clone();
            base.Initialize();
            this.FightStartCell = SummonCellId;
            this.CellId = SummonCellId;
            this.Direction = Owner.Point.OrientationTo(this.Point, false);
            this.Stats.InitializeBomb(Owner);

        }
        public override GameFightFighterInformations GetFightFighterInformations()
        {
            return new GameFightMonsterInformations(Id, Look.ToEntityLook(), new EntityDispositionInformations((short)CellId, (sbyte)Direction),
             Team.Id, 0, Alive, Stats.GetFightMinimalStats(), new ushort[0], Template.Id, GradeId);
        }

        public override FightTeamMemberInformations GetFightTeamMemberInformations()
        {
            throw new NotImplementedException();
        }


    }
}
