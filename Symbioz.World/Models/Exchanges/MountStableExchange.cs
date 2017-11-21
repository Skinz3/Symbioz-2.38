using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Providers.Items;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Exchanges
{
    public class MountStableExchange : Exchange
    {
        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.MOUNT_STABLE;
            }
        }

        public MountStableExchange(Character character)
            : base(character)
        {

        }
        public override void Open()
        {
            Character.Client.Send(new PaddockPropertiesMessage(new PaddockInformations(10, 10)));
            Character.Client.Send(new ExchangeStartOkMountMessage(new MountClientData[0], new MountClientData[0]));
        }
        public void HandleMountStable(sbyte actionType, uint[] ridesId)
        {
            if (actionType == 15) // Equiper la monture
            {
                if (Character.Inventory.HasMountEquiped)
                {
                    UnequipMount(Character.Inventory.Mount.UId);
                }

                EquipMount(ridesId[0]);
            }
            if (actionType == 13) // Obtenir un certificat de la monture
            {
                UnequipMount(ridesId[0]);
            }

        }
        private void EquipMount(uint itemUId)
        {
            CharacterItemRecord item = Character.Inventory.GetItem(itemUId);
            CharacterMountRecord mount = Character.Inventory.GetMount(item);
            Character.Inventory.SetMount(mount, item);
        }
        private void UnequipMount(long mountUId)
        {
            CharacterMountRecord mount = Character.Inventory.GetMount(mountUId);
            CharacterItemRecord item = mount.CreateCertificate(Character);
            Character.Inventory.AddItem(item);
            Character.Inventory.UnsetMount();
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
