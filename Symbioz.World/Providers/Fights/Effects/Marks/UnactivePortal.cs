using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Marks;

namespace Symbioz.World.Providers.Fights.Effects.Marks
{
    [SpellEffectHandler(EffectsEnum.Effect_UnactivePortal)]
    public class UnactivePortal : SpellEffectHandler
    {
        public UnactivePortal(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect, Fighter[] targets, MapPoint castPoint, bool critical) : base(source, spellLevel, effect, targets, castPoint, critical)
        {
        }

        public override bool Apply(Fighter[] targets)
        {
            var portal = Fight.GetMarks<Portal>(x => x.CenterPoint.CellId == CastPoint.CellId).FirstOrDefault();

            if (portal != null && portal.Source == Source)
            {
                portal.Unactive(Source);
            }
            return true;
        }
    }
}
