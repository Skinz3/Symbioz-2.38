using Symbioz.ORM;
using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Breeds
{
    [Table("BreedsSpells")]
    public class BreedSpellRecord : ITable
    {
        public static List<BreedSpellRecord> BreedsSpells = new List<BreedSpellRecord>();

        public ushort SpellId;

        public sbyte BreedId;

        public ushort ObtainLevel;

        public BreedSpellRecord(ushort spellid, sbyte breedId, ushort obtainLevel)
        {
            this.SpellId = spellid;
            this.BreedId = breedId;
            this.ObtainLevel = obtainLevel;
        }

        public static List<BreedSpellRecord> GetBreedSpells(sbyte breedId)
        {
            return BreedsSpells.FindAll(x => x.BreedId == breedId);
        }
       
    }
}
