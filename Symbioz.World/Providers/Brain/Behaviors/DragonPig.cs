using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("DragonPig")]
    public class DragonPig : Behavior
    {
        public const ushort LethalBlunderSpellId = 228;

        private SpellRecord LethalBlunderRecord
        {
            get;
            set;
        }
        public DragonPig(BrainFighter fighter)
            : base(fighter)
        {
            this.LethalBlunderRecord = SpellRecord.GetSpellRecord(LethalBlunderSpellId);
            this.Fighter.Fight.FightStartEvt += Fight_FightStart;
        }

        void Fight_FightStart(Fight fight)
        {
        }

    }
}
