using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Symbioz.World.Providers.Criterias;
using Symbioz.World.Models.Fights.Results;

namespace Symbioz.World.Providers.Fights.Results
{
    public class CustomFightLootProvider
    {
        private static Dictionary<CustomLootAttribute, Type> LootProviders = new Dictionary<CustomLootAttribute, Type>();

        [StartupInvoke(StartupInvokePriority.Eighth)]
        public static void Intialize()
        {
            foreach (var type in Program.WorldAssembly.GetTypes())
            {
                CustomLootAttribute attribute = type.GetCustomAttribute<CustomLootAttribute>();

                if (attribute != null)
                {
                    LootProviders.Add(attribute, type);
                }
            }
        }
        public static void Apply(FightPlayerResult result)
        {
            foreach (var provider in LootProviders)
            {
                if (result.Fight.FightType == provider.Key.FightType && CriteriaProvider.EvaluateCriterias(result.Character.Client, provider.Key.Criteria))
                {
                    CustomLoot customLoot = (CustomLoot)Activator.CreateInstance(provider.Value, result);
                    customLoot.Apply();
                }
            }
        }
    }
}
