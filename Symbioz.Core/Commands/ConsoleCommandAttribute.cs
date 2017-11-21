using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core.Commands
{
    public class ConsoleCommandAttribute : Attribute
    {
        public string Name { get; set; }

        public ConsoleCommandAttribute(string name)
        {
            this.Name = name;
        }
    }
}
