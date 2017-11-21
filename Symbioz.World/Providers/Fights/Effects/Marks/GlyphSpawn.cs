using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Marks;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Marks
{
    [SpellEffectHandler(EffectsEnum.Effect_Glyph), SpellEffectHandler(EffectsEnum.Effect_Glyph_402)]
    public class GlyphSpawn : SpellEffectHandler
    {
        public GlyphSpawn(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            MarkTriggerTypeEnum type = GetTriggerType();
            Zone zone = new Zone(Effect.ShapeType, Effect.Radius);
            Color color = Color.FromArgb(Effect.Value);
            Glyph glyph = new Glyph(Fight.PopNextMarkId(), Source, SpellLevel, Effect, CastPoint, zone, color, type);
            Fight.AddMark(glyph);
            return true;
        }

        private MarkTriggerTypeEnum GetTriggerType()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_Glyph:
                    return MarkTriggerTypeEnum.ON_TURN_STARTED;
                case EffectsEnum.Effect_Glyph_402:
                    return MarkTriggerTypeEnum.ON_TURN_ENDED;
            }

            return MarkTriggerTypeEnum.NONE;
        }
    }
}
