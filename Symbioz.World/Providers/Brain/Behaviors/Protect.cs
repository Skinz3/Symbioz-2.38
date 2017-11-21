using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("Protect")]
    public class Protect : Behavior
    {
        public Protect(BrainFighter fighter)
            : base(fighter)
        {
            fighter.BeforeDeadEvt += fighter_OnDeadEvt;
            fighter.Fight.OnFighters<CharacterFighter>(x => x.Character.Notification("Protegez " + Fighter.Name + "!"));
        }

        void fighter_OnDeadEvt(Fighter fighter)
        {
            foreach (var ally in Fighter.Team.GetFighters())
            {
                ally.Die(this.Fighter);
            }
            Fighter.Fight.CheckDeads();
        }
    }
}
