using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights
{
    public class TargetMaskAttribute : Attribute
    {
        public string Identifier
        {
            get;
            set;
        }
        public TargetMaskAttribute(string identifier)
        {
            this.Identifier = identifier;
        }


    }
}
