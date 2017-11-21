using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.Protocol.Enums;

namespace Symbioz.World.Providers.Brain.Behaviors.Salbatroces
{
    /// <summary>
    /// Ne pas soigner pendant 5 tours
    /// </summary>
    [Behavior("Tabacille")]
    public class Tabacille : SalbatroceBehavior
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
        public Tabacille(BrainFighter fighter) : base(fighter)
        {
            fighter.Fight.FightStartEvt += Fight_FightStartEvt;
            this.Fighter.OnTurnStartEvt += Fighter_OnTurnStartEvt;

            foreach (var enemy in fighter.OposedTeam().GetFighters())
            {
                enemy.OnHealEvt += Enemy_OnHealEvt;
            }
            TurnCount = 0;
        }

        private void Enemy_OnHealEvt(Fighter target, Fighter source)
        {
            TurnCount = 0;
        }

        private void Fight_FightStartEvt(Models.Fights.Fight obj)
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
