using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.D2OTypes
{
   public class D2oTypeAttribute : Attribute
    {
       public string Type { get; set; }

       public D2oTypeAttribute(string type)
       {
           this.Type = type;
       }
    }
}
