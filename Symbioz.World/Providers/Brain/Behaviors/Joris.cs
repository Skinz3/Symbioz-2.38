using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("Joris")]
    public class Joris : Behavior
    {
        const short MovementPointsDelta = 1;

        const short AllDamageBonusDelta = 25;

        const ushort SmileyId = 59;

        public Joris(BrainFighter fighter)
            : base(fighter)
        {
            this.Fighter.OnDamageTaken += Fighter_OnDamageTaken;
        }

        void Fighter_OnDamageTaken(Fighter fighter, Damage obj)
        {
            obj.Source.SwitchPosition(Fighter);
            this.Fighter.Stats.AllDamagesBonus.Context += AllDamageBonusDelta;
            this.AddPermanemtMovementPoints(MovementPointsDelta);
            this.Fighter.DisplaySmiley(SmileyId);
        }

        void AddPermanemtMovementPoints(short delta)
        {
            Fighter.Stats.MovementPoints.Context += delta;
            Fighter.Fight.PointsVariation(Fighter.Id, Fighter.Id, ActionsEnum.ACTION_CHARACTER_MOVEMENT_POINTS_WIN, delta);
        }

    }
}
