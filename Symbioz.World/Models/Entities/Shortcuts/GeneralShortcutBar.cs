using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Enums;
using Symbioz.World.Records.Characters;

namespace Symbioz.World.Models.Entities.Shortcuts
{
    public class GeneralShortcutBar : ShortcutBar
    {
        public GeneralShortcutBar(Character character) : base(character)
        {

        }

        public override ShortcutBarEnum BarEnum
        {
            get
            {
                return ShortcutBarEnum.GENERAL_SHORTCUT_BAR;
            }
        }

        public override List<CharacterShortcut> GetShortcuts()
        {
            return Character.Record.Shortcuts.FindAll(x => !(x is CharacterSpellShortcut));
        }

        public void OnItemRemoved(CharacterItemRecord obj)
        {
            var shortcut = GetItemShortcut(obj.UId);

            if (shortcut != null)
            {
                RemoveShortcut(shortcut.SlotId);
            }
        }

        private CharacterItemShortcut GetItemShortcut(uint itemUId)
        {
            return Shortcuts.OfType<CharacterItemShortcut>().FirstOrDefault(x => x.ItemUId == itemUId);
        }
    }
}
