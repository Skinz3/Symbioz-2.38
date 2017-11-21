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
    [SpellEffectHandler(EffectsEnum.Effect_DamageAirPerMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarthPerMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFirePerMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutralPerMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWaterPerMP)]
    public class DamagePerMpUsed : SpellEffectHandler
    {
        private EffectElementType ElementType
        {
            get;
            set;
        }
        public DamagePerMpUsed(Fighter source, SpellLevelRecord level, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, level, effect, targets, castPoint, critical)
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_DamageAirPerMP:
                    ElementType = EffectElementType.Air;
                    break;
                case EffectsEnum.Effect_DamageEarthPerMP:
                    ElementType = EffectElementType.Earth;
                    break;
                case EffectsEnum.Effect_DamageFirePerMP:
                    ElementType = EffectElementType.Fire;
                    break;
                case EffectsEnum.Effect_DamageNeutralPerMP:
                    ElementType = EffectElementType.Neutral;
                    break;
                case EffectsEnum.Effect_DamageWaterPerMP:
                    ElementType = EffectElementType.Water;
                    break;
            }
        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                base.AddTriggerBuff(target,FightDispellableEnum.DISPELLABLE, TriggerType.TURN_END, OnTurnEnded);
            }
            return true;
        }
        private bool OnTurnEnded(TriggerBuff buff, TriggerType trigger, object token)
        {
            short jetDelta = (short)(Effect.DiceMax * buff.Target.Stats.MpUsed);

            Jet jet = FormulasProvider.Instance.EvaluateJet(buff.Caster, ElementType, jetDelta, 0, buff.Caster.GetSpellBoost(SpellId), false);
            buff.Target.InflictDamages(new Damage(buff.Caster, buff.Target, jet, ElementType, Effect, Critical));

            return false;
        }
    }
}
