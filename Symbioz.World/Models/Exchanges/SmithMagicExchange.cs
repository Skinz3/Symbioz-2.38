using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Entities.Jobs;
using Symbioz.World.Models.Items;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Exchanges
{
    public class SmithMagicExchange : AbstractCraftExchange
    {
        public static ItemTypeEnum RuneType = ItemTypeEnum.RUNE_DE_FORGEMAGIE;

        private CharacterItemRecord RuneItem
        {
            get;
            set;
        }
        private EffectInteger RuneEffect
        {
            get
            {
                return RuneItem.FirstEffect<EffectInteger>();
            }
        }
        private CharacterItemRecord Item
        {
            get
            {
                return CraftedItems.GetItems().First();
            }
        }
        public SmithMagicExchange(Character character, uint skillId, JobsTypeEnum jobType)
            : base(character, skillId, jobType)
        {

        }

        public override void MoveItem(uint uid, int quantity)
        {
            CharacterItemRecord item = Character.Inventory.GetItem(uid);

            if (item.Template.TypeEnum == RuneType)
            {
                if (quantity > 0)
                {
                    this.RuneItem = item;
                }
                else
                {
                    this.RuneItem = null;
                }
            }
            {
                base.MoveItem(uid, quantity);
            }
        }
        public override void Ready(bool ready, ushort step)
        {
            for (int i = 0; i < RuneItem.Quantity; i++)
            {
                if (RuneEffect != null)
                {
                    Item.AddEffectInteger(RuneEffect.EffectEnum, RuneEffect.Value);
                    OnSucces();
                    Character.Inventory.OnItemModified(Item);
                    Character.Inventory.RemoveItem(RuneItem.UId, 1);
                }
                else
                {
                    OnFail();
                }
            }
          
        }
        private void OnSucces()
        {
            Character.Client.Send(new ExchangeCraftResultMagicWithObjectDescMessage((sbyte)CraftResultEnum.CRAFT_SUCCESS,
                   Item.GetObjectItemNotInContainer(), 1));
        }
        private void OnFail()
        {
            Character.Client.Send(new ExchangeCraftResultMessage((sbyte)CraftResultEnum.CRAFT_FAILED));
        }
        public override void SetCount(int count)
        {

        }
        public override void MoveKamas(int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
