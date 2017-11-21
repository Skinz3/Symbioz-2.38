using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Idols.d2o", "Idol"), Table("Idols")]
    public class Idols// : ID2OTable
    {
        [D2OField("id"), Primary]
        public int Id;

        [D2OField("description"), i18n]
        public string Description;

        [D2OField("categoryId")]
        public int CategoryId;

        [D2OField("itemId")]
        public int ItemId;

        [D2OField("groupOnly")]
        public bool GroupOnly;

        [D2OField("score")]
        public int Score;

        [D2OField("experienceBonus")]
        public int ExperienceBonus;

        [D2OField("dropBonus")]
        public int DropBonus;

        [D2OField("spellPairId")]
        public int SpellPairId;

        [D2OField("synergyIdolsIds")]
        public List<double> SynergyIdolsIds;

        [D2OField("synergyIdolsCoeff")]
        public List<double> SynergyIdolsCoeff;

        [D2OField("incompatibleMonsters")]
        public List<ushort> IncompatibleMonsters;


    }
}
