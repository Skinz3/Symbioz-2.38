using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using Symbioz.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Exchanges
{
    public class SellExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.BIDHOUSE_SELL;
            }
        }

        private BidShopRecord BidShop
        {
            get;
            set;
        }

        private Npc Npc
        {
            get;
            set;
        }

        private List<BidShopItemRecord> SelledItems
        {
            get;
            set;
        }

        public SellExchange(Character character, Npc npc, BidShopRecord bidShop)
            : base(character)
        {
            this.BidShop = bidShop;
            this.Npc = npc;

        }
        public void MoveItemPriced(uint uid, int quantity, uint price)
        {
            CharacterItemRecord item = Character.Inventory.GetItem(uid);

            if (item != null && item.Quantity >= quantity && item.CanBeExchanged())
            {
                BidShopItemRecord selledItem = item.ToBidShopItemRecord(BidShop.Id, Character.Client.Account.Id, price);
                selledItem.Quantity = (uint)quantity;
                Character.Inventory.RemoveItem(item.UId, (uint)quantity);
                AddSelledItem(selledItem);
            }
        }
        public void ModifyItemPriced(uint uid, int quantity, uint price)
        {
            BidShopItemRecord item = GetSelledItem(uid);

            if (item != null)
            {
                item.Price = price;
                item.Quantity = (uint)quantity;
                item.UpdateElement();
                Open();
            }
        }
        public void AddSelledItem(BidShopItemRecord item)
        {
            item.AddElement();
            SelledItems.Add(item);
            Character.Client.Send(new ExchangeBidHouseItemAddOkMessage(item.GetObjectItemToSellInBid()));

        }
        public override void MoveItem(uint uid, int quantity)
        {
            if (quantity < 0)
            {
                BidShopItemRecord item = GetSelledItem(uid);

                if (item != null && item.Quantity >= Math.Abs(quantity))
                {
                    item.RemoveElement();
                    SelledItems.Remove(item);
                    Character.Inventory.AddItem(item.ToCharacterItemRecord(Character.Id));
                    Character.Client.Send(new ExchangeBidHouseItemRemoveOkMessage((int)uid));
                }
            }
        }
        public BidShopItemRecord GetSelledItem(uint uid)
        {
            return SelledItems.Find(x => x.UId == uid);
        }
        public override void Ready(bool ready, ushort step)
        {
            throw new NotImplementedException();
        }

        public override void MoveKamas(int quantity)
        {
            throw new NotImplementedException();
        }

        public override void Open()
        {
            this.SelledItems = BidShopItemRecord.GetSellerItems(BidShop.Id, Character.Client.Account.Id);
            Character.Client.Send(new ExchangeStartedBidSellerMessage(BidShop.GetBuyerDescriptor((int)Npc.Id),
               SelledItems.ConvertAll<ObjectItemToSellInBid>(x => x.GetObjectItemToSellInBid()).ToArray()));
        }

    }
}
