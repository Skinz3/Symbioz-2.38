using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Attributes
{
    public class D2OAttribute : Attribute
    {
        public string FileName { get; set; }

        public string Module { get; set; }

        public D2OAttribute(string filename,string module)
        {
            this.FileName = filename;
            this.Module = module;
        }
        public override string ToString()
        {
            return FileName + ":" + Module;
        }

    }
}
