using Symbioz.Core;
using Symbioz.World.Network;
using Symbioz.World.Providers;
using Symbioz.World.Providers.Delayed;
using Symbioz.World.Records;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models
{
    public class DelayedAction
    {
        public DelayedActionRecord Record
        {
            get;
            private set;
        }
        public DelayedActionEnum Action
        {
            get;
            private set;
        }
        public ActionTimer Timer
        {
            get;
            private set;
        }

        public object Value
        {
            get;
            set;
        }

        public DelayedAction(DelayedActionRecord record)
        {
            this.Record = record;
            this.Timer = new ActionTimer(record.Interval * 1000, Execute, true);
            this.Action = (DelayedActionEnum)Enum.Parse(typeof(DelayedActionEnum), Record.ActionType);
        }
        public void Launch()
        {
            this.Timer.Start();
        }
        public void Execute()
        {
            try
            {
                DelayedActionsProvider.Handle(this);

            }
            catch (Exception ex)
            {
                Logger.Write<DelayedAction>("DelayedActionId: (" + Record.Id + "): " + ex, ConsoleColor.Red);
            }
        }
    }
}
