using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("Pusher")]
    public class Pusher : Behavior
    {
        public const short PushAmount = 2;

        public Pusher(BrainFighter fighter)
            : base(fighter)
        {
            this.Fighter.OnTurnStartEvt += Fighter_OnTurnStartEvt;
        }

        void Fighter_OnTurnStartEvt(Fighter obj)
        {
            foreach (var fighter in Fighter.OposedTeam().GetFighters())
            {
                fighter.Abilities.PushBack(Fighter, PushAmount, Fighter.Point);
            }
        }
    }
}
