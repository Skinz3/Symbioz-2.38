using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Symbioz.Core;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Models.Effects;

namespace Symbioz.World.Providers.Fights
{
    public class FightLookProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fighter">Fighter.??</param>
        /// <param name="currentLook">Clone() du Fighter.RealLook</param>
        /// <returns>Le nouveau Fighter.Look</returns>
        public delegate ContextActorLook CustomLookHandlerDel(Fighter fighter, ContextActorLook currentLook, EffectInstance effect);
        /// <summary>
        /// Key = SpellId
        /// </summary>
        public static Dictionary<ushort, CustomLookHandlerDel> Handlers = new Dictionary<ushort, CustomLookHandlerDel>();

        [StartupInvoke(StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var method in typeof(FightLookProvider).GetMethods())
            {
                var attributes = method.GetCustomAttributes<SpellLookAttribute>();

                foreach (var attribute in attributes)
                {
                    Handlers.Add(attribute.SpellId, (CustomLookHandlerDel)method.CreateDelegate(typeof(CustomLookHandlerDel)));
                }

            }

        }
        public static bool Exist(ushort spellId)
        {
            return Handlers.FirstOrDefault(x => x.Key == spellId).Value != null;
        }
        public static ContextActorLook TransformLook(Fighter fighter, ContextActorLook look, ushort spellid, EffectInstance effect)
        {
            var handler = Handlers.FirstOrDefault(x => x.Key == spellid);
            if (handler.Value != null)
                return handler.Value(fighter, look, effect);
            else
                return null;

        }
        /// <summary>
        /// Masque de Classe
        /// </summary>
        /// <param name="fighter"></param>
        /// <returns></returns>
        [SpellLook(2872)]
        public static ContextActorLook BreedMask(Fighter fighter, ContextActorLook look, EffectInstance effect)
        {
            look.RemoveSkin(1450);
            look.RemoveSkin(1449);
            look.RemoveSkin(1443);
            look.RemoveSkin(1448);
            if (!look.IsRiding)
                look.SetBones(1);
            return look;
        }
        /// <summary>
        /// Picole
        /// </summary>
        /// <param name="fighter"></param>
        /// <returns></returns>
        [SpellLook(686)]
        public static ContextActorLook DrunkenLook(Fighter fighter, ContextActorLook look, EffectInstance effect)
        {
            look.SetBones(44);
            return look;
        }
        [SpellLook(701)]
        public static ContextActorLook Zatoïshwanfighter(Fighter fighter, ContextActorLook look, EffectInstance effect)
        {
            if (!look.IsRiding)
                look.SetBones(453);
            else
                look.SetBones(1202);

            if (!look.IsRiding)
                look.SetScale(80);
            else
                look.SetScale(60);
            return look;
        }
        [SpellLook(2879)]
        public static ContextActorLook CowardLook(Fighter fighter, ContextActorLook look, EffectInstance effect)
        {
            if (!look.IsRiding)
                look.SetBones(1576);
            if (fighter.Sex)
            {
                look.AddSkin(1450);
            }
            else
            {
                look.AddSkin(1449);
            }
            return look;
        }
        [SpellLook(2880)]
        public static ContextActorLook PsycopathMask(Fighter fighter, ContextActorLook look, EffectInstance effect)
        {
            if (!look.IsRiding)
                look.SetBones(1575);

            if (fighter.Sex)
            {
                look.AddSkin(1448);
            }
            else
            {
                look.AddSkin(1443);
            }
            return look;
        }
        [SpellLook(6719)]
        [SpellLook(195)]
        public static ContextActorLook SadidaTear(Fighter fighter, ContextActorLook look, EffectInstance effect)
        {
            look.SetBones(3164);
            return look;
        }
        [SpellLook(542)] // Maho Firefoux
        public static ContextActorLook Metamourphose(Fighter fighter, ContextActorLook look, EffectInstance effect)
        {
            return ContextActorLook.BonesLook(1160, 100);
        }
        [SpellLook(7355)]
        public static ContextActorLook FinishBlowAngelTest(Fighter fighter, ContextActorLook look, EffectInstance effect)
        {
            return ContextActorLook.BonesLook(134, 200);
        }

    }
}