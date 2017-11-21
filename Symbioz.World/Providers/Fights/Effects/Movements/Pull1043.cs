using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Maps.Path;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_1043)]
    public class Pull1043 : SpellEffectHandler
    {
        public Pull1043(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            var direction = Source.Point.OrientationTo(CastPoint);

            MapPoint point = Source.Point;

            for (short i = 1; i < SpellLevel.MaxRange + 1; i++)
            {
                point = Source.Point.GetCellInDirection(direction, i);

                Fighter target = Fight.GetFighter(point.CellId);

                if (target != null)
                {
                    target.Slide(Source, Source.Point.GetCellInDirection(direction, 1).CellId);
                    break;
                }
            }

            return true;

        }
    }
}
