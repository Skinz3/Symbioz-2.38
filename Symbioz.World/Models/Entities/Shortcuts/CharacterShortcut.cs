using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Entities.Shortcuts
{
    public abstract class CharacterShortcut
    {
        public sbyte SlotId
        {
            get;
            set;
        }

        public CharacterShortcut()
        {

        }
        public abstract Shortcut GetShortcut();

    }
}
