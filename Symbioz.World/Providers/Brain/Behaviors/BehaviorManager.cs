
using Symbioz.Core.DesignPattern.StartupEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Symbioz.Core;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Models.Fights.Fighters;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    public class BehaviorManager
    {
        private static Logger logger = new Logger();

        private static Dictionary<string, Type> BehaviorTypes = new Dictionary<string, Type>();

        [StartupInvoke("Behaviors", StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var type in Program.WorldAssembly.GetTypes())
            {
                BehaviorAttribute attribute = type.GetCustomAttribute<BehaviorAttribute>();

                if (attribute != null)
                {
                    BehaviorTypes.Add(attribute.Identifier, type);
                }
            }
        }

        public static Behavior GetBehavior(string behaviorName, BrainFighter fighter)
        {
            var data = BehaviorTypes.FirstOrDefault(x => x.Key == behaviorName);

            if (data.Value != null)
            {
                return (Behavior)Activator.CreateInstance(data.Value, fighter);
            }
            else
            {
                logger.Error("Unable to handle beahvior identifier: " + behaviorName);
                return null;
            }
        }
        public static bool Exist(string behaviorName)
        {
            return BehaviorTypes.ContainsKey(behaviorName);
        }
    }
}
