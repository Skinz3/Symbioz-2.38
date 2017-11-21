using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("HellMina")]
    public class HellMina : Behavior
    {
        const short SlideHealAmount = 1000;

        public HellMina(BrainFighter fighter)
            : base(fighter)
        {
            fighter.AfterSlideEvt += fighter_OnSlideEvt;
        }

        void fighter_OnSlideEvt(Fighter fighter,Fighter source, short startCellId, short endCellId)
        {
            Fighter.Heal(Fighter, SlideHealAmount);

            DirectionsEnum direction = source.Point.OrientationTo(Fighter.Point);

            short step = (short)(source.Point.DistanceTo(Fighter.Point) - 1);
            short destinationCellId = source.Point.GetCellInDirection(direction, step).CellId;

            source.Slide(Fighter, destinationCellId);

         //   Fighter.Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_MOVE);
        }

    }
}
