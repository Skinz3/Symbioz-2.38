
/*using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using Symbioz.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Entities.Shortcuts
{
    public class SpellShortcutBar
    {
        private List<SpellShortcutRecord> m_shortcuts = new List<SpellShortcutRecord>();

        private Character m_character { get; set; }

        public SpellShortcutBar(Character character, List<SpellShortcutRecord> charactersShortcuts)
        {
            this.m_shortcuts = charactersShortcuts;
            this.m_character = character;
        }

        public void SwapShorcut(sbyte firstSlot, sbyte secondSlot)
        {
            var shortcut1 = GetShorcut(firstSlot);
            var shortcut2 = GetShorcut(secondSlot);

            if (shortcut1 != null && shortcut2 != null)
            {
                shortcut1.SlotId = shortcut1.SlotId == firstSlot ? secondSlot : firstSlot;
                shortcut2.SlotId = shortcut2.SlotId == firstSlot ? secondSlot : firstSlot;

                shortcut2.UpdateElement();
            }
            else if (shortcut1 != null)
            {
                shortcut1.SlotId = secondSlot;

            }

            shortcut1.UpdateElement();

            Refresh();
        }
        private void RefreshShortcut(SpellShortcutRecord record)
        {
            m_character.Client.Send(new ShortcutBarRefreshMessage((sbyte)ShortcutBarEnum.SPELL_SHORTCUT_BAR, record.GetShorctutSpell()));
        }
        public void Refresh()
        {
            Shortcut[] spells = m_shortcuts.ConvertAll<Shortcut>(x => x.GetShorctutSpell()).ToArray();
            m_character.Client.Send(new ShortcutBarContentMessage((sbyte)ShortcutBarEnum.SPELL_SHORTCUT_BAR, spells));
        }
        private SpellShortcutRecord GetShorcut(sbyte slotid)
        {
            return m_shortcuts.Find(x => x.SlotId == slotid);
        }
        public void RemoveShortcut(sbyte slot)
        {
            SpellShortcutRecord record = GetShorcut(slot);
            record.RemoveElement();
            m_shortcuts.Remove(record);

            m_character.Client.Send(new ShortcutBarRemovedMessage((sbyte)ShortcutBarEnum.SPELL_SHORTCUT_BAR, slot));
        }
        public void Add(ushort spellId)
        {
            sbyte slotId = GetFreeSlotId();
            this.Add(slotId, spellId);
        }
        public void Add(sbyte slotId, ushort spellId)
        {
            SpellShortcutRecord shortcut = m_shortcuts.Find(x => x.SlotId == slotId || x.SpellId == spellId);

            if (shortcut != null)
                RemoveShortcut(shortcut.SlotId);

            SpellShortcutRecord record = new SpellShortcutRecord(SpellShortcutRecord.PopNextId(), m_character.Id, spellId, slotId);
            record.AddElement();
            m_shortcuts.Add(record);
            RefreshShortcut(record);
        }
        public sbyte GetFreeSlotId()
        {
            return m_shortcuts.DynamicUId(x => x.SlotId, 0);
        }
    }
}*/
