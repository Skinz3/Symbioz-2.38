using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Types;

namespace Symbioz.World.Models.Entities.Shortcuts
{
    public class CharacterSpellShortcut : CharacterShortcut
    {
        public ushort SpellId
        {
            get;
            set;
        }
        public CharacterSpellShortcut()
        {
        }
        public CharacterSpellShortcut(sbyte slotId, ushort spellId)
        {
            this.SlotId = slotId;
            this.SpellId = spellId;

        }
        public override Shortcut GetShortcut()
        {
            return new ShortcutSpell(SlotId, SpellId);
        }
    }
}
