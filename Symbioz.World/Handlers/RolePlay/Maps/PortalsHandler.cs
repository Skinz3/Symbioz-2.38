using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Maps
{
    class PortalsHandler
    {
        [MessageHandler]
        public static void HandlePortalUseRequest(PortalUseRequestMessage message, WorldClient client)
        {
            Portal portal = client.Character.Map.Instance.GetPortal((int)message.portalId);

            if (portal != null)
            portal.Use(client.Character);
        }
    }
}
