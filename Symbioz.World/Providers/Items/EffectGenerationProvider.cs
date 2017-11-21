using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Network;
using Symbioz.World.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Symbioz.World.Providers.Items
{
    class EffectGenerationProvider
    {
        static Logger logger = new Logger();

        private static Dictionary<EffectGenerationAttribute, MethodInfo> Handlers = typeof(EffectGenerationProvider).MethodsWhereAttributes<EffectGenerationAttribute>();


        public static bool IsHandled(ushort effectId)
        {
            return Handlers.FirstOrDefault(x => (ushort)x.Key.Effect == effectId).Value != null;
        }

        public static Effect Handle(EffectInstance instance)
        {
            var handler = Handlers.FirstOrDefault(x => x.Key.Effect == (EffectsEnum)instance.EffectId);

            if (handler.Value != null)
            {
                return (Effect)handler.Value.Invoke(null, new object[] { instance });
            }
            else
            {
                logger.Error(instance.EffectId + " cannot be handled...");
                return null;
            }
        }

        [EffectGeneration(EffectsEnum.Effect_NonExchangableUntil)]
        public static Effect HandleExchangableOn(EffectInstance instance)
        {
            return null;
        }
        [EffectGeneration(EffectsEnum.Effect_984)]
        public static EffectInteger Handle983(EffectInstance instance)
        {
            return null;
        }



        [EffectGeneration(EffectsEnum.Effect_DamageWater)]
        [EffectGeneration(EffectsEnum.Effect_DamageNeutral)]
        [EffectGeneration(EffectsEnum.Effect_DamageFire)]
        [EffectGeneration(EffectsEnum.Effect_DamageAir)]
        [EffectGeneration(EffectsEnum.Effect_DamageEarth)]
        [EffectGeneration(EffectsEnum.Effect_StealHPWater)]
        [EffectGeneration(EffectsEnum.Effect_StealHPEarth)]
        [EffectGeneration(EffectsEnum.Effect_StealHPAir)]
        [EffectGeneration(EffectsEnum.Effect_StealHPFire)]
        [EffectGeneration(EffectsEnum.Effect_StealHPNeutral)]
        [EffectGeneration(EffectsEnum.Effect_RemoveAP)]
        [EffectGeneration(EffectsEnum.Effect_RemainingFights)]
        [EffectGeneration(EffectsEnum.Effect_StealKamas)]
        [EffectGeneration(EffectsEnum.Effect_HealHP_108)]
        public static EffectDice HandleWeaponDamage(EffectInstance instance)
        {
            return new EffectDice(instance.EffectId, instance.DiceMin, instance.DiceMax, (ushort)instance.Value);
        }
        /// <summary>
        /// Min & Max = Utilisation Actuelle
        /// Value = Utilisation Max
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [EffectGeneration(EffectsEnum.Effect_RemainingEtheral)]
        public static EffectDice HandleEffectEtheral(EffectInstance instance)
        {
            return new EffectDice(instance.EffectId, instance.DiceMax, instance.DiceMax, instance.DiceMax);
        }

        [EffectGeneration(EffectsEnum.Effect_PetMonsterFeed)]
        public static Effect MonsterFeedFightsDatas(EffectInstance instance)
        {
            return null;
        }
        [EffectGeneration(EffectsEnum.Effect_SpawnMonster)]
        public static EffectDice SpawnMonter(EffectInstance instance)
        {
            return new EffectDice(instance.EffectId, instance.DiceMin, instance.DiceMax, instance.DiceMax);
        }

    }
}
