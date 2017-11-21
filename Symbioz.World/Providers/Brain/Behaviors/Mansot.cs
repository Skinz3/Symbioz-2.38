using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Providers.Fights.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("Mansot")]
    public class Mansot : Behavior
    {
        public Mansot(BrainFighter fighter)
            : base(fighter)
        {
            foreach (var ally in Fighter.Team.GetFighters())
            {
                ally.BeforeDeadEvt += ally_OnDeadEvt;
            }
        }

        void ally_OnDeadEvt(Fighter obj)
        {
          
        }
    }
}
