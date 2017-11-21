using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Fighters
{
    /// <summary>
    /// Semble fonctionner, a verifier et a clean
    /// </summary>
    public class ControlableMonsterFighter : PlayableFighter, IMonster, ISummon<CharacterFighter>
    {
        private CharacterSpell[] Spells
        {
            get;
            set;
        }
        private ShortcutSpell[] Shortcuts
        {
            get;
            set;
        }
        public MonsterRecord Template
        {
            get;
            private set;
        }
        public sbyte GradeId
        {
            get;
            private set;
        }
        public MonsterGrade Grade
        {
            get
            {
                return Template.GetGrade(GradeId);
            }
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
        public CharacterFighter Owner
        {
            get;
            set;
        }
        public short SummonCellId
        {
            get;
            set;
        }
        private bool ContextSwitched
        {
            get
            {
                return Owner.Character.Fighter == this;
            }
        }
        public ControlableMonsterFighter(FightTeam team, MonsterRecord template, sbyte gradeId, CharacterFighter owner,
            short summonCellId)
            : base(team, 0)
        {
            this.Template = template;
            this.GradeId = gradeId;
            this.Owner = owner;
            this.Owner.OnLeavePreFightEvt += Owner_OnLeavePreFight;
            this.Owner.OnTurnEndEvt += Owner_TurnEnd;
            this.OnTurnEndEvt += ControlableMonsterFighter_TurnEnd;
            this.AfterDeadEvt += ControlableMonsterFighter_OnDead;
            this.SummonCellId = summonCellId;
            this.Spells = Array.ConvertAll(template.SpellRecords.ToArray(), x => new CharacterSpell(x.Id, x.GetLastLevelGrade()));

            this.Shortcuts = new ShortcutSpell[Spells.Length];

            for (int i = 0; i < Spells.Length; i++)
            {
                Shortcuts[i] = new ShortcutSpell((sbyte)i, Spells[i].SpellId);
            }
            this.IsReady = true;

        }

        void Owner_OnLeavePreFight()
        {
            Team.RemoveFighter(this);
        }

        void Fight_FightStart(Fight fight)
        {
            if (GetNextControlable(0) == this)
            {
                this.Owner.Character.SwapFighter(this);
                SwitchContext();
            }
        }
        void ControlableMonsterFighter_OnDead(Fighter fighter,bool recursiveCall)
        {
            Owner.OnTurnEndEvt -= Owner_TurnEnd;

            ControlableMonsterFighter controlable = GetNextControlable(1);

            if (controlable != null)
            {
                this.Owner.Character.SwapFighter(controlable);
            }
            else
            {
                this.Owner.Character.SwapFighterToMaster();
            }
        }

        void ControlableMonsterFighter_TurnEnd(Fighter fighter)
        {
            ControlableMonsterFighter nextControlable = GetNextControlable(1);

            if (nextControlable != null)
            {
                nextControlable.SwitchContext();
                this.Owner.Character.SwapFighter(nextControlable);
            }
            else
            {
                this.Owner.Character.SwapFighterToMaster();
            }
        }
        private ControlableMonsterFighter GetNextControlable(int o)
        {
            for (int i = Fight.TimeLine.Index + o; i < Fight.TimeLine.Fighters.Count; i++)
            {
                Fighter nextControlable = Fight.TimeLine.Fighters[i];

                if (nextControlable is ControlableMonsterFighter && ((ControlableMonsterFighter)nextControlable).Owner == Owner && nextControlable.Alive)
                {
                    return (ControlableMonsterFighter)nextControlable;
                }
                if (Owner == nextControlable)
                {
                    return null;
                }
            }
            for (int i = 0; i < Fight.TimeLine.Index; i++)
            {
                Fighter nextControlable = Fight.TimeLine.Fighters[i];

                if (nextControlable is ControlableMonsterFighter && ((ControlableMonsterFighter)nextControlable).Owner == Owner && nextControlable.Alive)
                {
                    return (ControlableMonsterFighter)nextControlable;
                }
                if (Owner == nextControlable)
                {
                    return null;
                }
            }
            return null;
        }
        void Owner_TurnEnd(Fighter fighter)
        {
            ControlableMonsterFighter nextControlable = GetNextControlable(1);

            if (nextControlable == this)
            {
                SwitchContext();
                this.Owner.Character.SwapFighter(this);
            }
        }
        public override void OnTurnStarted()
        {
            base.OnTurnStarted();
        }
        public void SwitchContext()
        {
            this.Owner.Character.Client.Send(new SlaveSwitchContextMessage(Owner.Id, this.Id, Array.ConvertAll(Spells, x => x.GetSpellItem())
                , Stats.GetCharacterCharacteristics(Owner.Character), Shortcuts));
        }
        public override GameFightFighterInformations GetFightFighterInformations()
        {
            return new GameFightMonsterInformations(Id, Look.ToEntityLook(), new EntityDispositionInformations((short)CellId, (sbyte)Direction),
               Team.Id, 0, Alive, Stats.GetFightMinimalStats(), new ushort[0], Template.Id, GradeId);
        }

        public override FightTeamMemberInformations GetFightTeamMemberInformations()
        {
            return new FightTeamMemberMonsterInformations(Id, Template.Id, GradeId);
        }

        public override void Send(Message message)
        {
            Owner.Send(message);
        }
        public override CharacterSpell GetSpell(ushort spellId)
        {
            return Spells.FirstOrDefault(x => x.SpellId == spellId);
        }
        public override void Initialize()
        {
            this.Id = Fight.PopNextContextualId();
            this.Stats = new FighterStats(Grade, Owner.Character.Record.Stats.Power);
            this.Look = Template.Look.Clone();
            base.Initialize();
            this.FightStartCell = SummonCellId;
            this.CellId = SummonCellId;
            this.Direction = Owner.Point.OrientationTo(this.Point, false);
            this.Stats.InitializeSummon(Owner, false);
            this.Fight.FightStartEvt += Fight_FightStart;
        }

        public bool IsOwner(Fighter fighter)
        {
            return this.Owner == fighter;
        }
        public ushort MonsterId
        {
            get
            {
                return Template.Id;
            }
        }

        public override Character GetCharacterPlaying()
        {
            return this.Owner.GetCharacterPlaying();
        }

    }
}
