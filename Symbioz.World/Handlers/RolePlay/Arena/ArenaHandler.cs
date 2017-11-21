using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.World.Network;
using Symbioz.World.Providers.Arena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Arena
{
    public class ArenaHandler
    {
        [MessageHandler]
        public static void HandleGameRolePlayArenaRegisterMessage(GameRolePlayArenaRegisterMessage message, WorldClient client)
        {
            if (client.Character.CanRegisterArena)
            {
                client.Character.RegisterArena();
            }
            else
            {
                client.Character.ReplyError("Cannot Join arena now");
            }

        }

        [MessageHandler]
        public static void HandleGameRolePlayArenaUnregisterMessage(GameRolePlayArenaUnregisterMessage message, WorldClient client)
        {
            client.Character.UnregisterArena();
        }

        [MessageHandler]
        public static void HandleGameRolePlayArenaFightAnswerMessage(GameRolePlayArenaFightAnswerMessage message, WorldClient client)
        {
            client.Character.AnwserArena(message.accept);
        }

    }
}
