using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Items.d2o", "Weapon"), Table("Weapons")]
    public class Weapons  //: ID2OTable
    {
        [D2OField("id"), Primary]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("craftXpRatio")]
        public short CraftXpRatio;

        [D2OField("range")]
        public short MaxRange;

        [D2OField("criticalHitBonus")]
        public short CriticalHitBonus;

        //[D2OField("criteriaTarget")]
        //public string CriteriaTarget;

        [D2OField("minRange")]
        public short MinRange;

        [D2OField("maxCastPerTurn")]
        public short MaxCastPerTurn;

        //[D2OField("recipeIds")]
        //public List<int> RecipeIds = new List<int>();

        [D2OField("etheral")]
        public bool Etheral;

        [D2OField("appearanceId")]
        public int AppearanceId;

        //[D2OField("dropMonsterIds")]
        //public List<ushort> DropMonsterIds = new List<ushort>();

        [D2OField("level")]
        public ushort Level;

        [D2OField("exchangeable")]
        public bool Exchangeable;

        [D2OField("realWeight")]
        public short RealWeight;

        [D2OField("castTestLos")]
        public bool CastTestLos;

        [D2OField("criteria")]
        public string Criteria;

        [D2OField("criticalHitProbability")]
        public short CritiablHitProbability;

        [D2OField("twoHanded")]
        public bool TwoHanded;

        [D2OField("itemSetId")]
        public int ItemSetId;

        [D2OField("castInDiagonal")]
        public bool CastInDiagonal;

        [D2OField("price")]
        public uint Price;

        [D2OField("apCost")]
        public short ApCost;

        [D2OField("castInLine")]
        public bool CastInLine;

        [D2OField("possibleEffects")]
        public string Effects;

        [D2OField("typeId")]
        public ushort TypeId;






    }
}
