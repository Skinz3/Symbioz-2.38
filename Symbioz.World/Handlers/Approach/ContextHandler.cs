using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.Approach
{
    class ContextHandler
    {
        [MessageHandler]
        public static void HandleCreateContextRequest(GameContextCreateRequestMessage message, WorldClient client)
        {
            client.Character.DestroyContext();
            client.Character.CreateContext(GameContextEnum.ROLE_PLAY);
            client.Character.OnContextCreated();
            client.Character.RefreshStats();
            client.Character.Teleport(client.Character.Record.MapId, client.Character.Record.CellId);
        }

    }
}
