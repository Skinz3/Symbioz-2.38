using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Maps;
using Symbioz.Protocol.Enums;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    /// <summary>
    /// Quand on frappe la mérulette, celle-ci téléporte tous les joueurs a leur position initiale.
    /// </summary>
    [Behavior("Mérulette")]
    public class Merulette : Behavior
    {
        public Merulette(BrainFighter fighter) : base(fighter)
        {
            fighter.OnDamageTaken += Fighter_OnDamageTaken;
        }

        private void Fighter_OnDamageTaken(Fighter arg1, Damage arg2)
        {
            Fighter.Fight.TeleportFightersToInitialPosition(Fighter);
        }
    }
}
