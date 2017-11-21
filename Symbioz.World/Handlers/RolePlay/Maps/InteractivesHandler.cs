using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.World.Network;
using Symbioz.World.Providers.Maps.Interactives;
using Symbioz.World.Records;
using Symbioz.World.Records.Interactives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Maps
{
    class InteractivesHandler
    {
        /// <summary>
        /// Map.Instance.UseInteractive(client.Character);
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleInteractiveUse(InteractiveUseRequestMessage message, WorldClient client)
        {
            client.Character.Map.Instance.UseInteractive(client.Character, message.elemId, message.skillInstanceUid);
        }
        [MessageHandler]
        public static void HandleZaapRespawnSaveRequest(ZaapRespawnSaveRequestMessage message, WorldClient client)
        {
            client.Character.Record.SpawnPointMapId = client.Character.Map.Id;
            client.Send(new ZaapRespawnUpdatedMessage(client.Character.Record.SpawnPointMapId));
        }
    }
}
