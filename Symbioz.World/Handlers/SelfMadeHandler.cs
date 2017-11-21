using SSync.IO;
using SSync.Messages;
using Symbioz.Modules;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.RawMessages;
using Symbioz.World.Network;
using Symbioz.World.Providers.Guilds;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers
{
    class SelfMadeHandler
    {
        [MessageHandler]
        public static void HandleRawDataMessage(RawDataMessage message, WorldClient client)
        {
            BigEndianReader reader = new BigEndianReader(message._content);
            RawMessage msg = RawDataProtocolManager.Build(reader);

            if (msg != null)
            {
                RawDataProtocolManager.Handle(msg, client);
            }
        }

        [RawDataMessageHandler]
        public static void HandleGuildArenaSubscribeAnswerMessage(GuildArenaSubscribeAnswerMessage message, WorldClient client)
        {
            if (message.Register)
            {
                GuildArenaProvider.Register(client.Character);
            }
            else if (GuildArenaProvider.IsRegister(client.Character.Guild))
            {
                GuildArenaProvider.Unregister(client.Character);
            }
        }
    }
}
