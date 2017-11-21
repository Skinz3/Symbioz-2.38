using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Types;

namespace Symbioz.World.Models.Entities.Shortcuts
{
    public class CharacterItemShortcut : CharacterShortcut
    {
        public CharacterItemShortcut(sbyte slotId, int itemUId, short itemGId)
        {
            this.SlotId = slotId;
            this.ItemUId = itemUId;
            this.ItemGId = itemGId;
        }
        public CharacterItemShortcut()
        {

        }
        public int ItemUId
        {
            get;
            set;
        }
        public short ItemGId
        {
            get;
            set;
        }

        public override Shortcut GetShortcut()
        {
            return new ShortcutObjectItem(SlotId, ItemUId, ItemGId);
        }
    }
}
