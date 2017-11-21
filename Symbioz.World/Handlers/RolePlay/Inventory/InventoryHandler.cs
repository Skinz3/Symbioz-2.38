using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Network;
using Symbioz.World.Providers.Items;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Inventory
{
    public class InventoryHandler
    {
        [MessageHandler]
        public static void HandleObjectSetPosition(ObjectSetPositionMessage message, WorldClient client)
        {
            if (!client.Character.Fighting && !client.Character.Busy)
            {
                client.Character.Inventory.SetItemPosition(message.objectUID, (CharacterInventoryPositionEnum)message.position, message.quantity);
            }
        }
        [MessageHandler]
        public static void HandeObjectDeleteMessage(ObjectDeleteMessage message, WorldClient client)
        {
            if (!client.Character.Busy && !client.Character.Fighting)
            {
                CharacterItemRecord item = client.Character.Inventory.GetItem(message.objectUID);

                if (item != null)
                {
                    client.Character.Inventory.RemoveItem(item, message.quantity);

                    if (item.IsValidMountCertificate)
                    {
                        CertificateProvider.Instance.OnCertificateDestroyed(client.Character, item);
                    }
                }
            }
        }
        [MessageHandler]
        public static void HandleInventoryPresetSave(InventoryPresetSaveMessage message, WorldClient client)
        {

        }
        [MessageHandler]
        public static void HandleObjectUseMessage(ObjectUseMessage message, WorldClient client)
        {
            if (!client.Character.Busy && !client.Character.Fighting)
                client.Character.UseItem(message.objectUID, true);
        }
        [MessageHandler]
        public static void HandleObjectUseMultipleMessage(ObjectUseMultipleMessage message, WorldClient client)
        {
            if (!client.Character.Busy && !client.Character.Fighting)
            {
                for (int i = 0; i < message.quantity; i++)
                {
                    client.Character.UseItem(message.objectUID, false);
                }
                client.Character.RefreshStats();
            }
        }
        [MessageHandler]
        public static void HandleLivingObjectChangeSkinRequest(LivingObjectChangeSkinRequestMessage message, WorldClient client)
        {
            CharacterItemRecord item = client.Character.Inventory.GetItem(message.livingUID);

            if (item != null)
            {
                client.Character.Inventory.ChangeLivingObjectSkin(item, (ushort)message.skinId, (CharacterInventoryPositionEnum)message.livingPosition);
            }
        }
        [MessageHandler]
        public static void HandleLivingObjectDissociate(LivingObjectDissociateMessage message, WorldClient client)
        {
            CharacterItemRecord item = client.Character.Inventory.GetItem(message.livingUID);

            if (item != null)
            {
                client.Character.Inventory.DissociateLiving(item);
            }
        }
        [MessageHandler]
        public static void HandleWrapperObjectDissociateRequest(WrapperObjectDissociateRequestMessage message, WorldClient client)
        {
            CharacterItemRecord item = client.Character.Inventory.GetItem(message.hostUID);

            if (item != null)
            {
                client.Character.Inventory.DissociateCompatibility(item,
                    (CharacterInventoryPositionEnum)message.hostPos);
            }
        }
        [MessageHandler]
        public static void HandleDropItem(ObjectDropMessage message, WorldClient client)
        {
            return;
            client.Character.Inventory.DropItem(message.objectUID, message.quantity);
        }

        [MessageHandler]
        public static void HandleMimicryObjectFreeAndAssociateRequest(MimicryObjectFeedAndAssociateRequestMessage message, WorldClient client)
        {
            var mimicryUsable = client.Character.Inventory.GetFirstItem(14485, 1);

            if (mimicryUsable == null)
            {
                return;
            }

            CharacterItemRecord hostItem = client.Character.Inventory.GetItem(message.hostUID);
            CharacterItemRecord foodItem = client.Character.Inventory.GetItem(message.foodUID);

            if (hostItem == null || foodItem == null)
            {
                return;
            }
            if (hostItem.Template.TypeEnum == ItemTypeEnum.FAMILIER || foodItem.Template.TypeEnum == ItemTypeEnum.FAMILIER)
            {
                client.Character.ReplyError("Les Familiers ne peuvent pas êtres Mimibiotiers");
                return;
            }
            if (hostItem.IsAssociated || foodItem.IsAssociated)
            {
                client.Character.ReplyError("Les objets associés ne peuvent pas être mimibioté.");
                return;
            }
            if (hostItem.GId == foodItem.GId)
            {
                client.Character.ReplyError("Les Objets que vous voulez Mimibioter sont les mêmes");
                return;
            }

            if (hostItem.Template.Level < foodItem.Template.Level)
            {
                client.Character.ReplyError("Impossible d'associer ces objets car le niveau de l'item d'appearence est inferieur au niveau de l'item de base.");
                return;
            }

            CharacterItemRecord mimicry = hostItem.ToMimicry(foodItem.GId, foodItem.AppearanceId);

            if (message.preview)
            {
                client.Send(new MimicryObjectPreviewMessage(mimicry.GetObjectItem()));
            }
            else
            {
                client.Character.Inventory.RemoveItem(message.hostUID, 1);
                client.Character.Inventory.RemoveItem(message.foodUID, 1);
                client.Character.Inventory.RemoveItem(message.symbioteUID, 1);
                client.Character.Inventory.RemoveItem(mimicryUsable, 1);
                client.Character.Inventory.AddItem(mimicry);
                mimicry.UpdateElement();
                client.Send(new MimicryObjectAssociatedMessage(mimicry.UId));
            }
        }
        [MessageHandler]
        public static void HandleMimicryObjectEraseRequest(MimicryObjectEraseRequestMessage message, WorldClient client)
        {
            CharacterItemRecord item = client.Character.Inventory.GetItem(message.hostUID);

            if (message.hostPos != 63)
            {
                client.Character.RefreshActorOnMap();
                client.Character.RefreshStats();
            }
            if (client.Character.Inventory.GetEquipedItems().Contains(item))
            {
                client.Character.Inventory.SetItemPosition(item.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, 1);
            }
            item.RemoveAllEffects(EffectsEnum.Effect_ChangeAppearence1151);
            CharacterItemRecord newItem = (CharacterItemRecord)item.CloneWithoutUID();
            newItem.AppearanceId = ItemRecord.GetItem(newItem.GId).AppearanceId;
            client.Character.Inventory.RemoveItem(item, item.Quantity);
            client.Character.Inventory.AddItem(newItem, 1);
            client.Character.RefreshActorOnMap();
            client.Character.RefreshStats();
        }


    }
}
