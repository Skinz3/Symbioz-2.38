using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    public class BehaviorAttribute : Attribute
    {
        public string Identifier
        {
            get;
            set;
        }
        public BehaviorAttribute(string identifier)
        {
            this.Identifier = identifier;
        }
    }
}
