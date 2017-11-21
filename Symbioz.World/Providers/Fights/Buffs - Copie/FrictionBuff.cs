using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public class FrictionBuff : Buff
    {
        public override FighterEvent EventType
        {
            get
            {
                return FighterEvent.AfterAttacked;
            }
        }
        public FrictionBuff(Fighter source, Fighter target, short delta,
         EffectInstance effect, ushort spellId)
            : base(source, target, delta, effect, spellId)
        {

        }
        public override void Apply()
        {
            base.Apply();
        }

        public override void Dispell()
        {

        }
        public override bool OnTriggered(object arg1, object arg2, object arg3)
        {
            Damage damage = arg1 as Damage;

            if (damage.ElementType != EffectElementType.Direct)
            {
                MapPoint point = damage.Source.Point;


                if (point.IsInLine(Target.Point))
                {
                    Target.Abilities.PullForward(Source, Delta, Source.Point);
                    //DirectionsEnum direction = Target.Point.OrientationTo(point, false);

                    //MapPoint nearestCellInDirection = Target.Point.GetNearestCellInDirection(direction);

                    //if (Source.Fight.IsCellFree(nearestCellInDirection.CellId))
                    //{
                    //    Target.Slide(Source, new short[] { nearestCellInDirection.CellId });
                    //}
                }
            }
            return false;

        }
     
    }
}
