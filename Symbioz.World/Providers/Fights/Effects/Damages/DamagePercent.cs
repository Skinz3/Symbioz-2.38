using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.Core;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Damages;

namespace Symbioz.World.Providers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_DamagePercentNeutral)]
    public class DamagePercent : SpellEffectHandler
    {
        public DamagePercent(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
           Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            int num = Source.Stats.CurrentLifePoints.GetPercentageOf(Effect.DiceMin);

            foreach (var target in targets)
            {
                target.InflictDamages(new Damage(Source, target, (short)num, EffectElementType.Neutral));
            }
            return true;
        }
    }
}
