using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Items;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Exchanges
{
    public class PlayerTradeExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.PLAYER_TRADE;
            }
        }

        private ItemCollection<CharacterItemRecord> ExchangedItems = new ItemCollection<CharacterItemRecord>();

        private bool IsReady = false;

        private int MovedKamas = 0;

        public Character SecondTrader { get; set; }

        public PlayerTradeExchange(Character character, Character secondCharacter)
            : base(character)
        {
            this.SecondTrader = secondCharacter;

            ExchangedItems.OnItemAdded += ExchangedItems_OnItemAdded;
            ExchangedItems.OnItemRemoved += ExchangedItems_OnItemRemoved;
            ExchangedItems.OnItemStacked += ExchangedItems_OnItemStacked;
            ExchangedItems.OnItemUnstacked += ExchangedItems_OnItemUnstacked;
        }

        #region Events
        void ExchangedItems_OnItemUnstacked(CharacterItemRecord arg1, uint arg2)
        {
            OnObjectModified(arg1);
        }
        void ExchangedItems_OnItemStacked(CharacterItemRecord arg1, uint arg2)
        {
            OnObjectModified(arg1);
        }
        void ExchangedItems_OnItemRemoved(CharacterItemRecord obj)
        {
            Character.Client.Send(new ExchangeObjectRemovedMessage(false, obj.UId));
            SecondTrader.Client.Send(new ExchangeObjectRemovedMessage(true, obj.UId));
        }

        void ExchangedItems_OnItemAdded(CharacterItemRecord obj)
        {
            Character.Client.Send(new ExchangeObjectAddedMessage(false, obj.GetObjectItem()));
            SecondTrader.Client.Send(new ExchangeObjectAddedMessage(true, obj.GetObjectItem()));
        }
        private void OnObjectModified(CharacterItemRecord obj)
        {
            Character.Client.Send(new ExchangeObjectModifiedMessage(false, obj.GetObjectItem()));
            SecondTrader.Client.Send(new ExchangeObjectModifiedMessage(true, obj.GetObjectItem()));
        }
        #endregion


        public override void Open()
        {
            this.Send(new ExchangeStartedWithPodsMessage((sbyte)ExchangeType,
                Character.Id, Character.Inventory.CurrentWeight,
                Character.Inventory.TotalWeight,
                SecondTrader.Id, SecondTrader.Inventory.CurrentWeight,
                SecondTrader.Inventory.TotalWeight));
        }

        public void Send(Message message)
        {
            Character.Client.Send(message);
            SecondTrader.Client.Send(message);
        }
        public override void Close()
        {
            SecondTrader.Client.Send(new ExchangeLeaveMessage((sbyte)this.DialogType, this.Succes));
            SecondTrader.Dialog = null;

            Character.Client.Send(new ExchangeLeaveMessage((sbyte)this.DialogType, this.Succes));
            Character.Dialog = null;
        }
        private bool CanMoveItem(CharacterItemRecord item, int quantity)
        {
            if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED || !item.CanBeExchanged())
                return false;

            CharacterItemRecord exchanged = null;

            exchanged = ExchangedItems.GetItem(item.GId, item.Effects);

            if (exchanged != null && exchanged.UId != item.UId)
                return false;

            exchanged = ExchangedItems.GetItem(item.UId);

            if (exchanged == null)
            {
                return true;
            }

            if (exchanged.Quantity + quantity > item.Quantity)
                return false;
            else
                return true;
        }
        public override void MoveItem(uint uid, int quantity)
        {
            if (!IsReady)
            {
                CharacterItemRecord item = Character.Inventory.GetItem(uid);

                if (item != null && CanMoveItem(item, quantity))
                {
                    if (SecondTrader.GetDialog<PlayerTradeExchange>().IsReady)
                    {
                        SecondTrader.GetDialog<PlayerTradeExchange>().Ready(false, 0);
                    }
                    if (quantity > 0)
                    {
                        if (item.Quantity >= quantity)
                        {
                            ExchangedItems.AddItem(item, (uint)quantity);
                        }
                    }
                    else
                    {
                        ExchangedItems.RemoveItem(item.UId, (uint)(Math.Abs(quantity)));
                    }
                }
            }
        }

        public override void Ready(bool ready, ushort step)
        {
            this.IsReady = ready;

            Send(new ExchangeIsReadyMessage(Character.Id, this.IsReady));

            if (this.IsReady && SecondTrader.GetDialog<PlayerTradeExchange>().IsReady)
            {
                foreach (var item in ExchangedItems.GetItems())
                {
                    item.CharacterId = SecondTrader.Id;
                    SecondTrader.Inventory.AddItem((CharacterItemRecord)item.CloneWithoutUID());
                    Character.Inventory.RemoveItem(item.UId, item.Quantity);
                }

                foreach (var item in SecondTrader.GetDialog<PlayerTradeExchange>().ExchangedItems.GetItems())
                {
                    item.CharacterId = Character.Id;
                    Character.Inventory.AddItem((CharacterItemRecord)item.CloneWithoutUID());
                    SecondTrader.Inventory.RemoveItem(item.UId, item.Quantity);
                }

                SecondTrader.AddKamas(MovedKamas);
                Character.RemoveKamas(MovedKamas);

                Character.AddKamas(SecondTrader.GetDialog<PlayerTradeExchange>().MovedKamas);
                SecondTrader.RemoveKamas(SecondTrader.GetDialog<PlayerTradeExchange>().MovedKamas);

                this.Succes = true;
                this.Close();
            }
        }

        public override void MoveKamas(int quantity)
        {
            if (quantity <= Character.Record.Kamas)
            {
                if (IsReady)
                {
                    Ready(false, 0);
                }
                if (SecondTrader.GetDialog<PlayerTradeExchange>().IsReady)
                {
                    SecondTrader.GetDialog<PlayerTradeExchange>().Ready(false, 0);
                }


                SecondTrader.Client.Send(new ExchangeKamaModifiedMessage(true, (uint)quantity));
                MovedKamas = quantity;
            }


        }

    }
}
