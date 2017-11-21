using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Records.Spells;
using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;

namespace Symbioz.World.Modules
{
    public class SpellCategoryBuilder
    {
        static Logger logger = new Logger();

        static EffectsEnum[] SpecialEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_Teleport,
            EffectsEnum.Effect_Summon,

        };
      //  [StartupInvoke("Spell Category Builder", StartupInvokePriority.Modules)]
        public static void Execute()
        {
            UpdateLogger uLog = new UpdateLogger();
            int i = 0;
            foreach (var spell in SpellRecord.Spells)
            {
                var oldCategory = spell.CategoryEnum;
                spell.CategoryEnum = Analyse(spell);
                spell.UpdateInstantElement();

                if (oldCategory != spell.CategoryEnum)
                {
                    logger.DarkGray(spell.Name + " category has been setted to: " + spell.CategoryEnum);
                }
                uLog.Update(i.Percentage(SpellRecord.Spells.Count));
                i++;
            }
            logger.Gray("Spells Updated!");
        }
        private static SpellCategoryEnum Analyse(SpellRecord spellRecord)
        {
            Dictionary<SpellCategoryEnum, int> selector = new Dictionary<SpellCategoryEnum, int>();

            foreach (var level in spellRecord.Levels)
            {
                foreach (var effect in level.Effects.ConvertAll<EffectsEnum>(x => x.EffectEnum))
                {
                    SpellCategoryEnum category = GetCategory(effect);

                    if (selector.ContainsKey(category))
                        selector[category] += 1;
                    else
                        selector.Add(category, 1);
                }
            }
            selector = selector.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            if (selector.Count > 0)
            {
                var special = GetSpecialCategory(spellRecord.GetLastLevel());
                return special == SpellCategoryEnum.Unknown ? selector.First().Key : special;
            }
            else
            {
                return SpellCategoryEnum.Unknown;
            }
        }
        private static SpellCategoryEnum GetSpecialCategory(SpellLevelRecord level)
        {
            foreach (var specialEffect in SpecialEffects)
            {
                var effect = level.Effects.FirstOrDefault(x => x.EffectEnum == specialEffect);

                if (effect != null)
                {
                    return GetCategory(specialEffect);
                }
            }

            return SpellCategoryEnum.Unknown;
        }
        private static SpellCategoryEnum GetCategory(EffectsEnum effect)
        {
            SpellCategoryEnum result = SpellCategoryEnum.Unknown;

            switch (effect)
            {
                case EffectsEnum.Effect_LuckyStrike:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_Teleport:
                    result = SpellCategoryEnum.Teleport;
                    break;
                case EffectsEnum.Effect_PushBack:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_PullForward:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SwitchPosition:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealMP_77:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddMP:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_HealHP_81:
                    result = SpellCategoryEnum.Heal;
                    break;
                case EffectsEnum.Effect_StealHPFix:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealAP_84:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamagePercentWater:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamagePercentEarth:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamagePercentAir:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamagePercentFire:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamagePercentNeutral:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_GiveHPPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_StealHPWater:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealHPEarth:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealHPAir:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealHPFire:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealHPNeutral:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageWater:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageEarth:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageAir:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageFire:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageNeutral:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_RemoveAP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddGlobalDamageReduction_105:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_ReflectSpell:

                    break;
                case EffectsEnum.Effect_AddDamageReflection:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_HealHP_108:
                    result = SpellCategoryEnum.Heal;
                    break;
                case EffectsEnum.Effect_109:
                    break;
                case EffectsEnum.Effect_AddHealth:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddAP_111:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddDamageBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_DoubleDamageOrRestoreHP:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddDamageMultiplicator:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddCriticalHit:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubRange:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddRange:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddStrength:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddAgility:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_RegainAP:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddDamageBonus_121:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddCriticalMiss:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddChance:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddWisdom:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddVitality:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddIntelligence:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_LostMP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddMP_128:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_StealKamas:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_LoseHPByUsingAP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DispelMagicEffects:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_LosingAP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_LosingMP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubRange_135:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddRange_136:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddPhysicalDamage_137:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_IncreaseDamage_138:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_RestoreEnergyPoints:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SkipTurn:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_Kill:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddPhysicalDamage_142:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_HealHP_143:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_DamageFix:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubDamageBonus:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_ChangesWords:
                    break;
                case EffectsEnum.Effect_ReviveAlly:
                    result = SpellCategoryEnum.ReviveDeath;
                    break;
                case EffectsEnum.Effect_Followed:
                    break;
                case EffectsEnum.Effect_ChangeAppearance:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_Invisibility:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubChance:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubVitality:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubAgility:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubIntelligence:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubWisdom:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubStrength:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_IncreaseWeight:
                    break;
                case EffectsEnum.Effect_DecreaseWeight:
                    break;
                case EffectsEnum.Effect_IncreaseAPAvoid:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_IncreaseMPAvoid:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubDodgeAPProbability:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubDodgeMPProbability:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddGlobalDamageReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddDamageBonusPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_166:
                    break;
                case EffectsEnum.Effect_SubAP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubMP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubCriticalHit:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubMagicDamageReduction:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubPhysicalDamageReduction:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddInitiative:
                    break;
                case EffectsEnum.Effect_SubInitiative:
                    break;
                case EffectsEnum.Effect_AddProspecting:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubProspecting:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddHealBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubHealBonus:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_Double:
                    result = SpellCategoryEnum.Summon;
                    break;
                case EffectsEnum.Effect_Summon:
                    result = SpellCategoryEnum.Summon;
                    break;
                case EffectsEnum.Effect_AddSummonLimit:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddMagicDamageReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddPhysicalDamageReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_185:
                    break;
                case EffectsEnum.Effect_SubDamageBonusPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_188:
                    break;
                case EffectsEnum.Effect_GiveKamas:
                    break;
                case EffectsEnum.Effect_197:
                    break;
                case EffectsEnum.Effect_201:
                    break;
                case EffectsEnum.Effect_RevealsInvisible:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_206:
                    break;
                case EffectsEnum.Effect_AddEarthResistPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddWaterResistPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddAirResistPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddFireResistPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddNeutralResistPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubEarthResistPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubWaterResistPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubAirResistPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubFireResistPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubNeutralResistPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_220:
                    break;
                case EffectsEnum.Effect_221:
                    break;
                case EffectsEnum.Effect_222:
                    break;
                case EffectsEnum.Effect_AddTrapBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddTrapBonusPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_229:
                    break;
                case EffectsEnum.Effect_230:
                    break;
                case EffectsEnum.Effect_239:
                    break;
                case EffectsEnum.Effect_AddEarthElementReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddWaterElementReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddAirElementReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddFireElementReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddNeutralElementReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubEarthElementReduction:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubWaterElementReduction:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubAirElementReduction:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubFireElementReduction:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubNeutralElementReduction:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddPvpEarthResistPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddPvpWaterResistPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddPvpAirResistPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddPvpFireResistPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddPvpNeutralResistPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubPvpEarthResistPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubPvpWaterResistPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubPvpAirResistPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubPvpFireResistPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubPvpNeutralResistPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddPvpEarthElementReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddPvpWaterElementReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddPvpAirElementReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddPvpFireElementReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddPvpNeutralElementReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddArmorDamageReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_StealChance:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealVitality:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealAgility:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealIntelligence:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealWisdom:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealStrength:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageHPLostPercentNeutral:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageHPLostPercentAir:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageHPLostPercentFire:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageHPLostPercentStrenght:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageHPLostPercentWater:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_281:
                    break;
                case EffectsEnum.Effect_282:
                    break;
                case EffectsEnum.Effect_283:
                    break;
                case EffectsEnum.Effect_284:
                    break;
                case EffectsEnum.Effect_285:
                    break;
                case EffectsEnum.Effect_286:
                    break;
                case EffectsEnum.Effect_287:
                    break;
                case EffectsEnum.Effect_288:
                    break;
                case EffectsEnum.Effect_289:
                    break;
                case EffectsEnum.Effect_290:
                    break;
                case EffectsEnum.Effect_291:
                    break;
                case EffectsEnum.Effect_292:
                    break;
                case EffectsEnum.Effect_SpellBoost:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_294:
                    break;
                case EffectsEnum.Effect_310:
                    break;
                case EffectsEnum.Effect_StealRange:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_333:
                    break;
                case EffectsEnum.Effect_ChangeAppearance_335:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_Trap:
                    result = SpellCategoryEnum.Mark;
                    break;
                case EffectsEnum.Effect_Glyph:
                    result = SpellCategoryEnum.Mark;
                    break;
                case EffectsEnum.Effect_Glyph_402:
                    result = SpellCategoryEnum.Mark;
                    break;
                case EffectsEnum.Effect_KillReplacePerInvocation:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_DispellSpell:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_407:
                    break;
                case EffectsEnum.Effect_410:
                    break;
                case EffectsEnum.Effect_411:
                    break;
                case EffectsEnum.Effect_412:
                    break;
                case EffectsEnum.Effect_413:
                    break;
                case EffectsEnum.Effect_AddPushDamageBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubPushDamageBonus:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddPushDamageReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubPushDamageReduction:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddCriticalDamageBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubCriticalDamageBonus:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddCriticalDamageReduction:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubCriticalDamageReduction:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddEarthDamageBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubEarthDamageBonus:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddFireDamageBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubFireDamageBonus:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddWaterDamageBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubWaterDamageBonus:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddAirDamageBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubAirDamageBonus:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddNeutralDamageBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubNeutralDamageBonus:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealAP_440:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_StealMP_441:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_513:
                    break;
                case EffectsEnum.Effect_SpawnPoint:
                    break;
                case EffectsEnum.Effect_601:
                    break;
                case EffectsEnum.Effect_602:
                    break;
                case EffectsEnum.Effect_603:
                    break;
                case EffectsEnum.Effect_LearnSpell:
                    break;
                case EffectsEnum.Effect_AddExp:
                    break;
                case EffectsEnum.Effect_AddPermanentWisdom:
                    break;
                case EffectsEnum.Effect_AddPermanentStrength:
                    break;
                case EffectsEnum.Effect_AddPermanentChance:
                    break;
                case EffectsEnum.Effect_AddPermanentAgility:
                    break;
                case EffectsEnum.Effect_AddPermanentVitality:
                    break;
                case EffectsEnum.Effect_AddPermanentIntelligence:
                    break;
                case EffectsEnum.Effect_612:
                    break;
                case EffectsEnum.Effect_AddSpellPoints:
                    break;
                case EffectsEnum.Effect_614:
                    break;
                case EffectsEnum.Effect_615:
                    break;
                case EffectsEnum.Effect_616:
                    break;
                case EffectsEnum.Effect_Book:
                    break;
                case EffectsEnum.Effect_622:
                    break;
                case EffectsEnum.Effect_623:
                    break;
                case EffectsEnum.Effect_624:
                    break;
                case EffectsEnum.Effect_625:
                    break;
                case EffectsEnum.Effect_626:
                    break;
                case EffectsEnum.Effect_627:
                    break;
                case EffectsEnum.Effect_628:
                    break;
                case EffectsEnum.Effect_631:
                    break;
                case EffectsEnum.Effect_640:
                    break;
                case EffectsEnum.Effect_641:
                    break;
                case EffectsEnum.Effect_642:
                    break;
                case EffectsEnum.Effect_643:
                    break;
                case EffectsEnum.Effect_645:
                    break;
                case EffectsEnum.Effect_646:
                    break;
                case EffectsEnum.Effect_647:
                    break;
                case EffectsEnum.Effect_648:
                    break;
                case EffectsEnum.Effect_649:
                    break;
                case EffectsEnum.Effect_654:
                    break;
                case EffectsEnum.Effect_666:
                    break;
                case EffectsEnum.Effect_669:
                    break;
                case EffectsEnum.Effect_670:
                    break;
                case EffectsEnum.Effect_671:
                    break;
                case EffectsEnum.Effect_Punishment_Damage:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_699:
                    break;
                case EffectsEnum.Effect_700:
                    break;
                case EffectsEnum.Effect_701:
                    break;
                case EffectsEnum.Effect_702:
                    break;
                case EffectsEnum.Effect_705:
                    break;
                case EffectsEnum.Effect_706:
                    break;
                case EffectsEnum.Effect_707:
                    break;
                case EffectsEnum.Effect_710:
                    break;
                case EffectsEnum.Effect_715:
                    break;
                case EffectsEnum.Effect_716:
                    break;
                case EffectsEnum.Effect_PetMonsterFeed:
                    break;
                case EffectsEnum.Effect_720:
                    break;
                case EffectsEnum.Effect_Title:
                    break;
                case EffectsEnum.Effect_725:
                    break;
                case EffectsEnum.Effect_730:
                    break;
                case EffectsEnum.Effect_731:
                    break;
                case EffectsEnum.Effect_732:
                    break;
                case EffectsEnum.Effect_740:
                    break;
                case EffectsEnum.Effect_741:
                    break;
                case EffectsEnum.Effect_742:
                    break;
                case EffectsEnum.Effect_750:
                    break;
                case EffectsEnum.Effect_751:
                    break;
                case EffectsEnum.Effect_AddDodge:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddLock:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubDodge:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubLock:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_760:
                    break;
                case EffectsEnum.Effect_Sacrifice:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_770:
                    break;
                case EffectsEnum.Effect_771:
                    break;
                case EffectsEnum.Effect_772:
                    break;
                case EffectsEnum.Effect_773:
                    break;
                case EffectsEnum.Effect_774:
                    break;
                case EffectsEnum.Effect_775:
                    break;
                case EffectsEnum.Effect_AddErosion:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_Revive:
                    result = SpellCategoryEnum.ReviveDeath;
                    break;
                case EffectsEnum.Effect_781:
                    break;
                case EffectsEnum.Effect_782:
                    break;
                case EffectsEnum.Effect_RepelsTo:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_Rollback:
                    break;
                case EffectsEnum.Effect_785:
                    break;
                case EffectsEnum.Effect_786:
                    break;
                case EffectsEnum.Effect_787:
                    break;
                case EffectsEnum.Effect_Punishment:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_789:
                    break;
                case EffectsEnum.Effect_790:
                    break;
                case EffectsEnum.Effect_791:
                    break;
                case EffectsEnum.Effect_TriggeredEffect:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_Rewind:
                    break;
                case EffectsEnum.Effect_795:
                    break;
                case EffectsEnum.Effect_PetFeedQuantity:
                    break;
                case EffectsEnum.Effect_805:
                    break;
                case EffectsEnum.Effect_806:
                    break;
                case EffectsEnum.Effect_807:
                    break;
                case EffectsEnum.Effect_LastMeal:
                    break;
                case EffectsEnum.Effect_810:
                    break;
                case EffectsEnum.Effect_RemainingFights:
                    break;
                case EffectsEnum.Effect_RemainingEtheral:
                    break;
                case EffectsEnum.Effect_813:
                    break;
                case EffectsEnum.Effect_814:
                    break;
                case EffectsEnum.Effect_815:
                    break;
                case EffectsEnum.Effect_816:
                    break;
                case EffectsEnum.Effect_825:
                    break;
                case EffectsEnum.Effect_ItemTeleport:
                    break;
                case EffectsEnum.Effect_905:
                    break;
                case EffectsEnum.Effect_930:
                    break;
                case EffectsEnum.Effect_931:
                    break;
                case EffectsEnum.Effect_932:
                    break;
                case EffectsEnum.Effect_933:
                    break;
                case EffectsEnum.Effect_934:
                    break;
                case EffectsEnum.Effect_935:
                    break;
                case EffectsEnum.Effect_936:
                    break;
                case EffectsEnum.Effect_937:
                    break;
                case EffectsEnum.Effect_939:
                    break;
                case EffectsEnum.Effect_940:
                    break;
                case EffectsEnum.Effect_946:
                    break;
                case EffectsEnum.Effect_947:
                    break;
                case EffectsEnum.Effect_948:
                    break;
                case EffectsEnum.Effect_949:
                    break;
                case EffectsEnum.Effect_AddState:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_RemoveState:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_952:
                    break;
                case EffectsEnum.Effect_960:
                    break;
                case EffectsEnum.Effect_961:
                    break;
                case EffectsEnum.Effect_Level:
                    break;
                case EffectsEnum.Effect_963:
                    break;
                case EffectsEnum.Effect_964:
                    break;
                case EffectsEnum.Effect_LivingObjectId:
                    break;
                case EffectsEnum.Effect_LivingObjectMood:
                    break;
                case EffectsEnum.Effect_LivingObjectSkin:
                    break;
                case EffectsEnum.Effect_LivingObjectCategory:
                    break;
                case EffectsEnum.Effect_LivingObjectLevel:
                    break;
                case EffectsEnum.Effect_NonExchangeable_981:
                    break;
                case EffectsEnum.Effect_NonExchangeable_982:
                    break;
                case EffectsEnum.Effect_NonExchangableUntil:
                    break;
                case EffectsEnum.Effect_984:
                    break;
                case EffectsEnum.Effect_985:
                    break;
                case EffectsEnum.Effect_986:
                    break;
                case EffectsEnum.Effect_987:
                    break;
                case EffectsEnum.Effect_988:
                    break;
                case EffectsEnum.Effect_989:
                    break;
                case EffectsEnum.Effect_990:
                    break;
                case EffectsEnum.Effect_994:
                    break;
                case EffectsEnum.Effect_MountDefinition:
                    break;
                case EffectsEnum.Effect_MountOwner:
                    break;
                case EffectsEnum.Effect_MountName:
                    break;
                case EffectsEnum.Effect_MountValidity:
                    break;
                case EffectsEnum.Effect_CityTeleport:
                    break;
                case EffectsEnum.Effect_1002:
                    break;
                case EffectsEnum.Effect_1003:
                    break;
                case EffectsEnum.Effect_1004:
                    break;
                case EffectsEnum.Effect_1005:
                    break;
                case EffectsEnum.Effect_1006:
                    break;
                case EffectsEnum.Effect_1007:
                    break;
                case EffectsEnum.Effect_1008:
                    break;
                case EffectsEnum.Effect_Detonate:
                    break;
                case EffectsEnum.Effect_1010:
                    break;
                case EffectsEnum.Effect_SummonExpControlable:
                    result = SpellCategoryEnum.Summon;
                    break;
                case EffectsEnum.Effect_DamageMpPercentageAir:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageMpPercentageEarth:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageMpPercentageNeutral:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageMpPercentageFire:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageMpPercentageWater:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_1017:
                    break;
                case EffectsEnum.Effect_1018:
                    break;
                case EffectsEnum.Effect_1019:
                    break;
                case EffectsEnum.Effect_1021:
                    break;
                case EffectsEnum.Effect_1022:
                    break;
                case EffectsEnum.Effect_1023:
                    break;
                case EffectsEnum.Effect_1024:
                    break;
                case EffectsEnum.Effect_1025:
                    break;
                case EffectsEnum.Effect_ActiveGlyph:
                    break;
                case EffectsEnum.Effect_AddComboDamagePercentage:
                    break;
                case EffectsEnum.Effect_1028:
                    break;
                case EffectsEnum.Effect_1029:
                    break;
                case EffectsEnum.Effect_1030:
                    break;
                case EffectsEnum.Effect_PassTurn:
                    break;
                case EffectsEnum.Effect_1032:
                    break;
                case EffectsEnum.Effect_1033:
                    break;
                case EffectsEnum.Effect_1034:
                    break;
                case EffectsEnum.Effect_1035:
                    break;
                case EffectsEnum.Effect_1036:
                    break;
                case EffectsEnum.Effect_1037:
                    break;
                case EffectsEnum.Eff_1038:
                    break;
                case EffectsEnum.Eff_AddShieldPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Eff_AddShield:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Eff_PushCaster:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Eff_BePulled:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_1043:
                    break;
                case EffectsEnum.Effect_Immunity_1044:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_AddSpellCooldown:
                    break;
                case EffectsEnum.Effect_1046:
                    break;
                case EffectsEnum.Effect_1047:
                    break;
                case EffectsEnum.Effect_SubHealthPercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_1049:
                    break;
                case EffectsEnum.Effect_1050:
                    break;
                case EffectsEnum.Effect_1051:
                    break;
                case EffectsEnum.Effect_1052:
                    break;
                case EffectsEnum.Effect_1053:
                    break;
                case EffectsEnum.Effect_IncreaseDamage_1054:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_1055:
                    break;
                case EffectsEnum.Effect_1057:
                    break;
                case EffectsEnum.Effect_1058:
                    break;
                case EffectsEnum.Effect_1059:
                    break;
                case EffectsEnum.Effect_1060:
                    break;
                case EffectsEnum.Effect_DamageSharing:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_1062:
                    break;
                case EffectsEnum.Effect_1063:
                    break;
                case EffectsEnum.Effect_1064:
                    break;
                case EffectsEnum.Effect_1065:
                    break;
                case EffectsEnum.Effect_1066:
                    break;
                case EffectsEnum.Effect_1067:
                    break;
                case EffectsEnum.Effect_1068:
                    break;
                case EffectsEnum.Effect_1069:
                    break;
                case EffectsEnum.Effect_1070:
                    break;
                case EffectsEnum.Effect_1071:
                    break;
                case EffectsEnum.Effect_1072:
                    break;
                case EffectsEnum.Effect_1073:
                    break;
                case EffectsEnum.Effect_1074:
                    break;
                case EffectsEnum.Effect_ReduceEffectsDuration:
                    break;
                case EffectsEnum.Effect_AddResistances:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubResistances:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddVitalityPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SubAp_1079:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SubMP_1080:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_1081:
                    break;
                case EffectsEnum.Effect_1082:
                    break;
                case EffectsEnum.Effect_1083:
                    break;
                case EffectsEnum.Effect_1084:
                    break;
                case EffectsEnum.Effect_1085:
                    break;
                case EffectsEnum.Effect_1086:
                    break;
                case EffectsEnum.Effect_1087:
                    break;
                case EffectsEnum.Effect_GlyphAuraSpawn:
                    break;
                case EffectsEnum.Effect_1092:
                    break;
                case EffectsEnum.Effect_1093:
                    break;
                case EffectsEnum.Effect_1094:
                    break;
                case EffectsEnum.Effect_1095:
                    break;
                case EffectsEnum.Effect_1096:
                    break;
                case EffectsEnum.Effect_Illusions:
                    break;
                case EffectsEnum.Effect_1098:
                    break;
                case EffectsEnum.Effect_PastTeleport:
                    break;
                case EffectsEnum.Effect_1100:
                    break;
                case EffectsEnum.Effect_1101:
                    break;
                case EffectsEnum.Effect_1102:
                    break;
                case EffectsEnum.Effect_PushBack_1103:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_SymetricCasterTeleport:
                    break;
                case EffectsEnum.Effect_SymetricPointTeleport:
                    break;
                case EffectsEnum.Effect_SymetricTargetTeleport:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_1107:
                    break;
                case EffectsEnum.Effect_1108:
                    break;
                case EffectsEnum.Effect_RestoreHPPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_1111:
                    break;
                case EffectsEnum.Effect_1118:
                    break;
                case EffectsEnum.Effect_1119:
                    break;
                case EffectsEnum.Effect_1120:
                    break;
                case EffectsEnum.Effect_1121:
                    break;
                case EffectsEnum.Effect_1122:
                    break;
                case EffectsEnum.Effect_1123:
                    break;
                case EffectsEnum.Effect_1124:
                    break;
                case EffectsEnum.Effect_1125:
                    break;
                case EffectsEnum.Effect_1126:
                    break;
                case EffectsEnum.Effect_1127:
                    break;
                case EffectsEnum.Effect_1128:
                    break;
                case EffectsEnum.Effect_1129:
                    break;
                case EffectsEnum.Effect_DamageAirPerAP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageWaterPerAP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageFirePerAP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageNeutralPerAP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageEarthPerAP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageAirPerMP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageWaterPerMP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageFirePerMP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageNeutralPerMP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_DamageEarthPerMP:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_1141:
                    break;
                case EffectsEnum.Effect_1142:
                    break;
                case EffectsEnum.Effect_WeaponDamagePercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_ChangeAppearence1151:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SpawnTaxCollector:
                    break;
                case EffectsEnum.Effect_Teleport1155:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_HealPercentageBonus:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_CastSpell:
                    break;
                case EffectsEnum.Effect_TakenDamageMultiply:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_TakenDamageHeal:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_Glyph1165:
                    result = SpellCategoryEnum.Mark;
                    break;
                case EffectsEnum.Effect_GlyphBoostPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_RunesBoostPercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_FinalDamageDamagePercent:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_FinalDamageReducePercent:
                    result = SpellCategoryEnum.Agressive;
                    break;
                case EffectsEnum.Effect_AddIdol:
                    break;
                case EffectsEnum.Effect_ChangeApparence1176:
                    result = SpellCategoryEnum.Buff;
                    break;
                case EffectsEnum.Effect_SpawnPortal:
                    result = SpellCategoryEnum.Mark;
                    break;
                case EffectsEnum.Effect_PortalTeleport:
                    break;
                case EffectsEnum.Effect_UnactivePortal:
                    break;
                case EffectsEnum.Effect_HealPercentFromTakenDamage:
                    break;
                case EffectsEnum.Effect_Rune:
                    result = SpellCategoryEnum.Mark;
                    break;
                case EffectsEnum.Effect_ActiveRunes:
                    break;
                case EffectsEnum.Effect_KillReplacePerControlableInvocation:
                    result = SpellCategoryEnum.Unknown;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
