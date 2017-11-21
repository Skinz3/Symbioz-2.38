using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Tools.D2P
{
    public class D2PVirtualFile
    {
        public D2PVirtualFile(string name, byte[] content)
        {
            this.Name = name;
            this.Content = content;
        }
        public string Name { get; set; }

        public byte[] Content { get; set; }
    }
}
