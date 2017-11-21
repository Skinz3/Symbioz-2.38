using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Effects.Buffs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_Invisibility)]
    public class Invisibility : SpellEffectHandler
    {
        public static readonly EffectsEnum[] NotSilentEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_DamageWater,
            EffectsEnum.Effect_DamageEarth,
            EffectsEnum.Effect_DamageAir,
            EffectsEnum.Effect_DamageFire,
            EffectsEnum.Effect_DamageNeutral,
            EffectsEnum.Effect_StealHPWater,
            EffectsEnum.Effect_StealHPEarth,
            EffectsEnum.Effect_StealHPAir,
            EffectsEnum.Effect_StealHPFire,
            EffectsEnum.Effect_StealHPNeutral,
        };

        public Invisibility(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                InvisibilityBuff buff = new InvisibilityBuff(target.BuffIdProvider.Pop(), target, Source, SpellLevel, Effect, SpellId, Critical, FightDispellableEnum.DISPELLABLE);
                target.AddAndApplyBuff(buff);
            }
            return true;
        }
    }
}
