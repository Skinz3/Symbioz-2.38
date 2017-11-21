using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Spells
{
    [Table("SpellsBombs")]
    public class SpellBombRecord : ITable
    {
        public static List<SpellBombRecord> SpellsBombs = new List<SpellBombRecord>();

        public ushort SpellId;

        public ushort ExplosionSpellId;

        public int WallColor;

        public ushort WallSpellId;

        public ushort CibleExplosionSpellId;

        public SpellBombRecord(ushort spellId, ushort explosionSpellId, int wallColor, ushort wallSpellId, ushort cibleExplosionSpellId)
        {
            this.SpellId = spellId;
            this.ExplosionSpellId = explosionSpellId;
            this.WallColor = wallColor;
            this.WallSpellId = wallSpellId;
            this.CibleExplosionSpellId = cibleExplosionSpellId;
        }

        public static SpellBombRecord GetSpellBombRecord(ushort spellId)
        {
            return SpellsBombs.FirstOrDefault(x => x.SpellId == spellId);
        }
    }
}
