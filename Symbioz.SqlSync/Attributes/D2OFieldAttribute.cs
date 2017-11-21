using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Attributes
{
    public class D2OFieldAttribute : Attribute
    {
        public string FieldName { get; set; }

        public D2OFieldAttribute(string fieldName)
        {
            this.FieldName = fieldName;
        }
    }
}
