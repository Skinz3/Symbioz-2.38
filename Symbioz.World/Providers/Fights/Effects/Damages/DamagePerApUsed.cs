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
    [SpellEffectHandler(EffectsEnum.Effect_DamageAirPerAP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarthPerAP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFirePerAP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutralPerAP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWaterPerAP)]
    public class DamagePerApUsed : SpellEffectHandler
    {
        private EffectElementType ElementType
        {
            get;
            set;
        }
        public DamagePerApUsed(Fighter source, SpellLevelRecord level, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, level, effect, targets, castPoint, critical)
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageAirPerAP:
                    ElementType = EffectElementType.Air;
                    break;
                case EffectsEnum.Effect_DamageEarthPerAP:
                    ElementType = EffectElementType.Earth;
                    break;
                case EffectsEnum.Effect_DamageFirePerAP:
                    ElementType = EffectElementType.Fire;
                    break;
                case EffectsEnum.Effect_DamageNeutralPerAP:
                    ElementType = EffectElementType.Neutral;
                    break;
                case EffectsEnum.Effect_DamageWaterPerAP:
                    ElementType = EffectElementType.Water;
                    break;
            }
        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                base.AddTriggerBuff(target, FightDispellableEnum.DISPELLABLE, TriggerType.TURN_END, OnTurnEnded);
            }
            return true;
        }
        private bool OnTurnEnded(TriggerBuff buff, TriggerType trigger, object token)
        {
            short jetMin = (short)(Effect.DiceMin * buff.Target.Stats.ApUsed);
            short jetMax = (short)(Effect.DiceMax * buff.Target.Stats.ApUsed);

            if (jetMin < jetMax)
            {
                Jet jet = FormulasProvider.Instance.EvaluateJet(buff.Caster, ElementType, jetMin, jetMax, buff.Caster.GetSpellBoost(SpellId), false);
                buff.Target.InflictDamages(new Damage(buff.Caster, buff.Target, jet, ElementType, Effect, Critical));
            }
                return false;
        }
    }
}
