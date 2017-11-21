using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps.Interactives
{
    public class InteractiveActionAttribute : Attribute
    {
        public string Identifier { get; set; }

        public InteractiveActionAttribute(string identifier)
        {
            this.Identifier = identifier;
        }
    }
}
