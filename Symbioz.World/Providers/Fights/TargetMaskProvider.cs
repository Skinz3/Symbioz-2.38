using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;

namespace Symbioz.World.Providers.Fights
{
    class TargetMaskProvider
    {
        static Logger logger = new Logger();

        private static Dictionary<TargetMaskAttribute, MethodInfo> Handlers = typeof(TargetMaskProvider).MethodsWhereAttributes<TargetMaskAttribute>();

        public static Fighter[] Handle(Fighter fighter, string identifier)
        {
            var handler = Handlers.FirstOrDefault(x => identifier.StartsWith(x.Key.Identifier));

            if (handler.Value != null)
            {
                string value = identifier.Remove(0, handler.Key.Identifier.Length);
                return (Fighter[])handler.Value.Invoke(null, new object[] { fighter, value });
            }
            else
            {
             
                return new Fighter[0];
            }
        }


        [TargetMask("A")]
        public static Fighter[] AllEnemies(Fighter fighter, string value)
        {
            return fighter.OposedTeam().GetFighters().ToArray();
        }

        [TargetMask("a")]
        public static Fighter[] AllAllies(Fighter fighter, string value)
        {
            return fighter.Team.GetFighters().ToArray();
        }

        [TargetMask("j")]
        public static Fighter[] AllSummons(Fighter fighter, string value)
        {
            return fighter.Fight.GetSummons();
        }

        [TargetMask("M")]
        public static Fighter[] Monsters(Fighter fighter, string value)
        {
            return Array.FindAll(AllEnemies(fighter, value), x => x is MonsterFighter);
        }

        [TargetMask("g")]
        public static Fighter[] Allies(Fighter fighter, string value)
        {
            var fighters = fighter.Team.GetFighters();
            fighters.Remove(fighter);
            return fighters.ToArray();
        }

        [TargetMask("i")]
        public static Fighter[] Invocations(Fighter fighter, string value)
        {
            return fighter.Fight.GetSummons();
        }
    }
}
