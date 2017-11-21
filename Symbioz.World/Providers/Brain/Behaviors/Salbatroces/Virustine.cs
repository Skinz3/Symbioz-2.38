using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors.Salbatroces
{
    /// <summary>
    /// Ne doit pas être poussé pendant 5 tour
    /// </summary>
    [Behavior("Virustine")]
    public class Virustine : SalbatroceBehavior
    {
        public const int TURN_COUNT = 5;

        public int TurnCount
        {
            get;
            private set;
        }
        private bool Invunlnerable
        {
            get
            {
                return Fighter.HasState(x => x.Invulnerable);
            }
        }
        public Virustine(BrainFighter fighter) : base(fighter)
        {
            fighter.Fight.FightStartEvt += Fight_FightStartEvt;
            fighter.AfterSlideEvt += Fighter_AfterSlideEvt;
            this.Fighter.OnTurnStartEvt += Fighter_OnTurnStartEvt;
            TurnCount = 0;
        }

        private void Fighter_AfterSlideEvt(Fighter target, Fighter source, short startCellId, short endCellId)
        {
            TurnCount = 0;
        }

        private void Fight_FightStartEvt(Fight obj)
        {
            Fighter.Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);
            this.MakeInvulnerable(this.Fighter, -1);
            Fighter.Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);
        }

        private void Fighter_OnTurnStartEvt(Fighter obj)
        {
            if (Invunlnerable)
            {
                TurnCount++;

                if (obj.Stats.MovementPoints.TotalInContext() != obj.Stats.MovementPoints.Total())
                {
                    TurnCount = 0;
                }
                if (TurnCount == TURN_COUNT)
                {
                    Fighter.Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_CHARACTER_DEATH);
                    Fighter.Die(Fighter);
                    Fighter.Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_CHARACTER_DEATH);
                }
            }

        }
    }
}
