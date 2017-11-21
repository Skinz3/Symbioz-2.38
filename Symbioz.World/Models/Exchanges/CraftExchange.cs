using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Items;
using Symbioz.Core;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Providers;
using Symbioz.World.Models.Entities.Jobs;
using Symbioz.World.Records;

namespace Symbioz.World.Models.Exchanges
{
    public class CraftExchange : AbstractCraftExchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.CRAFT;
            }
        }

        /// <summary>
        /// Id de l'item Rune de signature (todo, osef un peu)
        /// </summary>
        public const ushort SignatureItemId = 7508;

        public CraftExchange(Character character, uint skillId, JobsTypeEnum jobType)
            : base(character, skillId, jobType)
        {

        }
        /// <summary>
        /// FAILLE WPE
        /// </summary>
        /// <param name="gid"></param>
        public void SetRecipe(ushort gid)
        {
            var recipe = RecipeRecord.GetRecipe(gid);

            if (recipe != null && recipe.ResultLevel <= Character.GetJob(JobType).Level)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    CharacterItemRecord item = Character.Inventory.GetFirstItem(ingredient.Key, ingredient.Value);
                    if (item != null)
                    {
                        CraftedItems.AddItem(item, ingredient.Value);
                    }
                }
                SetCount(1);
            }
        }

        public override void Ready(bool ready, ushort step)
        {
            List<CharacterItemRecord> results = new List<CharacterItemRecord>();
            Dictionary<uint, uint> removed = new Dictionary<uint, uint>();

            if (ready)
            {
                var recipe = GetRecipe();

                if (recipe != null && recipe.ResultLevel <= CharacterJob.Level)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        if (Character.IsInExchange(ExchangeTypeEnum.CRAFT))
                        {
                            results.Add(recipe.Result.GetCharacterItem(Character.Id, 1, true)); // True = jet parfait

                            foreach (var ingredient in CraftedItems.GetItems())
                            {
                                if (!removed.ContainsKey(ingredient.UId))
                                    removed.Add(ingredient.UId, ingredient.Quantity);
                                else
                                    removed[ingredient.UId] += ingredient.Quantity;
                            }
                        }
                        else
                            return;
                    }

                    CraftedItems.Clear(false);

                    Character.Inventory.RemoveItems(removed);
                    Character.Inventory.AddItems(results);

                    OnCraftResulted(CraftResultEnum.CRAFT_SUCCESS, results.Last());

                    Character.SendMap(new ExchangeCraftInformationObjectMessage((sbyte)CraftResultEnum.CRAFT_SUCCESS, recipe.ResultId, (ulong)Character.Id));

                    int craftXpRatio = recipe.Result.Weapon ? WeaponRecord.GetWeapon(recipe.ResultId).CraftXpRatio : -1;
                    int exp = FormulasProvider.Instance.GetCraftXpByJobLevel(recipe.ResultLevel, CharacterJob.Level, craftXpRatio);
                    Character.AddJobExp(JobType, (ulong)(exp * Count));
                    SetCount(1);
                }
                else
                {
                    OnCraftResulted(CraftResultEnum.CRAFT_FAILED);
                }
            }
            else
            {
                OnCraftResulted(CraftResultEnum.CRAFT_FAILED);
            }
        }
        private RecipeRecord GetRecipe()
        {
            Dictionary<ushort, uint> ingredients = new Dictionary<ushort, uint>();

            foreach (var item in CraftedItems.GetItems())
            {
                if (!ingredients.ContainsKey(item.GId))
                    ingredients.Add(item.GId, item.Quantity);
            }

            var template = RecipeRecord.Recipes.Find(x => x.Ingredients.ScramEqualDictionary(ingredients) && x.SkillId == SkillId);
            return template;
        }
        private void OnCraftResulted(CraftResultEnum result, CharacterItemRecord item = null)
        {
            if (item != null)
                Character.Client.Send(new ExchangeCraftResultWithObjectDescMessage((sbyte)result, item.Template.GetObjectItemNotInContainer(item.UId, (uint)Count)));
            else
                Character.Client.Send(new ExchangeCraftResultMessage((sbyte)result));
        }
        public override void SetCount(int count)
        {
            if (count == 0)
                count = 1;

            this.Count = count;
            Character.Client.Send(new ExchangeCraftCountModifiedMessage(count));
        }
        public override void MoveKamas(int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
