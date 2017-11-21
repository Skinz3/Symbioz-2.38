using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using Symbioz.Core;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias
{
    class CriteriaProvider
    {
        public const char OR_SPLITTER = '|';

        public const char AND_SPLITTER = '&';

        public static Dictionary<string, Type> CriteriaTypes = new Dictionary<string, Type>();

        [StartupInvoke("Criterias", StartupInvokePriority.Eighth)]
        public static void Intialize()
        {
            foreach (var type in Program.WorldAssembly.GetTypes().WithAttributes(typeof(CriteriaAttribute)))
            {
                var attribute = type.GetCustomAttribute<CriteriaAttribute>();
                CriteriaTypes.Add(attribute.Identifier, type);
            }
        }
        private static bool Evaluate(WorldClient client, string criteria)
        {
            if (criteria == null || criteria == string.Empty)
                return true;
            string criteriaIndentifier = new string(criteria.Take(2).ToArray());
            var criteriaHandler = CriteriaTypes.FirstOrDefault(x => x.Key == criteriaIndentifier);
            if (criteriaHandler.Value != null)
            {
                var criteriaObj = Activator.CreateInstance(criteriaHandler.Value) as AbstractCriteria;
                criteriaObj.CriteriaFull = criteria;
                if (!criteriaObj.Eval(client))
                    return false;
                else
                    return true;
            }
            else
            {
                client.Character.Reply("Unknown criteria indentifier: " + criteriaIndentifier + ". Skeeping criteria");
                return true;
            }


        }
        public static bool EvaluateCriterias(WorldClient client, string criterias)
        {
            if (criterias.Contains(OR_SPLITTER))
            {
                foreach (var criteria in criterias.Split(OR_SPLITTER))
                {
                    if (Evaluate(client, criteria) == true)
                        return true;

                }
                return false;
            }
            else
            {
                foreach (var criteria in criterias.Split(AND_SPLITTER))
                {
                    if (Evaluate(client, criteria) == false)
                        return false;
                }
                return true;
            }
         

        }
    }
}
