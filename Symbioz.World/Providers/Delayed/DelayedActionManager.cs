using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Models;
using Symbioz.World.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Delayed
{
    class DelayedActionManager
    {
        static Logger logger = new Logger();

        public static List<DelayedAction> DelayedActions = new List<DelayedAction>();

        [StartupInvoke("DelayedActions", StartupInvokePriority.Last)]
        public static void Initialize()
        {
            foreach (var record in DelayedActionRecord.DelayedActions)
            {
                DelayedActions.Add(new DelayedAction(record));
            }

            foreach (var action in DelayedActions)
            {
                action.Launch();
            }
        }
        public static DelayedAction GetAction(DelayedActionEnum action, string value1)
        {
            return DelayedActions.FirstOrDefault(x => x.Action == action && x.Record.Value1 == value1);
        }
        public static DelayedAction GetAction(DelayedActionEnum action, string value1, string value2)
        {
            return DelayedActions.FirstOrDefault(x => x.Action == action && x.Record.Value1 == value1 && x.Record.Value2 == value2);
        }
        public static void AddAction(DelayedAction action)
        {
            DelayedActions.Add(action);
            action.Launch();
        }


    }
}
