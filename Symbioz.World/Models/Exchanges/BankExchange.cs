using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Items;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Exchanges
{
    public class BankExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get { return ExchangeTypeEnum.BANK; }
        }

        public const uint StorageMaxSlot = 300;

        private ItemCollection<BankItemRecord> m_items;

        public BankExchange(Character character, List<BankItemRecord> bankItems)
            : base(character)
        {
            m_items = new ItemCollection<BankItemRecord>(bankItems);
            m_items.OnItemAdded += m_items_OnItemAdded;
            m_items.OnItemRemoved += m_items_OnItemRemoved;
            m_items.OnItemStacked += m_items_OnItemStacked;
            m_items.OnItemUnstacked += m_items_OnItemUnstacked;
        }

        void m_items_OnItemUnstacked(BankItemRecord arg1, uint arg2)
        {
            arg1.UpdateElement();
            Character.Client.Send(new StorageObjectUpdateMessage(arg1.GetObjectItem()));
        }

        void m_items_OnItemStacked(BankItemRecord arg1, uint arg2)
        {
            arg1.UpdateElement();
            Character.Client.Send(new StorageObjectUpdateMessage(arg1.GetObjectItem()));
        }

        void m_items_OnItemRemoved(BankItemRecord obj)
        {
            obj.RemoveElement();
            Character.Client.Send(new StorageObjectRemoveMessage(obj.UId));
        }

        void m_items_OnItemAdded(BankItemRecord obj)
        {
            obj.AddElement();
            Character.Client.Send(new StorageObjectUpdateMessage(obj.GetObjectItem()));
        }
        public override void Open()
        {
            Character.Client.Send(new ExchangeStartedWithStorageMessage((sbyte)ExchangeType, StorageMaxSlot));
            Character.Client.Send(new StorageInventoryContentMessage(m_items.GetObjectsItems(), Character.Client.AccountInformations.BankKamas));
        }
        public override void MoveItem(uint uid, int quantity)
        {
            if (quantity > 0)
            {
                CharacterItemRecord item = Character.Inventory.GetItem(uid);

                if (item != null && item.Quantity >= quantity && item.CanBeExchanged())
                {
                    var bankItem = item.ToBankItemRecord(Character.Client.Account.Id);
                    bankItem.Quantity = (uint)quantity;
                    Character.Inventory.RemoveItem(item.UId, (uint)quantity);
                    m_items.AddItem(bankItem);
                }
            }
            else
            {
                BankItemRecord item = m_items.GetItem(uid);
                uint removedQuantity = (uint)Math.Abs(quantity);

                if (item != null && item.Quantity >= removedQuantity)
                {
                    var characterItemRecord = item.ToCharacterItemRecord(Character.Id);
                    characterItemRecord.Quantity = removedQuantity;
                    m_items.RemoveItem(uid, removedQuantity);
                    Character.Inventory.AddItem(characterItemRecord);

                }
            }
        }
        public override void MoveKamas(int quantity)
        {
            if (quantity < 0)
            {
                if (Character.Client.AccountInformations.BankKamas >= Math.Abs(quantity))
                    Character.AddKamas(Math.Abs(quantity));
                else
                    return;
            }
            else
            {
                if (!Character.RemoveKamas(quantity))
                    return;
            }

            Character.Client.AccountInformations.BankKamas += (uint)quantity;
            Character.Client.AccountInformations.UpdateElement();
            Character.Client.Send(new StorageKamasUpdateMessage(
                (int)Character.Client.AccountInformations.BankKamas));
        }

        public override void Ready(bool ready, ushort step)
        {
            throw new NotImplementedException();
        }

    }
}
