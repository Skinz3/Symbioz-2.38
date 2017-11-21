using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Core;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_GiveHPPercent)]
    public class GiveHPPercent : SpellEffectHandler
    {
        public GiveHPPercent(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
         Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            short num = (short)Source.Stats.CurrentLifePoints.GetPercentageOf(Effect.DiceMin);

            Source.InflictDamages(num, Source);

            foreach (var target in targets)
            {
                target.Heal(Source, num);
            }

            return true;
        }
    }
}
