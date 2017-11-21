using Symbioz.ORM;
using Symbioz.Protocol.Types;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Idols
{
    [Table("Idols", true, 6)]
    public class IdolRecord : ITable
    {
        public static List<IdolRecord> Idols = new List<IdolRecord>();

        [Primary]
        public ushort Id;
        public string Description;
        public int CategoryId;
        public ushort ItemId;
        public ushort SpellId;
        public sbyte SpellGrade;
        public bool GroupOnly;
        public int Score;
        public ushort ExperienceBonus;
        public ushort DropBonus;
        public List<int> SynergyIdolsIds;
        public List<double> SynergyIdolsCoeff;
        public List<ushort> IncompatibleMonsters;

        [Ignore]
        public SpellLevelRecord SpellLevel;

        public IdolRecord(ushort id, string description, int categoryId, ushort itemId, ushort spellId, sbyte spellGrade, bool groupOnly, int score, ushort experienceBonus, ushort dropBonus,
            List<int> synergyIdolsIds, List<double> synergyIdolsCoeff, List<ushort> incompatibleMonsters)
        {
            this.Id = id;
            this.Description = description;
            this.CategoryId = categoryId;
            this.ItemId = itemId;
            this.GroupOnly = groupOnly;
            this.Score = score;
            this.ExperienceBonus = experienceBonus;
            this.DropBonus = dropBonus;
            this.SpellId = spellId;
            this.SpellGrade = spellGrade;
            this.SynergyIdolsIds = synergyIdolsIds;
            this.SynergyIdolsCoeff = synergyIdolsCoeff;
            this.IncompatibleMonsters = incompatibleMonsters;
            this.SpellLevel = SpellLevelRecord.GetSpellLevel(spellId, spellGrade);
        }

        public PartyIdol GetPartyIdol(ulong[] ownerIds)
        {
            return new PartyIdol(Id, ExperienceBonus, DropBonus, ownerIds);
        }
        public Idol GetIdol()
        {
            return new Idol(Id, ExperienceBonus, DropBonus);
        }

        public static IdolRecord GetIdolFromItemId(ushort itemGId)
        {
            return Idols.FirstOrDefault(x => x.ItemId == itemGId);
        }
        public static IdolRecord GetIdol(ushort id)
        {
            return Idols.FirstOrDefault(x => x.Id == id);
        }
    }
}
