using Symbioz.Core.DesignPattern.StartupEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Symbioz.Core;
using Symbioz.World.Models.Fights.Fighters;

namespace Symbioz.World.Providers.Brain.Actions
{
    class BrainProvider
    {
        static Logger logger = new Logger();

        private static Dictionary<ActionType, Type> Brains = new Dictionary<ActionType, Type>();

        [StartupInvoke("Brain", StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var type in Program.WorldAssembly.GetTypes())
            {
                if (type.BaseType == typeof(BrainAction))
                {
                    IEnumerable<BrainAttribute> attributes = type.GetCustomAttributes<BrainAttribute>();

                    foreach (var attribute in attributes)
                    {
                        Brains.Add(attribute.ActionType, type);
                    }
                    logger.Gray(type.Name + " loaded");
                }
            }
        }
        public static BrainAction GetAction(BrainFighter fighter, ActionType actionType)
        {
            return (BrainAction)Activator.CreateInstance(Brains[actionType], new object[] { fighter });
        }
    }
}
