using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Attributes
{
    public class D2OFieldHandler : Attribute
    {
        public string Field;

        public D2OFieldHandler(string field)
        {
            this.Field = field;
        }
    }
}
