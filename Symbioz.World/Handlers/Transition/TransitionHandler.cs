using SSync.Messages;
using SSync.Transition;
using Symbioz.Core;
using Symbioz.ORM;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Messages;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.Transition
{
    class TransitionHandler
    {
        static Logger logger = new Logger();

        [MessageHandler]
        public static void HandleDisconnectClientRequest(DisconnectClientRequestMessage message,TransitionClient client)
        {
            client.SendReply(new DisconnectClientResultMessage(WorldServer.Instance.Disconnect(message.AccountId)),message.Guid);
        }
       
    }
}
