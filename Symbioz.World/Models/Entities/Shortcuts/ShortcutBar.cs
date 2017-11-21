using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;
using Symbioz.Protocol.Types;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;

namespace Symbioz.World.Models.Entities.Shortcuts
{
    public abstract class ShortcutBar
    {
        protected List<CharacterShortcut> Shortcuts
        {
            get;
            private set;
        }
        public abstract ShortcutBarEnum BarEnum
        {
            get;
        }
        protected Character Character
        {
            get;
            private set;
        }
        protected UniqueIdProvider IdProvider
        {
            get;
            private set;
        }
        public ShortcutBar(Character character)
        {
            this.Character = character;
            this.Shortcuts = GetShortcuts();
        }
        private CharacterShortcut GetShortcut(sbyte slotId)
        {
            return Shortcuts.Find(x => x.SlotId == slotId);
        }
        public void RemoveShortcut(sbyte slotId)
        {
            var shortcut = GetShortcut(slotId);

            if (shortcut != null)
            {
                Shortcuts.Remove(shortcut);
                Character.Record.Shortcuts.Remove(shortcut);
                Character.Client.Send(new ShortcutBarRemovedMessage((sbyte)BarEnum, slotId));
            }
        }

        public void Add(CharacterShortcut scut)
        {
            if (!CanAdd())
                return;
            var shortcut = GetShortcut(scut.SlotId);

            if (shortcut != null)
                RemoveShortcut(shortcut.SlotId);

            Character.Record.Shortcuts.Add(scut);
            Shortcuts.Add(scut);
            RefreshShortcut(scut);
        }
        public void Swap(sbyte firstSlot, sbyte secondSlot)
        {
            var shortcut1 = GetShortcut(firstSlot);
            var shortcut2 = GetShortcut(secondSlot);

            if (shortcut1 != null && shortcut2 != null)
            {
                shortcut1.SlotId = shortcut1.SlotId == firstSlot ? secondSlot : firstSlot;
                shortcut2.SlotId = shortcut2.SlotId == firstSlot ? secondSlot : firstSlot;
            }
            else if (shortcut1 != null)
            {
                shortcut1.SlotId = secondSlot;
            }

            Refresh();
        }

        private void RefreshShortcut(CharacterShortcut shortcut)
        {
            Character.Client.Send(new ShortcutBarRefreshMessage((sbyte)BarEnum, shortcut.GetShortcut()));
        }

        public void Refresh()
        {
            Character.Client.Send(new ShortcutBarContentMessage((sbyte)BarEnum, Array.ConvertAll(Shortcuts.ToArray(), x => x.GetShortcut())));
        }

        public abstract List<CharacterShortcut> GetShortcuts();


        public static CharacterShortcut GetCharacterShortcut(Character character, Shortcut shortcut)
        {
            if (shortcut is ShortcutObjectItem)
            {
                var shortcutObjectItem = shortcut as ShortcutObjectItem;

                var item = character.Inventory.GetItem((uint)shortcutObjectItem.itemUID);
                if (item != null)
                {
                    return new CharacterItemShortcut(shortcut.slot, shortcutObjectItem.itemUID, (short)item.GId);
                }
                else
                {
                    return null;
                }
            }
            if (shortcut is ShortcutSpell)
            {
                var shortcutSpell = shortcut as ShortcutSpell;

                return new CharacterSpellShortcut(shortcutSpell.slot, shortcutSpell.spellId);
            }
            return null;
        }

        public sbyte GetFreeSlotId()
        {
            for (int i = 0; i < 100; i++)
            {
                var first = Shortcuts.FirstOrDefault(x => x.SlotId == i);

                if (first == null)
                {
                    return (sbyte)i;
                }
            }
            return -1;
        }
        public bool CanAdd()
        {
            return GetFreeSlotId() != -1;
        }
    }
}
