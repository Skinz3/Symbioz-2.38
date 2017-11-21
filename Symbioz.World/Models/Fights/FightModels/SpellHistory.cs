using Symbioz.Core;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.FightModels
{
    public class SpellHistory
    {
        public static readonly int HistoryEntriesLimit = 60;

        private readonly LimitedStack<SpellHistoryEntry> m_underlyingStack = new LimitedStack<SpellHistoryEntry>(HistoryEntriesLimit);

        public Fighter Owner
        {
            get;
            private set;
        }
        private int CurrentRound
        {
            get
            {
                return (int)this.Owner.Fight.TimeLine.RoundNumber;
            }
        }
        public SpellHistory(Fighter owner)
        {
            this.Owner = owner;
        }
        public void RegisterCastedSpell(SpellHistoryEntry entry)
        {
            this.m_underlyingStack.Push(entry);
        }
        public void RegisterCastedSpell(SpellLevelRecord spell, Fighter target)
        {
            this.RegisterCastedSpell(new SpellHistoryEntry(this, spell, this.Owner, target, this.CurrentRound));
        }
        public bool CanCastSpell(SpellLevelRecord spell,short targetedCell)
        {
            SpellHistoryEntry spellHistoryEntry = this.m_underlyingStack.LastOrDefault((SpellHistoryEntry entry) => entry.Spell.Id == spell.Id);
            bool result;
            if (spellHistoryEntry == null && (long)this.CurrentRound < (long)((ulong)spell.InitialCooldown))
            {
                result = false;
            }
            else if (spellHistoryEntry == null)
            {
                result = true;
            }
            else if (spellHistoryEntry.IsGlobalCooldownActive(this.CurrentRound))
            {
                result = false;
            }
            else
            {
                SpellHistoryEntry[] array = (
                    from entry in this.m_underlyingStack
                    where entry.Spell.Id == spell.Id && entry.CastRound == this.CurrentRound
                    select entry).ToArray<SpellHistoryEntry>();
                if (array.Length == 0)
                {
                    result = true;
                }
                else if (spell.MaxCastPerTurn > 0u && (long)array.Length >= (long)((ulong)spell.MaxCastPerTurn))
                {
                    result = false;
                }
                else
                {
                    Fighter target = this.Owner.Fight.GetFighter(targetedCell);
                    if (target == null)
                    {
                        result = true;
                    }
                    else
                    {
                        int num = array.Count((SpellHistoryEntry entry) => entry.Target != null && entry.Target.Id == target.Id);
                        result = (spell.MaxCastPerTarget <= 0u || (long)num < (long)((ulong)spell.MaxCastPerTarget));
                    }
                }
            }
            return result;
        }
        public bool CanCastSpell(SpellLevelRecord spell)
        {
            SpellHistoryEntry spellHistoryEntry = this.m_underlyingStack.LastOrDefault((SpellHistoryEntry entry) => entry.Spell.Id == spell.Id);
            bool result;
            if (spellHistoryEntry == null && (long)this.CurrentRound < (long)((ulong)spell.InitialCooldown))
            {
                result = false;
            }
            else if (spellHistoryEntry == null)
            {
                result = true;
            }
            else if (spellHistoryEntry.IsGlobalCooldownActive(this.CurrentRound))
            {
                result = false;
            }
            else
            {
                SpellHistoryEntry[] array = (
                    from entry in this.m_underlyingStack
                    where entry.Spell.Id == spell.Id && entry.CastRound == this.CurrentRound
                    select entry).ToArray<SpellHistoryEntry>();
                result = (array.Length == 0 || spell.MaxCastPerTurn <= 0u || (long)array.Length < (long)((ulong)spell.MaxCastPerTurn));
            }
            return result;
        }
    }
}
