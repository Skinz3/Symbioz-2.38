using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
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
    public class NpcShopExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.NPC_SHOP;
            }
        }

        public Npc Npc
        {
            get;
            set;
        }

        public ItemRecord[] ItemToSell
        {
            get;
            set;
        }

        public ushort TokenId
        {
            get;
            set;
        }

        private bool LevelPrice
        {
            get;
            set;
        }
        public NpcShopExchange(Character character, Npc npc, ItemRecord[] itemToSell, ushort tokenId, bool levelPrice)
            : base(character)
        {
            this.Npc = npc;
            this.ItemToSell = itemToSell;
            this.TokenId = tokenId;
            this.LevelPrice = levelPrice;
        }

        public override void Open()
        {
            ObjectItemToSellInNpcShop[] items = Array.ConvertAll<ItemRecord, ObjectItemToSellInNpcShop>(ItemToSell, x => x.GetObjectItemToSellInNpcShop(LevelPrice));
            Character.Client.Send(new ExchangeStartOkNpcShopMessage(Npc.Id, TokenId, items));
        }

        public void Buy(ushort gid, uint quantity)
        {
            ItemRecord template = ItemToSell.FirstOrDefault(x => x.Id == gid);

            if (template != null)
            {
                if (this.TokenId == 0)
                {
                    int cost = (int)(template.GetPrice(LevelPrice) * quantity);

                    if (!Character.RemoveKamas(cost))
                        return;
                }
                else
                {
                    CharacterItemRecord tokenItem = Character.Client.Character.Inventory.GetFirstItem(TokenId, (uint)(template.GetPrice(LevelPrice) * quantity));

                    if (tokenItem == null)
                    {
                        Character.Client.Character.ReplyError("Vous ne possedez pas asser de token.");
                        return;
                    }
                    else
                    {
                        Character.Inventory.RemoveItem(tokenItem.UId, (uint)(quantity * template.GetPrice(LevelPrice)));
                    }
                }

                Character.Inventory.AddItem(gid, quantity,TokenId != 0);
                Character.Client.Send(new ExchangeBuyOkMessage());
            }

        }
        public void Sell(uint uid, uint quantity)
        {
            CharacterItemRecord item = Character.Inventory.GetItem(uid);

            if (item != null && item.CanBeExchanged() && item.Quantity >= quantity)
            {
                int gained = (int)(((double)item.Template.GetPrice(LevelPrice) / (double)10) * quantity);

                if (gained >= item.Template.GetPrice(LevelPrice))
                {
                    return;
                }

                gained = gained == 0 ? 1 : gained;

                Character.Inventory.RemoveItem(uid, quantity);

                if (TokenId == 0)
                {
                    Character.AddKamas(gained);
                }
                else
                {
                    Character.Inventory.AddItem(TokenId, (uint)gained);
                }
                Character.Client.Send(new ExchangeSellOkMessage());
            }
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
    }
}
