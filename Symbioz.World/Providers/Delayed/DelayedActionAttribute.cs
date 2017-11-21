using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Delayed
{
    public class DelayedActionAttribute : Attribute
    {
        public string ActionType { get; set; }

        public DelayedActionAttribute(string actionType)
        {
            this.ActionType = actionType;
        }
    }
}
