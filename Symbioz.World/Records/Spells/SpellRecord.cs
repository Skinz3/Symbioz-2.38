using Symbioz.ORM;
using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Spells
{
    [Table("Spells", true, 5)]
    public class SpellRecord : ITable
    {
        public static List<SpellRecord> Spells = new List<SpellRecord>();

        [Primary]
        public ushort Id;

        public string Name;

        public string Description;

        public List<int> SpellsLevels;

        [Ignore]
        public List<SpellLevelRecord> Levels;

        [Update]
        public sbyte Category;

        [Ignore]
        public SpellCategoryEnum CategoryEnum
        {
            get
            {
                return (SpellCategoryEnum)Category;
            }
            set
            {
                Category = (sbyte)value;
            }
        }

        public SpellLevelRecord GetLevel(sbyte grade)
        {
            return Levels.Find(x => x.Grade == grade);
        }
        public SpellLevelRecord GetLastLevel()
        {
            return Levels.Last();
        }
        public sbyte GetLastLevelGrade()
        {
            return GetLastLevel().Grade;
        }

        public SpellRecord(ushort id, string name, string description, List<int> spellLevels, sbyte category)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.SpellsLevels = spellLevels;
            this.Levels = SpellLevelRecord.GetSpellLevels(Id);
            this.Category = category;
        }

        public static SpellRecord GetSpellRecord(ushort id)
        {
            return Spells.Find(x => x.Id == id);
        }
    }
}
