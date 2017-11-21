using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_SubRange)]
    [SpellEffectHandler(EffectsEnum.Effect_SubWisdom)]
    [SpellEffectHandler(EffectsEnum.Effect_SubCriticalHit)]
    [SpellEffectHandler(EffectsEnum.Effect_SubDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_SubDamageBonusPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubAgility)]
    [SpellEffectHandler(EffectsEnum.Effect_SubStrength)]
    [SpellEffectHandler(EffectsEnum.Effect_SubChance)]
    [SpellEffectHandler(EffectsEnum.Effect_SubIntelligence)]
    [SpellEffectHandler(EffectsEnum.Effect_SubRange_135)]
    [SpellEffectHandler(EffectsEnum.Effect_SubHealBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_SubPushDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_SubPushDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_SubCriticalDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_SubCriticalDamageBonus)]

    [SpellEffectHandler(EffectsEnum.Effect_SubWaterResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubEarthResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubFireResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubAirResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_SubNeutralResistPercent)]
    public class StatsDebuff : SpellEffectHandler
    {
        public StatsDebuff(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
         Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (Fighter current in targets)
            {
                if (this.Effect.Duration > 0)
                {
                    base.AddStatBuff(current, (short)-Effect.DiceMin, GetEffectCaracteristic(current, Effect.EffectEnum), FightDispellableEnum.DISPELLABLE);
                }
            }
            return true;
        }

        public static Characteristic GetEffectCaracteristic(Fighter target, EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_SubRange:
                    return target.Stats.Range;

                case EffectsEnum.Effect_SubWisdom:
                    return target.Stats.Wisdom;

                case EffectsEnum.Effect_SubCriticalHit:
                    return target.Stats.CriticalHit;

                case EffectsEnum.Effect_SubDamageBonus:
                    return target.Stats.AllDamagesBonus;

                case EffectsEnum.Effect_SubDamageBonusPercent:
                    return target.Stats.DamagesBonusPercent;

                case EffectsEnum.Effect_SubAgility:
                    return target.Stats.Agility;

                case EffectsEnum.Effect_SubStrength:
                    return target.Stats.Strength;

                case EffectsEnum.Effect_SubChance:
                    return target.Stats.Chance;

                case EffectsEnum.Effect_SubIntelligence:
                    return target.Stats.Intelligence;

                case EffectsEnum.Effect_SubRange_135:
                    return target.Stats.Range;

                case EffectsEnum.Effect_SubHealBonus:
                    return target.Stats.HealBonus;

                case EffectsEnum.Effect_SubPushDamageBonus:
                    return target.Stats.PushDamageBonus;

                case EffectsEnum.Effect_SubPushDamageReduction:
                    return target.Stats.PushDamageReduction;

                case EffectsEnum.Effect_SubCriticalDamageReduction:
                    return target.Stats.CriticalDamageReduction;

                case EffectsEnum.Effect_SubCriticalDamageBonus:
                    return target.Stats.CriticalDamageBonus;

                case EffectsEnum.Effect_SubAirResistPercent:
                    return target.Stats.AirResistPercent;

                case EffectsEnum.Effect_SubFireResistPercent:
                    return target.Stats.FireResistPercent;

                case EffectsEnum.Effect_SubEarthResistPercent:
                    return target.Stats.EarthResistPercent;

                case EffectsEnum.Effect_SubWaterResistPercent:
                    return target.Stats.WaterResistPercent;

                case EffectsEnum.Effect_SubNeutralResistPercent:
                    return target.Stats.NeutralResistPercent;


                default:
                    target.Fight.Reply(effect + " is not considered as statBuff.");
                    return null;
            }

        }
    }
}
