using Symbioz.Core;
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

namespace Symbioz.World.Providers.Fights.Effects.Heals
{
    [SpellEffectHandler(EffectsEnum.Effect_HealHP_108)]
    public class Heal : SpellEffectHandler
    {
        public Heal(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
            Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }

        public override bool Apply(Fighter[] targets)
        {
            short jet = Effect.DiceMax > 0 ? (short)new AsyncRandom().Next(Effect.DiceMin, Effect.DiceMax + 1) : (short)Effect.DiceMin;
            short num = FormulasProvider.Instance.GetHealDelta(Source, jet);

            foreach (var target in targets)
            {
                target.Heal(Source, num);
            }

            return true;
        }
    }
}
