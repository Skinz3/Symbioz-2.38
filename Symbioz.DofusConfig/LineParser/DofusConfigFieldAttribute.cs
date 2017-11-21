using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.DofusConfig.LineParser
{
    public class DofusConfigFieldAttribute : Attribute
    {
        public string FieldName
        {
            get;
            private set;
        }

        public DofusConfigFieldAttribute(string fieldName)
        {
            this.FieldName = fieldName;
        }
    }
}
