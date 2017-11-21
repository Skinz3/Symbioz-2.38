using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Spells;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_SymetricCasterTeleport)]
    public class SymetricCasterTeleport : SpellEffectHandler
    {
        public SymetricCasterTeleport(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
           Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            if (Fight.GetFighter(CastPoint.CellId) != null)
            {
                Fighter target1 = targets.FirstOrDefault();
                MapPoint point = CastPoint;

                point =  new MapPoint((2 * Source.Point.X - CastPoint.X), (2 * Source.Point.Y - CastPoint.Y));

                if (target1 != null)
                {

                    Fighter target2 = Fight.GetFighter(point);

                    if (target2 == null)
                    {
                        if (Fight.IsCellFree(point.CellId))
                            target1.Teleport(Source, point);
                    }
                    else
                    {
                        target1.SwitchPosition(target2);
                    }
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
