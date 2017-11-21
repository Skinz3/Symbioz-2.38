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
    [SpellEffectHandler(EffectsEnum.Effect_StealRange)]
    [SpellEffectHandler(EffectsEnum.Effect_StealChance)]
    [SpellEffectHandler(EffectsEnum.Effect_StealVitality)]
    [SpellEffectHandler(EffectsEnum.Effect_StealAgility)]
    [SpellEffectHandler(EffectsEnum.Effect_StealIntelligence)]
    [SpellEffectHandler(EffectsEnum.Effect_StealStrength)]
    public class StatsSteal : SpellEffectHandler
    {
        public StatsSteal(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
         Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (Fighter current in targets)
            {
                EffectsEnum[] buffDisplayedEffect = GetBuffDisplayedEffect(this.Effect.EffectEnum);
                base.AddStatBuff(current, (short)-Effect.DiceMin, GetEffectCaracteristic(current, this.Effect.EffectEnum), FightDispellableEnum.DISPELLABLE, (short)buffDisplayedEffect[1]);
                base.AddStatBuff(Source, (short)Effect.DiceMin, GetEffectCaracteristic(Source, Effect.EffectEnum), FightDispellableEnum.DISPELLABLE, (short)buffDisplayedEffect[0]);
            }
            return true;
        }
        private static Characteristic GetEffectCaracteristic(Fighter fighter, EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_StealChance:
                    return fighter.Stats.Chance;

                case EffectsEnum.Effect_StealVitality:
                    return fighter.Stats.Vitality;

                case EffectsEnum.Effect_StealAgility:
                    return fighter.Stats.Agility;

                case EffectsEnum.Effect_StealIntelligence:
                    return fighter.Stats.Intelligence;

                case EffectsEnum.Effect_StealWisdom:
                    return fighter.Stats.Wisdom;

                case EffectsEnum.Effect_StealStrength:
                    return fighter.Stats.Strength;

                case EffectsEnum.Effect_StealRange:
                    return fighter.Stats.Range;

                default:
                    throw new Exception("No associated caracteristic to effect '" + effect + "'");
            }
        }
        private static EffectsEnum[] GetBuffDisplayedEffect(EffectsEnum effect)
        {
            EffectsEnum[] result;
            switch (effect)
            {
                case EffectsEnum.Effect_StealChance:
                    result = new EffectsEnum[]
				{
					EffectsEnum.Effect_AddChance,
					EffectsEnum.Effect_SubChance
				};
                    break;
                case EffectsEnum.Effect_StealVitality:
                    result = new EffectsEnum[]
				{
					EffectsEnum.Effect_AddVitality,
					EffectsEnum.Effect_SubVitality
				};
                    break;
                case EffectsEnum.Effect_StealAgility:
                    result = new EffectsEnum[]
				{
					EffectsEnum.Effect_AddAgility,
					EffectsEnum.Effect_SubAgility
				};
                    break;
                case EffectsEnum.Effect_StealIntelligence:
                    result = new EffectsEnum[]
				{
					EffectsEnum.Effect_AddIntelligence,
					EffectsEnum.Effect_SubIntelligence
				};
                    break;
                case EffectsEnum.Effect_StealWisdom:
                    result = new EffectsEnum[]
				{
					EffectsEnum.Effect_AddWisdom,
					EffectsEnum.Effect_SubWisdom
				};
                    break;
                case EffectsEnum.Effect_StealStrength:
                    result = new EffectsEnum[]
				{
					EffectsEnum.Effect_AddStrength,
					EffectsEnum.Effect_SubStrength
				};
                    break;
                default:
                    if (effect != EffectsEnum.Effect_StealRange)
                    {
                        throw new System.Exception("No associated caracteristic to effect '" + effect + "'");
                    }
                    result = new EffectsEnum[]
				{
					EffectsEnum.Effect_AddRange,
					EffectsEnum.Effect_SubRange
				};
                    break;
            }
            return result;
        }
    }
}
