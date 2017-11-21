using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers
{
    class BasicHandler
    {
        [MessageHandler]
        public static void HandleBasicPing(BasicPingMessage message,WorldClient client)
        {
            client.Send(new BasicPongMessage(message.quiet));
        }
    }
}
