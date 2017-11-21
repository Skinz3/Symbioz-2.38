using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Items
{
    [Table("Recipes", true, 8)]
    public class RecipeRecord : ITable
    {
        public static List<RecipeRecord> Recipes = new List<RecipeRecord>();

        [Primary]
        public ushort ResultId;

        [Ignore]
        public ItemRecord Result;

        public string ResultName;

        public ushort ResultTypeId;

        public ushort ResultLevel;

        public List<ushort> IngredientIds;

        public List<uint> Quantities;

        public sbyte JobId;

        public uint SkillId;

        [Ignore]
        public Dictionary<ushort, uint> Ingredients = new Dictionary<ushort, uint>();

        public RecipeRecord(ushort resultId, string resultName, ushort resultTypeId,
            ushort resultLevel, List<ushort> ingredientIds, List<uint> quantities,
            sbyte jobId, uint skillId)
        {
            this.ResultId = resultId;
            this.ResultName = resultName;
            this.ResultTypeId = resultTypeId;
            this.ResultLevel = resultLevel;
            this.IngredientIds = ingredientIds;
            this.Quantities = quantities;
            this.JobId = jobId;
            this.SkillId = skillId;

            for (int i = 0; i < IngredientIds.Count(); i++)
            {
                Ingredients.Add(IngredientIds[i], Quantities[i]);
            }
            this.Result = ItemRecord.GetItem(resultId);
        }

        public static RecipeRecord GetRecipe(ushort gid)
        {
            return Recipes.Find(x => x.ResultId == gid);
        }
    }
}
