using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Exchanges;
using Symbioz.World.Network;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Mounts
{
    public class MountsHandler
    {
        public const short MINIMUM_LEVEL_FOR_MOUNT = 60;

        [MessageHandler]
        public static void HandleMountInformationRequest(MountInformationRequestMessage message, WorldClient client)
        {
            CharacterMountRecord record = null;

            foreach (var re in CharacterMountRecord.CharactersMounts)
            {
                if (re.UId == message.id) record = re;
            }

            if (record != null)
            {
                client.Send(new MountDataMessage(record.GetMountClientData()));
            }
        }
        [MessageHandler]
        public static void HandleExchangeHandleMountsStableMessage(ExchangeHandleMountsStableMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.MOUNT_STABLE))
            {
                client.Character.GetDialog<MountStableExchange>().HandleMountStable(message.actionType, message.ridesId);
            }
        }
        [MessageHandler]
        public static void HandleToggleRidingRequest(MountToggleRidingRequestMessage message, WorldClient client)
        {
            if (client.Character.Level >= MINIMUM_LEVEL_FOR_MOUNT)
                client.Character.Inventory.ToggleMount();
            else
                client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 227, MINIMUM_LEVEL_FOR_MOUNT);
        }
    }
}
