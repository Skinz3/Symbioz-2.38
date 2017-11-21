using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Handlers.RolePlay;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Network;
using Symbioz.World.Providers.Maps.Path;
using Symbioz.World.Records;
using System;
using System.Collections.Generic;
using Symbioz.World.Records.Almanach;
using System.Drawing;
using System.Linq;
using Symbioz.Core;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Items;
using Symbioz.World.Providers;
using Symbioz.World.Models.Dialogs;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Exchanges;
using Symbioz.World.Providers.Items;
using Symbioz.World.Models.Dialogs.DialogBox;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Breeds;
using Symbioz.World.Records.Items;
using Symbioz.World.Records.Interactives;
using Symbioz.World.Records.Npcs;
using Symbioz.World.Models.Entities.Jobs;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Maps.Interactives;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities.HumanOptions;
using Symbioz.World.Providers.Parties;
using Symbioz.World.Providers.Arena;
using Symbioz.World.Handlers.Approach;
using Symbioz.World.Records.Spells;
using Symbioz.World.Models.Parties;
using Symbioz.World.Modules;
using Symbioz.World.Records.Guilds;
using Symbioz.World.Providers.Maps.Cinematics;
using Symbioz.World.Models.Entities.Shortcuts;
using Symbioz.World.Models.Guilds;
using Symbioz.World.Providers.Guilds;
using Symbioz.World.Models.Fights;

namespace Symbioz.World.Models.Entities
{
    public class Character : Entity
    {
        public WorldClient Client
        {
            get;
            set;
        }

        public CharacterRecord Record
        {
            get;
            private set;
        }
        public ushort ExpMultiplicator
        {
            get
            {
                return (ushort)Client.Characters.Count(x => ExperienceRecord.GetCharacterLevel(x.Exp) > Level);
            }
        }


        public BreedRecord Breed
        {
            get
            {
                return BreedRecord.GetBreed(Record.BreedId);
            }
        }
        public bool Riding
        {
            get
            {
                return Look.IsRiding;
            }
        }

        public bool ChangeMap
        {
            get;
            set;
        }
        public SpellShortcutBar SpellShortcutBar
        {
            get;
            private set;
        }
        public GeneralShortcutBar GeneralShortcutBar
        {
            get;
            private set;
        }
        public bool Collecting
        {
            get;
            set;
        }
        public PlayableFighter Fighter
        {
            get;
            private set;
        }
        public CharacterFighter FighterMaster
        {
            get;
            private set;
        }
        public bool Fighting
        {
            get
            {
                return Fighter != null;
            }
        }
        public int FighterCount
        {
            get
            {
                return MinationCount() + 1; // + companion?
            }
        }
        public bool InArena
        {
            get
            {
                return ArenaMember != null;
            }
        }
        public ArenaMember ArenaMember
        {
            get;
            private set;
        }
        /// <summary>
        /// Last Map before enter arena
        /// </summary>
        public int? PreviousRoleplayMapId
        {
            get;
            set;
        }
        public bool CanRegisterArena
        {
            get
            {
                return InRoleplay && !InArena;
            }
        }
        public bool HasGuild
        {
            get
            {
                return Record.GuildId != 0;
            }
        }
        public GuildInstance Guild
        {
            get;
            set;
        }
        public GuildMemberInstance GuildMember
        {
            get
            {
                return Guild.GetMember(Id);
            }
        }
        public Inventory Inventory
        {
            get;
            private set;
        }
        public Dialog Dialog
        {
            get;
            set;
        }
        public RequestBox RequestBox
        {
            get;
            set;
        }
        public ushort[] SkillsAllowed
        {
            get;
            private set;
        }
        private CharacterHumanOptionOrnament ActiveOrnament
        {
            get
            {
                return GetFirstHumanOption<CharacterHumanOptionOrnament>();
            }
        }
        private CharacterHumanOptionTitle ActiveTitle
        {
            get
            {
                return GetFirstHumanOption<CharacterHumanOptionTitle>();
            }
        }

        public AbstractParty Party
        {
            get;
            set;
        }

        public bool HasParty()
        {
            if (this.Party != null && this.Party.Members.Contains(this))
                return true;
            return false;
        }
        public bool HadBlockOtherPartiesInvitations
        {
            get;
            set;
        }

        public List<AbstractParty> GuestedParties
        {
            get;
            set;
        }

        public bool IsMute
        {
            get;
            set;
        }

        public T GetRequestBox<T>() where T : RequestBox
        {
            return (T)RequestBox;
        }
        public T GetDialog<T>() where T : Dialog
        {
            return (T)Dialog;
        }
        public void OpenDialog(Dialog dialog, bool force = false)
        {
            if (!Busy || force)
            {
                try
                {
                    this.Dialog = dialog;
                    this.Dialog.Open();
                }
                catch
                {
                    ReplyError("Impossible d'éxecuter l'action.");
                    LeaveDialog();
                }
            }
            else
            {
                ReplyError("Unable to open dialog while busy...");
            }
        }
        public void SwapFighter(PlayableFighter newFighter)
        {
            this.Fighter = newFighter;
        }
        public void SwapFighterToMaster()
        {
            this.Fighter = FighterMaster;
        }
        public PlayableFighter CreateFighter(FightTeam team)
        {
            if (Look.RemoveAura())
                RefreshActorOnMap();

            this.MovementKeys = null;
            this.IsMoving = false;
            this.Map.Instance.RemoveEntity(this);
            this.DestroyContext();
            this.CreateContext(GameContextEnum.FIGHT);
            this.RefreshStats();
            this.Client.Send(new GameFightStartingMessage((sbyte)team.Fight.FightType, team.Fight.BlueTeam.Id, team.Fight.RedTeam.Id));
            this.FighterMaster = new CharacterFighter(this, team, CellId);
            this.Fighter = FighterMaster;

            if (team.Fight.MinationAllowed)
                this.ApplyMination(this.FighterMaster, team);

            return Fighter;
        }
        private void ApplyMination(CharacterFighter master, FightTeam team)
        {
            CharacterItemRecord[] items = Inventory.GetEquipedMinationItems();

            foreach (var item in items)
            {
                EffectMination effect = item.FirstEffect<EffectMination>();
                EffectMinationLevel effectLevel = item.FirstEffect<EffectMinationLevel>();

                if (effectLevel == null) // to remove (axiom)
                {
                    effectLevel = new EffectMinationLevel(1, 0, 0);
                    item.AddEffect(effectLevel);
                }
                var fighter = new MinationMonsterFighter(team, MonsterRecord.GetMonster(effect.MonsterId),
                      effect.GradeId, effectLevel.Level, master, team.GetPlacementCell());

                team.AddFighter(fighter);
                fighter.SetLife(effectLevel.Level * 20, true);

            }
        }
        private int MinationCount()
        {
            return Array.FindAll(Inventory.GetEquipedItems(), x => x.HasEffect<EffectMination>()).Count();
        }
        public FighterRefusedReasonEnum CanRequestFight(Character target)
        {
            FighterRefusedReasonEnum result;
            if (target.Fighting || target.Busy)
            {
                result = FighterRefusedReasonEnum.OPPONENT_OCCUPIED;
            }
            else
            {
                if (this.Fighting || this.Busy)
                {
                    result = FighterRefusedReasonEnum.IM_OCCUPIED;
                }
                else
                {
                    if (target == this)
                    {
                        result = FighterRefusedReasonEnum.FIGHT_MYSELF;
                    }
                    else
                    {
                        if (this.ChangeMap || target.ChangeMap || target.Map != Map || !Map.Position.AllowFightChallenges || !Map.ValidForFight)
                        {
                            result = FighterRefusedReasonEnum.WRONG_MAP;
                        }
                        else
                        {
                            result = FighterRefusedReasonEnum.FIGHTER_ACCEPTED;
                        }
                    }
                }
            }
            return result;
        }
        public FighterRefusedReasonEnum CanAgress(Character target)
        {
            if (target.Client.Ip == this.Client.Ip && Client.Account.Role <= ServerRoleEnum.Animator)
            {
                return FighterRefusedReasonEnum.MULTIACCOUNT_NOT_ALLOWED;
            }
            if (target.Busy)
            {
                return FighterRefusedReasonEnum.OPPONENT_OCCUPIED;
            }
            if (target == this)
            {
                return FighterRefusedReasonEnum.FIGHT_MYSELF;
            }
            if (this.Level - target.Level > 20)
            {
                return FighterRefusedReasonEnum.INSUFFICIENT_RIGHTS;
            }
            if (!Map.Position.AllowAggression)
            {
                return FighterRefusedReasonEnum.WRONG_MAP;
            }
            if (target.Record.Alignment.Side == this.Record.Alignment.Side)
            {
                return FighterRefusedReasonEnum.WRONG_ALIGNMENT;
            }
            if (Busy)
            {
                return FighterRefusedReasonEnum.IM_OCCUPIED;
            }
            if (!InRoleplay)
            {
                return FighterRefusedReasonEnum.TOO_LATE;
            }

            return FighterRefusedReasonEnum.FIGHTER_ACCEPTED;
        }
        public long GetRewardExperienceFromPercentage(int percentage)
        {
            long result = (long)(UpperBoundExperience * (percentage / 100d));
            result = result / Level;
            return result;
        }
        public bool CanAlmanach(AlmanachRecord almanach)
        {
            return Record.LastAlmanachDay != almanach.Id;
        }
        public bool DoAlmanach(AlmanachRecord almanach)
        {
            if (this.Inventory.Exist(almanach.ItemGId, almanach.Quantity))
            {

                this.Inventory.RemoveItem(this.Inventory.GetFirstItem(almanach.ItemGId, almanach.Quantity), almanach.Quantity);
                this.OnItemLost(almanach.ItemGId, almanach.Quantity);

                this.Inventory.AddItem((ushort)almanach.RewardItemGId, (uint)almanach.RewardItemQuantity);
                this.OnItemGained((ushort)almanach.RewardItemGId, (uint)almanach.RewardItemQuantity);
                long xp = GetRewardExperienceFromPercentage(almanach.XpRewardPercentage);

                if (Level < 200)
                {
                    this.AddExperience((ulong)xp);
                    this.OnExperienceGained(xp);
                    this.RefreshStats();
                }
                this.Record.LastAlmanachDay = almanach.Id;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInDialog(DialogTypeEnum type)
        {
            if (Dialog == null)
                return false;
            return Dialog.DialogType == type;
        }
        public bool IsInExchange(ExchangeTypeEnum type)
        {
            var exchange = GetDialog<Exchange>();
            if (exchange != null)
                return exchange.ExchangeType == type;
            else
                return false;
        }
        public void AcceptRequest()
        {
            if (this.IsInRequest() && this.RequestBox.Target == this)
            {
                this.RequestBox.Accept();
            }
        }
        public void DenyRequest()
        {
            if (this.IsInRequest() && this.RequestBox.Target == this)
            {
                this.RequestBox.Deny();
            }
        }
        public void CancelRequest()
        {
            if (this.IsInRequest())
            {
                if (this.IsRequestSource())
                {
                    this.RequestBox.Cancel();
                }
                else
                {
                    if (this.IsRequestTarget())
                    {
                        this.DenyRequest();
                    }
                }
            }
        }
        public bool IsInRequest()
        {
            return this.RequestBox != null;
        }
        public bool IsRequestSource()
        {
            return this.IsInRequest() && this.RequestBox.Source == this;
        }
        public bool IsRequestTarget()
        {
            return this.IsInRequest() && this.RequestBox.Target == this;
        }
        public bool Busy
        {
            get
            {
                return Dialog != null || RequestBox != null || ChangeMap || !CanInteract;
            }
        }
        public bool CanInteract
        {
            get;
            set;
        }
        public override long Id
        {
            get
            {
                return Record.Id;
            }
        }

        public override string Name
        {
            get
            {
                return Record.Name;
            }
        }
        public override ContextActorLook Look
        {
            get
            {
                return Record.Look;
            }
            set
            {
                Record.Look = value;
            }
        }

        private ushort m_level;



        public ushort Level
        {
            get
            {
                return m_level;
            }

            private set
            {
                this.m_level = value;
                this.LowerBoundExperience = ExperienceRecord.GetExperienceForLevel(this.Level).Player;
                this.UpperBoundExperience = ExperienceRecord.GetExperienceForNextLevel(this.Level).Player;
            }
        }

        public ulong Experience
        {
            get
            {
                return this.Record.Exp;
            }
            private set
            {
                this.Record.Exp = value;

                if (value >= this.UpperBoundExperience && this.Level < ExperienceRecord.MaxCharacterLevel || value < this.LowerBoundExperience)
                {
                    ushort level = this.Level;
                    this.Level = ExperienceRecord.GetCharacterLevel(this.Record.Exp);
                    int difference = (int)(this.Level - level);
                    this.OnLevelChanged(level, difference, true);
                }
            }
        }
        public void RefreshStats()
        {
            Client.Send(new CharacterStatsListMessage(Record.Stats.GetCharacterCharacteristics(this)));
        }
        public void Restat(bool addStatPoints = true)
        {
            this.Record.Restat(addStatPoints);
            this.RefreshStats();
        }
        public void SetDirection(DirectionsEnum direction)
        {
            Record.Direction = (sbyte)direction;
            SendMap(new GameMapChangeOrientationMessage(new ActorOrientation(Id, (sbyte)direction)));
        }
        public void AddFollower(ContextActorLook look)
        {
            CharacterHumanOptionFollowers followers = GetFirstHumanOption<CharacterHumanOptionFollowers>();

            if (followers != null)
            {
                followers.AddFollower(look);
            }
            else
            {
                AddHumanOption(new CharacterHumanOptionFollowers(look));
            }
        }
        public void RemoveFollower(ContextActorLook look)
        {
            CharacterHumanOptionFollowers followers = GetFirstHumanOption<CharacterHumanOptionFollowers>();

            if (followers == null)
            {
                new Logger().Error("Error while removing follower!");
                return;
            }
            else
            {
                followers.RemoveFollower(look);

                if (followers.Looks.Count == 0)
                    RemoveHumanOption<CharacterHumanOptionFollowers>();
            }
        }

        public void AddExperience(ulong amount)
        {
            Experience += amount;
        }

        public void SetLevel(ushort newLevel)
        {
            if (newLevel > ExperienceRecord.MaxCharacterLevel)
            {
                Reply("New level must be < " + ExperienceRecord.MaxCharacterLevel);
            }
            else
            {
                Experience = ExperienceRecord.GetExperienceForLevel(newLevel).Player;
            }
        }
        private void OnLevelChanged(ushort oldLevel, int amount, bool send)
        {
            if (send && Level > oldLevel)
            {
                this.SendMap(new CharacterLevelUpInformationMessage((byte)this.Level, Record.Name, (uint)Id));
                Client.Send(new CharacterLevelUpMessage((byte)this.Level));

            }
            CheckSpells();

            if (Level > oldLevel)
            {
                Record.Stats.LifePoints += (5 * amount);
                Record.Stats.MaxLifePoints += (5 * amount);
                Record.SpellPoints += (ushort)(amount);
                Record.StatsPoints += (ushort)(5 * amount);

            }
            else if (Level < oldLevel)
            {
                Record.Stats.LifePoints += (5 * amount);
                Record.Stats.MaxLifePoints += (5 * amount);
                Record.StatsPoints = (ushort)(Level * 5 - 5);
                CheckRemovedSpells();
                Inventory.UnequipAll();
            }

            if (oldLevel < 100 && this.Level >= 100)
            {
                LearnEmote((byte)EmotesEnum.PowerAura);
                Record.Stats.ActionPoints.Base += 1;
                LearnOrnament((ushort)OrnamentsEnum.Hundred, send);
            }
            if (oldLevel < 160 && this.Level >= 160)
            {
                LearnOrnament((ushort)OrnamentsEnum.HundredSixty, send);
            }
            if (oldLevel < 200 && this.Level == 200)
            {
                LearnOrnament((ushort)OrnamentsEnum.TwoHundred, send);
            }

            if (HasParty())
            {
                Party.UpdateMember(this);
            }
            if (send)
            {

                RefreshActorOnMap();
                RefreshStats();
            }
        }
        public ulong LowerBoundExperience
        {
            get;
            private set;
        }
        public ulong UpperBoundExperience
        {
            get;
            private set;
        }

        private bool New
        {
            get;
            set;
        }

        private GameContextEnum? m_context
        {
            get; set;
        }

        public GameContextEnum? Context
        {
            get
            {
                return m_context;
            }
        }

        public bool InRoleplay
        {
            get
            {
                return Context.HasValue && Context.Value == GameContextEnum.ROLE_PLAY;
            }
        }

        public ushort MovedCell
        {
            get;
            set;
        }

        public ushort SubareaId
        {
            get
            {
                if (Map != null)
                {
                    return Map.SubAreaId;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override ushort CellId
        {
            get
            {
                return Record.CellId;
            }
            set
            {
                Record.CellId = value;
            }
        }

        public override DirectionsEnum Direction
        {
            get
            {
                return (DirectionsEnum)Record.Direction;
            }
            set
            {
                Record.Direction = (sbyte)value;
            }
        }
        public bool IsMoving
        {
            get;
            private set;
        }

        public short[] MovementKeys
        {
            get;
            private set;
        }

        public Character(WorldClient client, CharacterRecord record, bool isNew)
        {
            this.Record = record;
            this.Client = client;
            this.New = isNew;
            this.GuestedParties = new List<AbstractParty>();
            this.Level = ExperienceRecord.GetCharacterLevel(record.Exp);
            this.Inventory = new Inventory(this);
            this.SpellShortcutBar = new SpellShortcutBar(this);
            this.GeneralShortcutBar = new GeneralShortcutBar(this);
            this.SkillsAllowed = SkillsProvider.Instance.GetAllowedSkills(this).ToArray();
            this.Party = null;
            this.CanInteract = true;

            if (isNew)
            {
                OnLevelChanged(1, Level - 1, false);
            }

        }
        public void RefreshActorOnMap()
        {
            SendMap(new GameRolePlayShowActorMessage(GetActorInformations()));
        }
        public void RefreshActor()
        {
            Client.Send(new GameRolePlayShowActorMessage(GetActorInformations()));
        }
        public void CheckSpells()
        {
            foreach (var spell in Breed.GetSpellsForLevel(this.Level, Record.Spells))
            {
                LearnSpell(spell);
            }
            RefreshSpells();
        }
        public void CheckRemovedSpells()
        {
            var spells2 = Breed.GetSpellsForLevel(200, new List<CharacterSpell>());
            var spells = Breed.GetSpellsForLevel(this.Level, new List<CharacterSpell>());


            foreach (var spell in Record.Spells.ToArray())
            {
                if (!spells.Contains(spell.SpellId) && spells2.Contains(spell.SpellId))
                {
                    RemoveSpell(spell.SpellId);
                }
            }

        }
        public bool HasSpell(ushort spellId)
        {
            return Record.Spells.Find(x => x.SpellId == spellId) != null;
        }
        public void LearnSpell(ushort spellId)
        {
            if (!HasSpell(spellId))
            {
                Record.Spells.Add(new CharacterSpell(spellId, 1));
                if (SpellShortcutBar.CanAdd())
                {
                    SpellShortcutBar.Add(spellId);
                    RefreshShortcuts();
                }
                RefreshSpells();

                TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 3, spellId);
            }
        }
        public void RemoveSpell(ushort spellId)
        {
            if (HasSpell(spellId))
            {
                Record.Spells.RemoveAll(x => x.SpellId == spellId);
                SpellShortcutBar.Remove(spellId);
                RefreshSpells();
                RefreshShortcuts();
            }
        }
        public CharacterSpell GetSpell(ushort spellId)
        {
            return Record.Spells.Find(x => x.SpellId == spellId);
        }

        public void ModifySpell(ushort spellId, sbyte gradeId)
        {
            if (!Fighting)
            {
                CharacterSpell actualSpell = GetSpell(spellId);

                if (actualSpell == null)
                {
                    Client.Send(new SpellModifyFailureMessage());
                    return;
                }

                int cost = actualSpell.Grade < gradeId ? CharacterSpell.GetBoostCost(actualSpell.Grade, gradeId)
                    : CharacterSpell.GetBoostCost(gradeId, actualSpell.Grade);

                if (actualSpell.Grade < gradeId)
                {
                    if (cost <= Record.SpellPoints)
                    {
                        Record.SpellPoints -= (ushort)cost;
                    }
                    else
                    {
                        Client.Send(new SpellModifyFailureMessage());
                        return;
                    }
                }
                else
                {
                    if (actualSpell.Grade > gradeId)
                    {
                        Record.SpellPoints += (ushort)cost;
                    }
                    else
                    {
                        Client.Send(new SpellModifyFailureMessage());
                    }
                }

                actualSpell.SetGrade(gradeId);
                RefreshStats();
                Client.Send(new SpellModifySuccessMessage(spellId, gradeId));
            }
            else
            {
                Client.Send(new SpellModifyFailureMessage());
            }


        }

        public ShortcutBar GetShortcutBar(ShortcutBarEnum barEnum)
        {
            switch (barEnum)
            {
                case ShortcutBarEnum.GENERAL_SHORTCUT_BAR:
                    return GeneralShortcutBar;
                case ShortcutBarEnum.SPELL_SHORTCUT_BAR:
                    return SpellShortcutBar;
            }

            throw new Exception("Unknown shortcut bar, " + barEnum);
        }
        public void AddSpellPoints(ushort amount)
        {
            Record.SpellPoints += amount;
            RefreshStats();
        }
        public void RefreshShortcuts()
        {
            SpellShortcutBar.Refresh();
            GeneralShortcutBar.Refresh();

        }
        public void RefreshSpells()
        {
            Client.Send(new SpellListMessage(true, Record.Spells.ConvertAll<SpellItem>(x => x.GetSpellItem()).ToArray()));
        }
        public void OnItemGained(ushort gid, uint quantity)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, new object[] { quantity, gid });
        }
        public void OnExperienceGained(long experience)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 8, new object[] { experience });
        }
        public void OnItemLost(ushort gid, uint quantity)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 22, new object[] { quantity, gid });
        }
        public void OnItemSelled(ushort gid, uint quantity, uint price)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 65, new object[] { price, string.Empty, gid, quantity });
        }
        public void OnKamasGained(int amount)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 45, new object[] { amount });
        }
        public void OnKamasLost(int amount)
        {
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 46, new object[] { amount });
        }
        public void PlayEmote(byte id)
        {
            EmoteRecord template = EmoteRecord.GetEmote(id);

            if (!Collecting && !ChangeMap)
            {
                if (Look.RemoveAura())
                    RefreshActorOnMap();

                if (template.IsAura)
                {
                    ushort bonesId = template.AuraBones;

                    if (template.Id == (byte)EmotesEnum.PowerAura)
                        bonesId = (ushort)(this.Level >= 100 && this.Level != 200 ? 169 : 170);

                    this.Look.AddAura(bonesId);
                    this.RefreshActorOnMap();
                }
                else
                {
                    this.SendMap(new EmotePlayMessage(id, 0, this.Id, this.Client.Account.Id));
                }
            }

        }
        public void RandomTalkEmote()
        {
            byte[] talkEmotes = new byte[] { 49, 66, 9, 2, 10, 88 };
            PlayEmote(talkEmotes.Random());
        }
        public bool LearnEmote(byte id)
        {
            if (!Record.KnownEmotes.Contains(id))
            {
                Record.KnownEmotes.Add(id);
                Client.Send(new EmoteAddMessage(id));
                return true;
            }
            else
            {
                this.Reply("Vous connaissez déja cette émote.");
                return false;
            }
        }
        public bool HasReachObjective(short id)
        {
            return Record.DoneObjectives.Contains(id);
        }
        public void ReachObjective(short id)
        {
            if (!Record.DoneObjectives.Contains(id))
            {
                Record.DoneObjectives.Add(id);
                this.Reply("Nouvel objectif atteint.");
            }
        }
        public bool OnFightEnded(bool winner, FightTypeEnum type)
        {
            this.Inventory.DecrementEtherals();

            if (type == FightTypeEnum.FIGHT_TYPE_PVP_ARENA)
            {
                this.Teleport(PreviousRoleplayMapId.Value);
                PreviousRoleplayMapId = null;
                return true;
            }
            else if (winner && type == FightTypeEnum.FIGHT_TYPE_PvM)
            {
                EndFightActionRecord endFightAction = EndFightActionRecord.GetEndFightAction(Map.Id);
                if (endFightAction != null)
                {
                    this.Teleport(endFightAction.TeleportMapId, endFightAction.TeleportCellId);
                    return true;
                }
            }
            return false;
        }
        public bool RemoveEmote(byte id)
        {
            if (Record.KnownEmotes.Contains(id))
            {
                Record.KnownEmotes.Remove(id);
                Client.Send(new EmoteRemoveMessage(id));
                return true;
            }
            else
            {
                this.Reply("Impossible de retirer l'émote.");
                return false;
            }
        }
        public void RefreshEmotes()
        {
            Client.Send(new EmoteListMessage(Record.KnownEmotes.ToArray()));
        }
        public void OnEnterMap()
        {
            this.ChangeMap = false;
            this.UpdateServerExperience(Map.SubArea.ExperienceRate);

            if (this.Busy)
                this.LeaveDialog();

            if (!Fighting) // Teleport + Fight
            {
                lock (this.Map.Instance)
                {
                    this.Map.Instance.AddEntity(this);

                    this.Map.Instance.MapComplementary(Client);
                    this.Map.Instance.MapFightCount(Client);

                    foreach (Character current in this.Map.Instance.GetEntities<Character>())
                    {
                        if (current.IsMoving)
                        {
                            Client.Send(new GameMapMovementMessage(current.MovementKeys, current.Id));
                            Client.Send(new BasicNoOperationMessage());
                        }
                    }

                    Client.Send(new BasicNoOperationMessage());
                    Client.Send(new BasicTimeMessage(DateTime.Now.DateTimeToUnixTimestamp(), 1));
                }
            }
            if (HasParty())
            {
                Party.UpdateMember(this);
            }

        }
        public void RefreshGuild()
        {
            if (HasGuild)
            {

                Guild = GuildProvider.Instance.GetGuild(Record.GuildId);

                if (GuildMember == null || Guild == null)
                {
                    RemoveHumanOption<CharacterHumanOptionGuild>();
                    Record.GuildId = 0;
                    return;
                }
                GuildMember.OnConnected(this);
                SendGuildMembership();

                if (Guild.Record.Motd != null && Guild.Record.Motd.Content != null)
                {
                    Client.Send(new GuildMotdMessage(Guild.Record.Motd.Content, Guild.Record.Motd.Timestamp,
                        Guild.Record.Motd.MemberId, Guild.Record.Motd.MemberName));
                }
            }
        }
        public void SendGuildMembership()
        {
            Client.Send(new GuildMembershipMessage(Guild.Record.GetGuildInformations(), GuildMember.Record.Rights, true));
        }
        public void CreateContext(GameContextEnum context)
        {
            if (Context.HasValue)
            {
                DestroyContext();
            }

            Client.Send(new GameContextCreateMessage((sbyte)context));
            m_context = context;
        }
        public void DestroyContext()
        {
            Client.Send(new GameContextDestroyMessage());
            this.m_context = null;
        }
        public void UpdateServerExperience(int rate)
        {
            ushort percent = (ushort)(100 * (rate + ExpMultiplicator));
            Client.Send(new ServerExperienceModificatorMessage(percent));
        }
        public void TextInformation(TextInformationTypeEnum msgType, ushort msgId, params object[] parameters)
        {
            Client.Send(new TextInformationMessage((sbyte)msgType, msgId,
                (from entry in parameters
                 select entry.ToString()).ToArray()));

        }
        /// <summary>
        /// This is a wtf part, im not able to fix this weird bug (Module stop loading, ContextCreateMessage not received)
        /// </summary>
        public void SafeConnection()
        {
            ActionTimer timer = new ActionTimer(10000, CreateContextForced, false);
            timer.Start();
        }
        void CreateContextForced()
        {
            if (!this.Context.HasValue)
            {
                Logger.Write<Character>("Context Creation is Forced for " + Name, ConsoleColor.Green);
                ContextHandler.HandleCreateContextRequest(null, Client);
            }
        }
        public void OnConnected()
        {
            this.Client.Send(new AlmanachCalendarDateMessage(1));
            this.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 89, new string[0]);
            this.Reply(WorldConfiguration.Instance.WelcomeMessage, Color.BlueViolet);

            foreach (var notifRecord in NotificationRecord.GetConnectionNotifications(Client.Account.Id))
            {
                this.Reply(notifRecord.Notification);
                notifRecord.RemoveElement();
            }
            if (Inventory.HasMountEquiped)
            {
                Client.Send(new MountSetMessage(Inventory.Mount.GetMountClientData()));
            }
        }
        public void OpenPopup(byte lockDuration, string author, string content)
        {
            Client.Send(new PopupWarningMessage(lockDuration, author, content));
        }
        public void SpawnPoint(bool needComplementary = false)
        {
            TeleportZaap(Record.SpawnPointMapId, needComplementary);
        }
        /// <summary>
        /// Teleporte a une carte possédant un zaap
        /// </summary>
        /// <param name="mapId"></param>
        public void TeleportZaap(int mapId, bool needComplementary = false)
        {
            if (mapId != -1)
            {
                MapRecord destinationMap = MapRecord.GetMap(mapId);
                if (destinationMap.HasZaap())
                    Teleport(mapId, ZaapDialog.GetTeleporterCell(destinationMap, destinationMap.Zaap), needComplementary);
                else
                {
                    this.ReplyError("No zaap at here, aborting teleportation.");
                    return;
                }
            }
            else
            {
                this.Teleport(WorldConfiguration.Instance.StartMapId,
                    (WorldConfiguration.Instance.StartCellId), needComplementary);
            }
            Reply("Vous avez été téléporté.");
        }
        public void Reply(object value, Color color, bool bold = false, bool underline = false)
        {
            value = ApplyPolice(value, bold, underline);
            Client.Send(new TextInformationMessage(0, 0, new string[] { string.Format("<font color=\"#{0}\">{1}</font>", color.ToArgb().ToString("X"), value) }));
        }
        object ApplyPolice(object value, bool bold, bool underline)
        {
            if (bold)
                value = "<b>" + value + "</b>";
            if (underline)
                value = "<u>" + value + "</u>";
            return value;
        }
        public void Reply(object value, bool bold = false, bool underline = false)
        {
            value = ApplyPolice(value, bold, underline);
            Client.Send(new TextInformationMessage(0, 0, new string[] { value.ToString() }));
        }
        public void ReplyError(object value)
        {
            Reply(value, Color.DarkRed, false, false);
        }
        public void Notification(string message)
        {
            Client.Send(new NotificationByServerMessage(24, new string[] { message }, true));
        }
        public void Teleport(int mapId, ushort? cellid = null, bool needComplementary = false, bool force = false)
        {
            if (Fighting)
                return;
            if (ChangeMap)
                return;
            if (Busy && !force)
                return;

            if (Record.MapId != mapId)
                ChangeMap = true;

            var teleportMap = MapRecord.GetMap(mapId);

            if (teleportMap != null)
            {
                if (cellid < 0 || cellid > 560)
                    cellid = teleportMap.RandomWalkableCell();

                if (cellid.HasValue)
                {
                    if (!teleportMap.Walkable(cellid.Value))
                    {
                        cellid = teleportMap.RandomWalkableCell();
                    }
                }
                else
                {
                    if (!teleportMap.Walkable(this.CellId))
                    {
                        this.CellId = teleportMap.RandomWalkableCell();
                    }
                }

                if (Map != null && Map.Id == mapId && !needComplementary)
                {
                    if (cellid != null)
                    {
                        SendMap(new TeleportOnSameMapMessage(Id, (ushort)cellid));
                        Record.CellId = cellid.Value;
                    }
                    else
                        SendMap(new TeleportOnSameMapMessage(Id, (ushort)this.Record.CellId));
                    this.MovementKeys = null;
                    this.IsMoving = false;
                    return;
                }
                if (Map != null)
                    Map.Instance.RemoveEntity(this);

                this.MovementKeys = null;
                this.IsMoving = false;
                CurrentMapMessage(mapId);


                if (cellid != null)
                    this.Record.CellId = cellid.Value;
                this.Record.MapId = mapId;
            }
            else
            {
                Client.Character.ReplyError("The map dosent exist...");
            }
        }
        public void AddMinationExperience(ulong experienceFightDelta)
        {
            foreach (var item in Inventory.GetEquipedMinationItems())
            {
                var effect = item.FirstEffect<EffectMinationLevel>();

                if (effect != null)
                {
                    var level = effect.Level;
                    effect.AddExperience(experienceFightDelta);

                    if (level != effect.Level)
                    {
                        OnMinationLevelUp(item.FirstEffect<EffectMination>(), effect.Level);
                    }
                    Inventory.OnItemModified(item);
                }
            }

        }
        public void CurrentMapMessage(int mapId)
        {
            Client.Send(new CurrentMapMessage(mapId, WorldConfiguration.Instance.MapKey));

        }
        public void MoveOnMap(short[] cells)
        {
            if (!Busy)
            {
                ushort clientCellId = (ushort)PathParser.ReadCell(cells.First());

                if (clientCellId == CellId)
                {

                    if (Look.RemoveAura())
                        RefreshActorOnMap();
                    sbyte direction = PathParser.GetDirection(cells.Last());
                    ushort cellid = (ushort)PathParser.ReadCell(cells.Last());

                    this.Record.Direction = direction;
                    this.MovedCell = cellid;
                    this.IsMoving = true;
                    this.MovementKeys = cells;
                    this.SendMap(new GameMapMovementMessage(cells, this.Id));
                }
                else
                {
                    this.NoMove();
                }
            }
            else
            {
                this.NoMove();
            }
        }
        public void NoMove()
        {
            this.Client.Send(new GameMapNoMovementMessage((short)Point.X, (short)Point.Y));
        }
        public void OpenUIByObject(sbyte id, uint itemUid)
        {
            Client.Send(new ClientUIOpenedByObjectMessage(id, itemUid));
        }
        public void EndMove()
        {
            this.Record.CellId = this.MovedCell;
            this.MovedCell = 0;
            this.IsMoving = false;
            this.MovementKeys = null;

            DropItem item = Map.Instance.GetDroppedItem(this.Record.CellId);

            if (item != null)
            {
                item.OnPickUp(this);
            }

        }

        public void LeaveDialog()
        {
            if (this.Dialog == null && !this.IsInRequest())
            {
                this.ReplyError("Unknown dialog...");
                return;
            }
            else
            {
                if (this.IsInRequest())
                {
                    this.CancelRequest();
                }
                if (this.Dialog != null)
                    this.Dialog.Close();
            }
        }
        public void OpenPaddock()
        {
            this.OpenDialog(new MountStableExchange(this));
        }
        public void OpenBank()
        {
            this.OpenDialog(new BankExchange(this, BankItemRecord.GetBankItems(this.Client.Account.Id)));
        }
        public void OpenGuildCreationPanel()
        {
            this.OpenDialog(new GuildCreationDialog(this));
        }
        public void OpenNpcShop(Npc npc, ItemRecord[] itemToSell, ushort tokenId, bool priceLevel)
        {
            this.OpenDialog(new NpcShopExchange(this, npc, itemToSell, tokenId, priceLevel));
        }
        public void TalkToNpc(Npc npc, NpcActionRecord action)
        {
            this.OpenDialog(new NpcTalkDialog(this, npc, action));
        }
        public void OpenZaap(MapInteractiveElementSkill skill)
        {
            this.OpenDialog(new ZaapDialog(this, skill));
        }
        public void OpenZaapi(MapInteractiveElementSkill skill)
        {
            this.OpenDialog(new ZaapiDialog(this, skill));
        }
        public void OpenBidhouseSell(Npc npc, BidShopRecord bidshop, bool force)
        {
            this.OpenDialog(new SellExchange(this, npc, bidshop), force);
        }
        public void OpenBidhouseBuy(Npc npc, BidShopRecord bidshop, bool force)
        {
            this.OpenDialog(new BuyExchange(this, npc, bidshop), force);
        }
        public void OpenCraftPanel(uint skillId, JobsTypeEnum jobType)
        {
            this.OpenDialog(new CraftExchange(this, skillId, jobType));
        }
        public void OpenSmithMagicPanel(uint skillId, JobsTypeEnum jobType)
        {
            this.OpenDialog(new SmithMagicExchange(this, skillId, jobType));
        }
        public void ReadDocument(ushort documentId)
        {
            this.OpenDialog(new BookDialog(this, documentId));
        }

        public bool AddKamas(int value)
        {
            if (value <= int.MaxValue)
            {
                if (Record.Kamas + value >= Inventory.MaxKamas)
                {
                    Record.Kamas = Inventory.MaxKamas;
                }
                else
                    Record.Kamas += value;
                Inventory.RefreshKamas();
                return true;
            }
            return false;
        }
        public void RegisterArena()
        {
            this.ArenaMember = ArenaProvider.Instance.Register(this);
            this.ArenaMember.UpdateStep(true, PvpArenaStepEnum.ARENA_STEP_REGISTRED);
        }
        public void OnItemUnequipedArena()
        {
            TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 298);
        }
        public void UnregisterArena()
        {
            if (InArena)
            {
                ArenaProvider.Instance.Unregister(this);
                this.ArenaMember.UpdateStep(false, PvpArenaStepEnum.ARENA_STEP_UNREGISTER);
                this.ArenaMember = null;
            }
            else
            {
                Logger.Write<Character>("Try to unregister arena while not in arena...", ConsoleColor.Red);
            }
        }
        public void AnwserArena(bool accept)
        {
            if (InArena)
            {
                ArenaMember.Anwser(accept);
            }
            else
            {
                Logger.Write<Character>("Unable to answer arena while not in arena...", ConsoleColor.Red);
            }
        }
        public void RefreshArenaInfos()
        {
            Client.Send(new GameRolePlayArenaUpdatePlayerInfosMessage(Record.ArenaRank.GetArenaRankInfos()));
        }
        public PlayerStatus GetPlayerStatus()
        {
            return new PlayerStatus(0);
        }
        public CharacterJob GetJob(JobsTypeEnum job)
        {
            return Record.Jobs.Find(x => x.JobType == job);
        }
        public void RefreshJobs()
        {
            Client.Send(new JobCrafterDirectorySettingsMessage(Record.Jobs.ConvertAll<JobCrafterDirectorySettings>(x => x.GetDirectorySettings()).ToArray()));
            Client.Send(new JobDescriptionMessage(Record.Jobs.ConvertAll<JobDescription>(x => x.GetJobDescription()).ToArray()));
            Client.Send(new JobExperienceMultiUpdateMessage(Record.Jobs.ConvertAll<JobExperience>(x => x.GetJobExperience()).ToArray()));
        }
        public void AddJobExp(JobsTypeEnum jobType, ulong amount)
        {
            CharacterJob job = GetJob(jobType);
            ushort currentLevel = job.Level;
            ulong highest = ExperienceRecord.HighestExperience().Job;

            if (job.Experience + amount > highest)
                job.Experience = highest;
            else
                job.Experience += amount;

            Client.Send(new JobExperienceUpdateMessage(job.GetJobExperience()));

            if (currentLevel != job.Level)
            {
                Client.Send(new JobLevelUpMessage((byte)job.Level, job.GetJobDescription()));
                this.SkillsAllowed = SkillsProvider.Instance.GetAllowedSkills(this).ToArray();
            }

        }
        public void SetSide(AlignmentSideEnum side)
        {
            this.Record.Alignment.Side = side;
            this.RefreshStats();
            this.RefreshActorOnMap();
        }
        public void AddHonor(ushort amount)
        {
            ushort highest = (ushort)ExperienceRecord.HighestHonorExperience().Honor;

            if (Record.Alignment.Honor + amount > highest)
                Record.Alignment.Honor = highest;
            else
                Record.Alignment.Honor += amount;


            RefreshActorOnMap();
            RefreshStats();
        }
        public void RemoveHonor(ushort amount)
        {
            if (Record.Alignment.Honor - amount < 0)
            {
                Record.Alignment.Honor = 0;
            }
            else
            {
                Record.Alignment.Honor -= amount;
            }
            RefreshActorOnMap();
            RefreshStats();
        }
        public AggressableStatusEnum TogglePvP()
        {
            if (Record.Alignment.Agressable == AggressableStatusEnum.NON_AGGRESSABLE)
            {
                Record.Alignment.Agressable = AggressableStatusEnum.PvP_ENABLED_AGGRESSABLE;
            }
            else if (Record.Alignment.Agressable == AggressableStatusEnum.PvP_ENABLED_AGGRESSABLE)
            {
                Record.Alignment.Agressable = AggressableStatusEnum.NON_AGGRESSABLE;
            }


            RefreshActorOnMap();
            RefreshStats();

            return Record.Alignment.Agressable;
        }
        public void RefreshAlignment()
        {
            Client.Send(new AlignmentRankUpdateMessage(Record.Alignment.Value, false));
        }
        public bool RemoveKamas(int value)
        {
            if (Record.Kamas >= value)
            {
                Record.Kamas -= value;
                Inventory.RefreshKamas();
                return true;
            }
            else
            {
                return false;
            }

        }
        private void AddHumanOption(CharacterHumanOption option)
        {
            Record.HumanOptions.Add(option);
            RefreshActorOnMap();
        }
        private void RemoveHumanOption(CharacterHumanOption option)
        {
            Record.HumanOptions.Remove(option);
            RefreshActorOnMap();
        }
        public void RemoveHumanOption<T>() where T : CharacterHumanOption
        {
            Record.HumanOptions.RemoveAll(x => x is T);
            RefreshActorOnMap();
        }
        private T GetFirstHumanOption<T>() where T : CharacterHumanOption
        {
            return Record.HumanOptions.OfType<T>().ToArray().FirstOrDefault();
        }

        public void SendTitlesAndOrnamentsList()
        {
            Client.Send(new TitlesAndOrnamentsListMessage(Record.KnownTitles.ToArray(), Record.KnownOrnaments.ToArray(),
                (ushort)(ActiveTitle != null ? ActiveTitle.TitleId : 0), (ushort)(ActiveOrnament != null ? ActiveOrnament.OrnamentId : 0)));
        }

        public bool LearnOrnament(ushort id, bool send)
        {
            if (!Record.KnownOrnaments.Contains(id))
            {
                Record.KnownOrnaments.Add(id);
                if (send)
                    Client.Send(new OrnamentGainedMessage((short)id));
                return true;
            }
            else
            {
                return false;
            }

        }
        public void UseItem(uint uid, bool send)
        {
            var item = this.Inventory.GetItem(uid);

            if (item != null)
            {
                if (ItemUseProvider.Handle(Client.Character, item))
                    this.Inventory.RemoveItem(item.UId, 1);

                if (send)
                {
                    this.RefreshStats();
                }
            }


        }
        public bool ForgetOrnament(ushort id)
        {
            if (Record.KnownOrnaments.Contains(id))
            {
                Record.KnownOrnaments.Remove(id);
                if (ActiveOrnament.OrnamentId == id)
                {
                    RemoveHumanOption<CharacterHumanOptionOrnament>();
                    RefreshActorOnMap();

                }
                return true;
            }
            return false;

        }
        public bool Mute(int seconds)
        {
            if (!Record.Muted)
            {
                Record.Muted = true;

                ActionTimer timer = new ActionTimer(seconds * 1000, new Action(() =>
                   {
                       Record.Muted = false;
                   }), false);
                timer.Start();

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasOrnament(ushort id)
        {
            return Record.KnownOrnaments.Contains(id) ? true : false;

        }
        public void OpenRequestBox(RequestBox box)
        {
            box.Source.RequestBox = box;
            this.RequestBox = box;
            box.Open();
        }
        public void OnExchangeError(ExchangeErrorEnum error)
        {
            this.Client.Send(new ExchangeErrorMessage((sbyte)error));
        }
        public void OnPartyJoinError(PartyJoinErrorEnum error, int partyId = 0)
        {
            this.Client.Send(new PartyCannotJoinErrorMessage((uint)partyId, (sbyte)error));
        }
        /// <summary>
        /// Conditions & EnterParty (Stump)
        /// </summary>
        /// <param name="character"></param>
        public void InviteParty(Character character)
        {
            if (!this.HasParty())
            {
                AbstractParty party = PartyProvider.Instance.CreateParty(this);
                PartyProvider.Instance.Parties.Add(party);
                party.Create(this, character);
            }
            else
            {
                if (!Party.IsFull)
                    AbstractParty.SendPartyInvitationMessage(character, this, this.Party);
            }
        }
        public bool SetOrnament(ushort id)
        {
            if (id == 0)
            {
                RemoveHumanOption<CharacterHumanOptionOrnament>();
                Client.Send(new OrnamentSelectedMessage(id));
                return true;
            }

            if (HasOrnament(id))
            {
                if (ActiveOrnament != null && ActiveOrnament.OrnamentId == id)
                    return false;

                RemoveHumanOption<CharacterHumanOptionOrnament>();
                AddHumanOption(new CharacterHumanOptionOrnament(id));
                Client.Send(new OrnamentSelectedMessage(id));
                return true;

            }
            return false;
        }

        public bool LearnTitle(ushort id)
        {
            if (!Record.KnownTitles.Contains(id))
            {
                Record.KnownTitles.Add(id);
                Client.Send(new TitleGainedMessage(id));
                return true;

            }
            return false;
        }

        public bool ForgetTitle(ushort id)
        {

            if (Record.KnownTitles.Contains(id))
            {
                Record.KnownTitles.Remove(id);
                Client.Send(new TitleLostMessage(id));

                return true;

            }
            return false;

        }
        public bool HasTitle(ushort id)
        {
            return Record.KnownTitles.Contains(id) ? true : false;
        }

        public bool SelectTitle(ushort id)
        {
            if (id == 0)
            {
                RemoveHumanOption<CharacterHumanOptionTitle>();
                Client.Send(new TitleSelectedMessage(id));
                return true;

            }
            if (HasTitle(id))
            {
                if (ActiveTitle != null && ActiveTitle.TitleId == id)
                    return false;

                RemoveHumanOption<CharacterHumanOptionTitle>();
                AddHumanOption(new CharacterHumanOptionTitle(id, string.Empty));
                Client.Send(new TitleSelectedMessage(id));
                return true;

            }
            return false;

        }
        public void PlayCinematic(ushort id)
        {
            Client.Send(new CinematicMessage(id));
        }
        public void OnContextCreated()
        {
            if (this.New && WorldConfiguration.Instance.PlayDefaultCinematic)
            {
                PlayCinematic(10);
                New = false;
            }
        }
        public override GameRolePlayActorInformations GetActorInformations()
        {
            return new GameRolePlayCharacterInformations(Id, Look.ToEntityLook(),
                new EntityDispositionInformations((short)CellId, (sbyte)Direction),
                Name, new HumanInformations(new ActorRestrictionsInformations(), Record.Sex,
                    Record.HumanOptions.ConvertAll<HumanOption>(x => x.GetHumanOption()).ToArray())
                , Client.Account.Id, Record.Alignment.GetActorAlignmentInformations());
        }
        public CharacterMinimalInformations GetCharacterMinimalInformations()
        {
            return new CharacterMinimalInformations((ulong)Id, Name, (byte)Level);
        }
        public void OnChatError(ChatErrorEnum error)
        {
            Client.Send(new ChatErrorMessage((sbyte)error));
        }
        public void SetRestrictions()
        {
            Client.Send(new SetCharacterRestrictionsMessage(this.Id,
                new ActorRestrictionsInformations(false, false, false, false, false, false, false, false, false, false, false,
                    false, false, true, true, false, false, false, false, false, false)));
        }
        public void RejoinMap(FightTypeEnum fightType, bool winner, bool spawnJoin)
        {
            DestroyContext();
            CreateContext(GameContextEnum.ROLE_PLAY);
            this.RefreshStats();
            this.Fighter = null;
            this.FighterMaster = null;
            if (spawnJoin && !winner && Client.Account.Role != ServerRoleEnum.Fondator)
            {
                SpawnPoint(true);
            }
            else
            {
                if (!OnFightEnded(winner, fightType))
                    CurrentMapMessage(Record.MapId);
            }

        }
        public void OnDisconnected()
        {
            if (Dialog != null)
                Dialog.Close();
            if (IsInRequest())
                CancelRequest();

            if (InArena)
                UnregisterArena();

            if (Fighting)
                FighterMaster.OnDisconnected();

            if (HasParty())
                Party.Leave(this);

            if (HasGuild)
                GuildMember.OnDisconnected();

            DeclineAllPartyInvitations();

            Look.RemoveAura();
            if (Map != null)
                Map.Instance.RemoveEntity(this);

            Record.UpdateElement();

        }

        private void DeclineAllPartyInvitations()
        {
            foreach (var party in new List<AbstractParty>(GuestedParties))
            {
                party.RefuseInvation(this);
            }
        }


        public void OnGuildCreated(GuildCreationResultEnum result)
        {
            Client.Send(new GuildCreationResultMessage((sbyte)result));
            Dialog.Close();
        }


        public override string ToString()
        {
            return "Character: (" + Name + ")";
        }

        public void OnGuildJoined(GuildInstance guild, GuildMemberInstance member)
        {
            this.Guild = guild;
            this.Record.GuildId = Guild.Id;
            this.AddHumanOption(new CharacterHumanOptionGuild(Guild.Record.GetGuildInformations()));
            Client.Send(new GuildJoinedMessage(Guild.Record.GetGuildInformations(),
                member.Record.Rights, true));
        }
        public void OnMinationLevelUp(EffectMination minationEffect, ushort newLevel)
        {
            OpenPopup(0, "Félicitation", "Votre Pokéfus " + minationEffect.MonsterName + " vient de passer niveau " + newLevel + "!");
        }

    }

}
