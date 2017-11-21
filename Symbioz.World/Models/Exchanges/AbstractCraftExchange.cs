using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
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
    public abstract class AbstractCraftExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.CRAFT;
            }
        }

        protected uint SkillId
        {
            get;
            set;
        }

        protected JobsTypeEnum JobType
        {
            get;
            set;
        }

        protected CharacterJob CharacterJob
        {
            get;
            set;
        }
        protected int Count
        {
            get;
            set;
        }

        protected ItemCollection<CharacterItemRecord> CraftedItems = new ItemCollection<CharacterItemRecord>();

        public AbstractCraftExchange(Character character, uint skillId, JobsTypeEnum jobType)
            : base(character)
        {
            this.SkillId = skillId;
            this.JobType = jobType;
            this.CharacterJob = Character.GetJob(JobType);
            this.Count = 1;

            this.CraftedItems.OnItemAdded += CraftedItems_OnItemAdded;
            this.CraftedItems.OnItemRemoved += CraftedItems_OnItemRemoved;

            this.CraftedItems.OnItemQuantityChanged += CraftedItems_OnItemQuantityChanged;

        }
        void CraftedItems_OnItemQuantityChanged(CharacterItemRecord arg1, uint arg2)
        {
            Character.Client.Send(new ExchangeObjectModifiedMessage(false, arg1.GetObjectItem()));
        }
        void CraftedItems_OnItemRemoved(CharacterItemRecord obj)
        {
            Character.Client.Send(new ExchangeObjectRemovedMessage(false, obj.UId));
        }

        void CraftedItems_OnItemAdded(CharacterItemRecord obj)
        {
            Character.Client.Send(new ExchangeObjectAddedMessage(false, obj.GetObjectItem()));
        }
        private bool CanAddItem(CharacterItemRecord item, int quantity)
        {
            if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                return false;

            CharacterItemRecord exchanged = null;

            exchanged = CraftedItems.GetItem(item.GId, item.Effects);

            if (exchanged != null && exchanged.UId != item.UId)
                return false;

            exchanged = CraftedItems.GetItem(item.UId);

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
            CharacterItemRecord item = Character.Inventory.GetItem(uid);

            if (item != null)
            {
                if (quantity > 0 && CanAddItem(item, quantity))
                {

                    this.CraftedItems.AddItem(item, (uint)quantity);
                }
                else
                {
                    this.CraftedItems.RemoveItem(item.UId, (uint)Math.Abs(quantity));
                }
            }
            else
            {
                this.CraftedItems.Clear(false);
            }
        }
        public abstract void SetCount(int count);

        public override void Open()
        {
            Character.Client.Send(new ExchangeStartOkCraftWithInformationMessage(SkillId));
        }
    }
}
