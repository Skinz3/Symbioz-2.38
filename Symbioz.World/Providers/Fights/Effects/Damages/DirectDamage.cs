using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_DamageFire)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarth)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageAir)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWater)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutral)]
    public class DirectDamage : SpellEffectHandler
    {
        public EffectElementType ElementType
        {
            get;
            set;
        }

        public DirectDamage(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
            Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {
            switch (effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageEarth:
                    ElementType = EffectElementType.Earth;
                    break;
                case EffectsEnum.Effect_DamageWater:
                    ElementType = EffectElementType.Water;
                    break;
                case EffectsEnum.Effect_DamageFire:
                    ElementType = EffectElementType.Fire;
                    break;
                case EffectsEnum.Effect_DamageAir:
                    ElementType = EffectElementType.Air;
                    break;
                case EffectsEnum.Effect_DamageNeutral:
                    ElementType = EffectElementType.Neutral;
                    break;
            }
        }
        public override bool Apply(Fighter[] targets)
        {
            if (Effect.Duration > 0)
            {
                foreach (var target in targets)
                {
                    base.AddTriggerBuff(target, FightDispellableEnum.DISPELLABLE, TriggerType.TURN_BEGIN, DamageTrigger);
                }
            }
            else
            {
                Jet jet = FormulasProvider.Instance.EvaluateJet(Source, ElementType, Effect, this.SpellId);

                foreach (var target in targets)
                {
                    target.InflictDamages(new Damage(Source, target, jet.Clone(), ElementType, Effect, Critical));
                }
            }
            return true;
        }

        private bool DamageTrigger(TriggerBuff buff, TriggerType trigger, object token)
        {
            Jet jet = FormulasProvider.Instance.EvaluateJet(Source, ElementType, Effect, this.SpellId);
            buff.Target.InflictDamages(new Damage(Source, buff.Target, jet, ElementType, Effect, Critical));
            return false;
        }

    }
}
