using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Items;
using Symbioz.World.Network;
using Symbioz.World.Providers.Maps.Npcs;
using Symbioz.World.Records;
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
    public class BuyExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get { return ExchangeTypeEnum.BIDHOUSE_BUY; }
        }

        private BidShopRecord BidShop { get; set; }

        private Npc Npc { get; set; }

        private List<BidShopItemRecord> Items
        {
            get
            {
                return BidShopItemRecord.GetBidShopItems(BidShop.Id);
            }
        }

        private ushort GIdWatched;

        public BuyExchange(Character character, Npc npc, BidShopRecord bidShop)
            : base(character)
        {
            this.BidShop = bidShop;
            this.Npc = npc;
        }

        public void ShowTypes(uint type)
        {
            GIdWatched = 0;
            Character.Client.Send(new ExchangeTypesExchangerDescriptionForUserMessage(GetGIDs((ItemTypeEnum)type)));
        }
        public void ShowList(ushort gid)
        {
            GIdWatched = gid;
            Character.Client.Send(new ExchangeTypesItemsExchangerDescriptionForUserMessage(
               (SortedItems(GetItemsByGId(gid)))));
        }
        private BidShopItemRecord GetItem(uint uid)
        {
            return Items.Find(x => x.UId == uid);
        }
        public void Buy(uint uid, uint quantity, uint price)
        {
            BidShopItemRecord item = GetItem(uid);

            if (item != null)
            {
                if (item.Price == price)
                {
                    if (Character.RemoveKamas((int)price))
                    {
                        Character.Inventory.AddItem(item.ToCharacterItemRecord(Character.Id));
                        item.RemoveElement();
                        ShowList(item.GId);

                        if (Items.Count(x => x.GId == item.GId) == 0)
                            RemoveGId(item.GId);

                        this.AddGain(item);

                    }
                }
            }
            else
            {
                Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 64);
                ShowList(GIdWatched);
                ShowTypes(GIdWatched);
            }
        }

        public void RemoveGId(ushort gid)
        {
            Character.Client.Send(new ExchangeBidHouseGenericItemRemovedMessage(gid));
        }
        private List<BidShopItemRecord> GetItemsByGId(ushort gid)
        {
            return Items.FindAll(x => x.GId == gid);
        }

        private BidExchangerObjectInfo[] SortedItems(List<BidShopItemRecord> items)
        {
            List<BidExchangerObjectInfo> result = new List<BidExchangerObjectInfo>();

            foreach (var itemsData in ItemCollection<BidShopItemRecord>.SortByEffects(items).Keys)
            {
                BidShopItemRecord sample = null;

                int[] prices = new int[3];

                for (int i = 0; i < BidShop.Quantities.Count; i++)
                {
                    int quantityPrice = 0;

                    List<BidShopItemRecord> sortedItems = itemsData.FindAll(x => x.Quantity == BidShop.Quantities[i]).OrderBy(x => x.Price).ToList();

                    if (sortedItems.Count > 0)
                    {
                        quantityPrice = (int)sortedItems[0].Price;
                        sample = sortedItems[0];
                    }
                    prices[i] = quantityPrice;
                }

                result.Add(sample.GetBidExchangerObjectInfo(prices.ToArray()));

            }
            return result.ToArray();
        }
        private uint[] GetGIDs(ItemTypeEnum type)
        {
            var items = Items.FindAll(x => x.Template.TypeEnum == type);
            return items.ConvertAll<uint>(x => x.GId).Distinct().ToArray();
        }
        public override void MoveItem(uint uid, int quantity)
        {
            throw new NotImplementedException();
        }

        public override void Ready(bool ready, ushort step)
        {
            throw new NotImplementedException();
        }

        public override void MoveKamas(int quantity)
        {
            throw new NotImplementedException();
        }
        public override void Close()
        {
            base.Close();
        }

        public override void Open()
        {
            Character.Client.Send(new ExchangeStartedBidBuyerMessage(BidShop.GetBuyerDescriptor((int)Npc.Id)));
        }
        public void AddGain(BidShopItemRecord item)
        {
            var client = WorldServer.Instance.GetOnlineClient(item.AccountId);

            string notification = "Banque : + " + item.Price + " Kamas (vente de " + item.Quantity + " <b>[" + item.Template.Name + "]</b>) hors ligne.";

            if (client != null)
            {
                AccountInformationsRecord.AddBankKamas(item.AccountId, item.Price);
                client.Character.OnItemSelled(item.GId, item.Quantity, item.Price);
            }
            else
            {
                AccountInformationsRecord.AddBankKamas(item.AccountId, item.Price);
                NotificationRecord.Add(item.AccountId, notification);

            }
        }

    }
}
