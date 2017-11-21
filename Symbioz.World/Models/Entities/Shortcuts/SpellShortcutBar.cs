using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;

namespace Symbioz.World.Models.Entities.Shortcuts
{
    public class SpellShortcutBar : ShortcutBar
    {
        public SpellShortcutBar(Character character) : base(character)
        {
        }

        public override ShortcutBarEnum BarEnum
        {
            get
            {
                return ShortcutBarEnum.SPELL_SHORTCUT_BAR;
            }
        }

        public override List<CharacterShortcut> GetShortcuts()
        {
            return Character.Record.Shortcuts.FindAll(x => x is CharacterSpellShortcut);
        }
        public void Remove(ushort spellId)
        {
            var shortcut = GetShortcuts().OfType<CharacterSpellShortcut>().FirstOrDefault(x => x.SpellId == spellId);

            if (shortcut != null)
            {
                Shortcuts.Remove(shortcut);
                Character.Record.Shortcuts.Remove(shortcut);
                Character.Client.Send(new ShortcutBarRemovedMessage((sbyte)BarEnum, shortcut.SlotId));
            }
        }
        public void Add(ushort spellId)
        {
            sbyte slotId = GetFreeSlotId();
            base.Add(new CharacterSpellShortcut(slotId, spellId));
        }

      
    }
}
