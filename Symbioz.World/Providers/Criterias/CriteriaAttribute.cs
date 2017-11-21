using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias
{
    public class CriteriaAttribute : Attribute
    {
        public string Identifier { get; set; }

        public CriteriaAttribute(string indentifier)
        {
            this.Identifier = indentifier;
        }
    }
}
