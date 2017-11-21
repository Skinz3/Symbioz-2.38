using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Admin
{
    internal class AdminHandler
    {
        public const string QUIET_TELEPORT = "moveto";

        [MessageHandler]
        public static void HandleAdminQuietCommand(AdminQuietCommandMessage message, WorldClient client)
        {
            if (client.Account.Role >= ServerRoleEnum.Administrator && !client.Character.Busy)
            {
                if (message.content.StartsWith(QUIET_TELEPORT))
                {
                    int mapid;
                    if (int.TryParse(message.content.Split(null).Last(), out mapid))
                    {
                        client.Character.Teleport(mapid);
                    }
                }
                else
                {
                    client.Character.ReplyError("Unknown command: " + message.content);
                }
            }

        }
    }
}
