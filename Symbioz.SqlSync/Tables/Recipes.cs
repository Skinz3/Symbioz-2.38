using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Recipes.d2o", "Recipe"), Table("Recipes")]
    public class Recipes  //: ID2OTable
    {
        [D2OField("resultId")]
        public ushort ResultId;

        [D2OField("resultNameId"), i18n]
        public string ResultName;

        [D2OField("resultTypeId")]
        public short ResultTypeId;

        [D2OField("resultLevel")]
        public short ResultLevel;

        [D2OField("ingredientIds")]
        public List<ushort> IngredientIds = new List<ushort>();

        [D2OField("quantities")]
        public List<ushort> Quantities = new List<ushort>();

        [D2OField("jobId")]
        public ushort JobId;

        [D2OField("skillId")]
        public ushort SkillId;
    }
}
