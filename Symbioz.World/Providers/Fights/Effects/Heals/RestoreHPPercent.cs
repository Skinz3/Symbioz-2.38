using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;

namespace Symbioz.World.Providers.Fights.Effects.Heals
{
    [SpellEffectHandler(EffectsEnum.Effect_RestoreHPPercent)]
    public class RestoreHPPercent : SpellEffectHandler
    {
        public RestoreHPPercent(Fighter source ,SpellLevelRecord spellLevel, EffectInstance effect,
           Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint,critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                short delta = (short)target.Stats.CurrentMaxLifePoints.GetPercentageOf(Effect.DiceMin);
                target.Heal(Source, delta);
            }
            return true;
        }
    }
}
