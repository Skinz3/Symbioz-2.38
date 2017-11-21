using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Symbioz.World.Providers.Maps.Npcs
{
    public class NpcReplyAttribute : Attribute
    { 
        public string Identifier { get; set; }

        public NpcReplyAttribute(string identifier)
        {
            this.Identifier = identifier;
        }
    }
}
