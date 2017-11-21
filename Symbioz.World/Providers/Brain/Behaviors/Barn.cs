using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;
using Symbioz.World.Models.Fights;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("Barn")]
    public class Barn : Behavior
    {
        public const ushort SummonedMonsterId = 671;

        private MonsterRecord SummonedTemplate
        {
            get;
            set;
        }
        private AsyncRandom GradeRandomizer
        {
            get;
            set;
        }
        public Barn(BrainFighter fighter)
            : base(fighter)
        {
            this.GradeRandomizer = new AsyncRandom();
            fighter.Fight.FightStartEvt += Fight_FightStart;

            fighter.OnDamageTaken += fighter_OnDamageTaken;

            this.SummonedTemplate = MonsterRecord.GetMonster(SummonedMonsterId);
        }

        void fighter_OnDamageTaken(Fighter fighter, Damage obj)
        {
            List<Fighter> fighters = Fighter.Fight.GetAllFighters();
            fighters.Remove(Fighter);
            fighters.Random().Heal(Fighter, obj.Delta);
        }

        void Fight_FightStart(Fight fight)
        {
            CharacterFighter master = this.Fighter.OposedTeam().GetFighters<CharacterFighter>().FirstOrDefault();

            Fighter.AfterSlideEvt += Fighter_OnSlideEvt;

        }

        void Fighter_OnSlideEvt(Fighter target, Fighter source, short startCellId, short endCellId)
        {
            SummonedFighter summon = CreateSummoned(source);

            Fighter.Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);
            Fighter.Fight.AddSummon(summon);
            Fighter.Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);
        }

        private SummonedFighter CreateSummoned(Fighter master)
        {
            return new SummonedFighter(SummonedTemplate, (sbyte)GradeRandomizer.Next(1, 5), master, master.Team, this.Fighter.Fight.RandomFreeCell());
        }

    }
}
