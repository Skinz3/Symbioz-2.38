using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Actions
{
    public class BrainAttribute : Attribute
    {
        public ActionType ActionType
        {
            get;
            set;
        }

        public BrainAttribute(ActionType actionType)
        {
            this.ActionType = actionType;
        }
    }
}
