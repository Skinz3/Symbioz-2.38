using Symbioz.Core.DesignPattern;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Records.Items;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Items
{
    public class WeaponManager : Singleton<WeaponManager>
    {
        /// <summary>
        /// Les cibles des armes sont Alliés + Ennemis.
        /// </summary>
        public const string WeaponTargetMask = "g#A";

        /// <summary>
        /// Les effets uniquements liés aux armes, séléctionnés parmis tout les effets de l'arme lors
        /// d'un cast.
        /// </summary>
        private readonly EffectsEnum[] WeaponEffects = new EffectsEnum[]
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
			EffectsEnum.Effect_RemoveAP,
			EffectsEnum.Effect_RemainingFights,
            EffectsEnum.Effect_StealKamas,
            EffectsEnum.Effect_HealHP_108,
		};

        /// <summary>
        /// Effets d'armes étant boostés en cas de coup critique.
        /// </summary>
        private readonly EffectsEnum[] WeaponBoostableEffects = new EffectsEnum[]
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
              EffectsEnum.Effect_HealHP_108,
        };
        /// <summary>
        /// Spell Id du coup de poing (corps a corps)
        /// </summary>
        public const ushort PunchSpellId = 0;

        /// <summary>
        /// Zone du coup du poing
        /// </summary>
        public const string PunchRawZone = "P1";
        /// <summary>
        /// Le nombre d'utilisation par tour en fonction du coût en PA de l'arme.
        /// </summary>
        /// <param name="apCost">Coût en PA.</param>
        /// <returns>Nombre d'utilisation.</returns>
        public short GetMaxCastPerTurn(short apCost)
        {
            if (apCost <= 2)
            {
                return 3;
            }
            else if (apCost == 3)
            {
                return 2;
            }
            else if (apCost >= 4)
            {
                return 1;
            }
            else
                return 1;

        }
        /// <summary>
        /// La zone ciblée en fonction du type d'arme.
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public string GetRawZone(ItemTypeEnum type)
        {
            switch (type)
            {
                case ItemTypeEnum.ARC:
                    return "P1";

                case ItemTypeEnum.BAGUETTE:
                    return "P1";

                case ItemTypeEnum.BÂTON:
                    return "T1";

                case ItemTypeEnum.DAGUE:
                    return "P1";

                case ItemTypeEnum.ÉPÉE:
                    return "P1";

                case ItemTypeEnum.MARTEAU:
                    return "C1";

                case ItemTypeEnum.PELLE:
                    return "L1";

                case ItemTypeEnum.HACHE:
                    return "P1";

            }

            return "P1";
        }
        public SpellLevelRecord GetWeaponSpellLevel(WeaponRecord weapon)
        {
            List<EffectInstance> effects = SelectWeaponEffects(weapon.Effects);
            List<EffectInstance> criticalEffects = GetCriticalEffects(weapon, SelectWeaponEffects(weapon.Effects));
            short maxCastPerTurn = GetMaxCastPerTurn(weapon.ApCost);

            return new SpellLevelRecord(-1, WeaponManager.PunchSpellId, 1, weapon.ApCost, weapon.MinRange,
                weapon.MaxRange, weapon.CastInLine, weapon.CastInDiagonal, weapon.CastTestLos,
                weapon.CriticalHitProbability, false, false, false, false, 0, maxCastPerTurn, 0,
                0, 0, 0, new List<short>(), new List<short>(), effects, criticalEffects);
        }
        private List<EffectInstance> SelectWeaponEffects(List<EffectInstance> effects)
        {
            List<EffectInstance> results = new List<EffectInstance>(effects);
            results.RemoveAll(x => !WeaponEffects.Contains(x.EffectEnum));
            return results;
        }
        private List<EffectInstance> GetCriticalEffects(WeaponRecord weapon, List<EffectInstance> effects)
        {
            List<EffectInstance> results = new List<EffectInstance>();

            foreach (var effect in effects.FindAll(x => WeaponBoostableEffects.Contains(x.EffectEnum)))
            {
                EffectInstance newEffect = effect.Clone();
                newEffect.DiceMin += (ushort)weapon.CriticalHitBonus;
                newEffect.DiceMax += (ushort)weapon.CriticalHitBonus;
                results.Add(newEffect);
            }
            return results;
        }
    }
}
