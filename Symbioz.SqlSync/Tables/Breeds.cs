using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Breeds.d2o", "Breed"), Table("Breeds")]
    public class Breeds  //: ID2OTable
    {
        [D2OField("id"), Primary]
        public int Id;

        [D2OField("shortNameId"), i18n]
        public string Name;

        [D2OField("maleLook")]
        public string MaleLook;

        [D2OField("femaleLook")]
        public string FemaleLook;

        [D2OField("maleColors")]
        public List<int> MaleColors = new List<int>();

        [D2OField("femaleColors")]
        public List<int> FemaleColors = new List<int>();

        [D2OField("statsPointsForIntelligence")]
        public string SPForIntelligence;

        [D2OField("statsPointsForAgility")]
        public string SPForAgility;

        [D2OField("statsPointsForStrength")]
        public string SPForStrength;

        [D2OField("statsPointsForVitality")]
        public string SPForVitality;

        [D2OField("statsPointsForWisdom")]
        public string SPForWisdom;

        [D2OField("statsPointsForChance")]
        public string SPForChance;

        [D2OField("breedSpellsId")]
        public List<ushort> BreedSpells = new List<ushort>();
    }
}
