using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("Puller")]
    public class Puller : Behavior
    {
        public const short PullAmount = 3;

        public Puller(BrainFighter fighter)
            : base(fighter)
        {
            this.Fighter.OnTurnStartEvt += Fighter_OnTurnStartEvt;
        }

        void Fighter_OnTurnStartEvt(Fighter obj)
        {
            foreach (var fighter in Fighter.OposedTeam().GetFighters())
            {
                fighter.Abilities.PullForward(Fighter, PullAmount, Fighter.Point);
            }
        }
    }
}
