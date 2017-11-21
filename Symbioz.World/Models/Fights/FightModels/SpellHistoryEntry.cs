using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.FightModels
{
    public class SpellHistoryEntry
    {
        public SpellHistory History
        {
            get;
            private set;
        }
        public SpellLevelRecord Spell
        {
            get;
            private set;
        }
        public Fighter Caster
        {
            get;
            private set;
        }
        public Fighter Target
        {
            get;
            private set;
        }
        public int CastRound
        {
            get;
            private set;
        }
        public SpellHistoryEntry(SpellHistory history, SpellLevelRecord spell, Fighter caster, Fighter target, int castRound)
        {
            this.History = history;
            this.Spell = spell;
            this.Caster = caster;
            this.Target = target;
            this.CastRound = castRound;
        }
        public int GetElapsedRounds(int currentRound)
        {
            return currentRound - this.CastRound;
        }
        public bool IsGlobalCooldownActive(int currentRound)
        {
            return (long)this.GetElapsedRounds(currentRound) < (long)((ulong)this.Spell.MinCastInterval);
        }
    }
}
