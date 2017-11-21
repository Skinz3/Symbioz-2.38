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
    [SpellEffectHandler(EffectsEnum.Effect_Rune)]
    public class RuneSpawn : SpellEffectHandler
    {
        public RuneSpawn(Fighter source, SpellLevelRecord level, EffectInstance effect,
         Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, level, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            if (Fight.GetMarks<Rune>(x => x.CenterPoint.CellId == CastPoint.CellId).Count() > 0)
            {
                return false;
            }

            Zone zone = new Zone(Effect.ShapeType, Effect.Radius);
            Color color = Color.FromArgb(Effect.Value);
            Rune rune = new Rune(Fight.PopNextMarkId(), Source, SpellLevel, Effect, CastPoint, zone, color);
            Fight.AddMark(rune);
            return true;
        }
    }
}
