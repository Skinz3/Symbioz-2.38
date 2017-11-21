using Symbioz.Protocol.Enums;
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
    [SpellEffectHandler(EffectsEnum.Effect_RepelsTo)]
    public class RepelsTo : SpellEffectHandler
    {
        public RepelsTo(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {
           
        }
        public override bool Apply(Fighter[] targets)
        {
            DirectionsEnum orientation = this.Source.Point.OrientationTo(CastPoint, true); //Source.Point=> this.TargetedPoint
            short adjCellId = this.Source.Point.GetCellInDirection(orientation, (short)1).CellId;

            Fighter target = this.Fight.GetFirstFighter<Fighter>(entry => (int)entry.CellId == adjCellId);
            if (target == null)
            {
                return false;
            }
            else
            {
                MapPoint startPoint = target.Point;
                MapPoint point = CastPoint;
                MapPoint[] cellsOnLineBetween = startPoint.GetCellsOnLineBetween(CastPoint);
                for (int index = 0; index < cellsOnLineBetween.Length; ++index)
                {
                    MapPoint mapPoint = cellsOnLineBetween[index];
                    if (!this.Fight.IsCellFree(mapPoint.CellId))
                        point = index <= 0 ? startPoint : new MapPoint((short)cellsOnLineBetween[index - 1].CellId);
                    if (this.Fight.ShouldTriggerOnMove(mapPoint.CellId))
                    {
                        point = mapPoint;
                        break;
                    }
                }
                target.Slide(Source, point.CellId);
                return true;
            }
        }
    }
}
