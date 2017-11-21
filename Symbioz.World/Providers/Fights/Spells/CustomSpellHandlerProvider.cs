using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;
using System.Reflection;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using Symbioz.World.Models.Maps;
using Symbioz.Core.DesignPattern.StartupEngine;

namespace Symbioz.World.Providers.Fights.Spells
{
    class CustomSpellHandlerProvider
    {
        private static Dictionary<CustomSpellHandlerAttribute[], Type> Handlers = new Dictionary<CustomSpellHandlerAttribute[], Type>();

        [StartupInvoke(StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var type in Program.WorldAssembly.GetTypes())
            {
                CustomSpellHandlerAttribute[] handlers = type.GetCustomAttributes<CustomSpellHandlerAttribute>().ToArray();

                if (handlers.Length > 0)
                {
                    Handlers.Add(handlers, type);
                }
            }
        }

        public static bool IsHandled(ushort spellId)
        {
            foreach (var handler in Handlers)
            {
                if (handler.Key.FirstOrDefault(x => x.SpellId == spellId) != null)
                    return true;
            }
            return false;
        }
        public static void Handle(Fighter fighter, SpellLevelRecord level, MapPoint castPoint, bool criticalHit)
        {
            var handler = Handlers.FirstOrDefault(x => x.Key.FirstOrDefault(w => w.SpellId == level.SpellId) != null);

            if (handler.Value != null)
            {
                var obj = (CustomSpellHandler)Activator.CreateInstance(handler.Value, new object[] { fighter, level, castPoint, criticalHit });
                obj.Execute();
            }
        }
    }
}
