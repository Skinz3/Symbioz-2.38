using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Tools.D2O
{    
    public class DataClass
    {
        // Properties
        public object this[string Name]
        {
            get
            {
                if (!this.Fields.ContainsKey(Name))
                {
                    return null;
                }
                return this.Fields[Name];
            }
        }

        // Fields
        public Dictionary<string, object> Fields = new Dictionary<string, object>();
        public string Name;
    }


}
