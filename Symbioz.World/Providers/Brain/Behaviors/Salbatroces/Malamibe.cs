using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Fights;

namespace Symbioz.World.Providers.Brain.Behaviors.Salbatroces
{
    [Behavior("Malamibe")]
    public class Malamibe : Behavior
    {
        public const short HP_PER_PLAYER = 2000;

        public const int MAX_TURN_COUNT_ALIVE = 3;

        private int TurnCount
        {
            get;
            set;
        }
        public void OnSummoned()
        {
            foreach (var fighter in Fighter.OposedTeam().GetFighters())
            {
                fighter.AfterDeadEvt += Fighter_AfterDeadEvt;
                Fighter.AddLife(HP_PER_PLAYER, false);
            }
            foreach (var ally in Fighter.Team.GetFighters())
            {
                if (ally != this.Fighter) // Stack Overflow
                    ally.OnAttemptToInflictEvt += OnAllyInflicted;
            }
            Fighter.BeforeTakeDamagesEvt += Fighter_BeforeTakeDamagesEvt;
            Fighter.ShowFighter();
        }

        private void Fighter_BeforeTakeDamagesEvt(Fighter arg1, Models.Fights.Damages.Damage arg2)
        {
            if (arg2.Target == Fighter)
            {
                arg2.Delta /= 2;
            }
        }

        private void OnAllyInflicted(Fighter arg1, Models.Fights.Damages.Damage arg2)
        {
            Fighter.InflictDamages(arg2);
        }

        private void Fighter_AfterDeadEvt(Fighter obj,bool recursiveCall)
        {
            Fighter.SubLife(HP_PER_PLAYER);
        }

        public Malamibe(BrainFighter fighter) : base(fighter)
        {
            this.Fighter.OnTurnStartEvt += Fighter_OnTurnStartEvt;
        }


        private void Fighter_OnTurnStartEvt(Fighter obj)
        {
            TurnCount++;

            if (TurnCount == MAX_TURN_COUNT_ALIVE)
            {
                Fighter.OposedTeam().KillTeam();
            }
        }

    }
}
