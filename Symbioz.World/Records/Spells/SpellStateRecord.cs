using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Spells
{
    [Table("SpellsStates")]
    public class SpellStateRecord : ITable
    {
        public static List<SpellStateRecord> SpellsStates = new List<SpellStateRecord>();

        [Primary]
        public short Id;

        public string Name;

        public bool PreventsSpellCast;

        public bool PreventsFight;

        public bool IsSilent;

        public bool CantBeMoved;

        public bool CantBePushed;

        public bool CantDealDamage;

        public bool Invulnerable;

        public bool CantSwitchPosition;

        public bool Incurable;

        public bool InvulnerableMelee;

        public bool InvulnerableRange;

        public SpellStateRecord(short id, string name, bool preventspellCast, bool preventFight, bool isSilent, bool cantBeMoved, bool cantBePushed, bool cantDeadDamage, bool invulnerable,
            bool cantSwitchPosition, bool incurable, bool invulnerableMelee, bool invulnerableRange)
        {
            this.Id = id;
            this.Name = name;
            this.PreventsSpellCast = preventspellCast;
            this.PreventsFight = preventFight;
            this.IsSilent = isSilent;
            this.CantBeMoved = cantBeMoved;
            this.CantBePushed = cantBePushed;
            this.CantDealDamage = cantDeadDamage;
            this.Invulnerable = invulnerable;
            this.CantSwitchPosition = cantSwitchPosition;
            this.Incurable = incurable;
            this.InvulnerableMelee = invulnerableMelee;
            this.InvulnerableRange = invulnerableRange;
        }

        public static SpellStateRecord GetState(int id)
        {
            return SpellsStates.Find(x => x.Id == id);
        }
    }
}
