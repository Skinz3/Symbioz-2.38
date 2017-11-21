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

namespace Symbioz.World.Providers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_AddRange_136)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWisdom)]
    [SpellEffectHandler(EffectsEnum.Effect_AddPhysicalDamage_137)]
    [SpellEffectHandler(EffectsEnum.Effect_AddCriticalHit)]
    [SpellEffectHandler(EffectsEnum.Effect_AddDamageBonus_121)]
    [SpellEffectHandler(EffectsEnum.Effect_AddMagicDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_IncreaseDamage_1054)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAgility)]
    [SpellEffectHandler(EffectsEnum.Effect_AddDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddSummonLimit)]
    [SpellEffectHandler(EffectsEnum.Effect_IncreaseDamage_138)]
    [SpellEffectHandler(EffectsEnum.Effect_AddDamageBonusPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddStrength)]
    [SpellEffectHandler(EffectsEnum.Effect_AddPhysicalDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddChance)]
    [SpellEffectHandler(EffectsEnum.Effect_AddIntelligence)]
    [SpellEffectHandler(EffectsEnum.Effect_AddRange)]
    [SpellEffectHandler(EffectsEnum.Effect_AddDamageReflection)]
    [SpellEffectHandler(EffectsEnum.Effect_AddPhysicalDamage_142)]
    [SpellEffectHandler(EffectsEnum.Effect_AddHealBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddPushDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddPushDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_WeaponDamagePercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddCriticalDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddCriticalDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_FinalDamageDamagePercent)]
    // [SpellEffectHandler(EffectsEnum.Effect_AddLock)]
    // [SpellEffectHandler(EffectsEnum.Effect_AddDodge)]
    public class StatsBuff : SpellEffectHandler
    {
        public StatsBuff(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
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

                    base.AddStatBuff(current, (short)Effect.RandomizeMinMax(), GetEffectCaracteristic(current, Effect.EffectEnum), FightDispellableEnum.DISPELLABLE);
                }
            }
            return true;
        }
        public static Characteristic GetEffectCaracteristic(Fighter target, EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_AddRange:
                    return target.Stats.Range;

                case EffectsEnum.Effect_AddRange_136:
                    return target.Stats.Range;

                case EffectsEnum.Effect_IncreaseDamage_138:
                    return target.Stats.DamagesBonusPercent;

                case EffectsEnum.Effect_AddAgility:
                    return target.Stats.Agility;

                case EffectsEnum.Effect_AddChance:
                    return target.Stats.Chance;

                case EffectsEnum.Effect_AddIntelligence:
                    return target.Stats.Intelligence;

                case EffectsEnum.Effect_AddStrength:
                    return target.Stats.Strength;

                case EffectsEnum.Effect_AddDamageBonus:
                    return target.Stats.AllDamagesBonus;

                case EffectsEnum.Effect_AddSummonLimit:
                    return target.Stats.SummonableCreaturesBoost;

                case EffectsEnum.Effect_IncreaseDamage_1054:
                    return target.Stats.DamagesBonusPercent;

                case EffectsEnum.Effect_AddCriticalHit:
                    return target.Stats.CriticalHit;

                case EffectsEnum.Effect_RunesBoostPercent:
                    return target.Stats.RuneBonusPercent;

                case EffectsEnum.Effect_AddDamageBonus_121:
                    return target.Stats.AllDamagesBonus;

                case EffectsEnum.Effect_AddWisdom:
                    return target.Stats.Wisdom;

                case EffectsEnum.Effect_AddAP_111:
                    return target.Stats.ActionPoints;

                case EffectsEnum.Effect_AddMP:
                    return target.Stats.MovementPoints;

                case EffectsEnum.Effect_AddMP_128:
                    return target.Stats.MovementPoints;

                case EffectsEnum.Effect_AddHealBonus:
                    return target.Stats.HealBonus;

                case EffectsEnum.Effect_AddPushDamageBonus:
                    return target.Stats.PushDamageBonus;

                case EffectsEnum.Effect_AddPushDamageReduction:
                    return target.Stats.PushDamageReduction;

                case EffectsEnum.Effect_WeaponDamagePercent:
                    return target.Stats.WeaponDamagesBonusPercent;

                case EffectsEnum.Effect_AddDamageReflection:
                    return target.Stats.Reflect;

                case EffectsEnum.Effect_AddCriticalDamageReduction:
                    return target.Stats.CriticalDamageReduction;

                case EffectsEnum.Effect_AddCriticalDamageBonus:
                    return target.Stats.CriticalDamageBonus;

                case EffectsEnum.Effect_FinalDamageDamagePercent:
                    return target.Stats.DamagesBonusPercent;

                default:
                    target.Fight.Reply(effect + " is not considered as statBuff.");
                    return null;
            }
        }
    }
}
