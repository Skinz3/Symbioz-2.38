using Symbioz.Core;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Maps;
using Symbioz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Records.Characters;
using Symbioz.World.Models.Entities;
using Symbioz.World.Network;
using Symbioz.World.Models.Maps.Instances;

namespace Symbioz.World.Models.Items
{
    public class DropItem
    {
        public int Id
        {
            get;
            private set;
        }

        public ushort Quantity
        {
            get;
            set;
        }

        public CharacterItemRecord Record
        {
            get;
            set;
        }

        public ushort CellId
        {
            get;
            set;
        }

        public AbstractMapInstance Map
        {
            get;
            private set;
        }

        private ActionTimer Timer;

        public bool PickedUp
        {
            get;
            private set;
        }

        public DropItem(CharacterItemRecord record, ushort quantity, ushort cellid, AbstractMapInstance map)
        {
            this.Id = map.PopNextDropItemId();
            this.Record = record;
            this.Map = map;
            this.CellId = cellid;
            this.Quantity = quantity;
            Timer = new ActionTimer(900000, Remove, false);
            PickedUp = false;

        }

        public void OnPickUp(Character character)
        {
            if (!PickedUp)
            {
                PickedUp = true;
                var record = Record.CloneWithoutUID().ToCharacterItemRecord(character.Id);
                character.Inventory.AddItem(record, Quantity);
                Remove();
            }
        }

        public static DropItem Create(CharacterItemRecord record, ushort quantity, ushort cellid, AbstractMapInstance map)
        {
            return new DropItem(record, quantity, cellid, map);
        }

        void Remove()
        {
            Map.RemoveDropItem(this);
            Timer.Stop();
            Timer = null;
        }
    }
}
