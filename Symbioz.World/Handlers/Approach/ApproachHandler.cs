using SSync.IO;
using SSync.Messages;
using SSync.Transition;
using Symbioz.Core;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Messages;
using Symbioz.World.Network;
using Symbioz.World.Records;
using Symbioz.World.Records.Breeds;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.Approach
{
    class ApproachHandler
    {
        public static object m_locker = new object();

        [MessageHandler]
        public static void HandleClientKeyRequestMessage(ClientKeyMessage message, WorldClient client)
        {

        }
        [MessageHandler]
        public static void HandleReloginTokenRequestMessage(ReloginTokenRequestMessage message, WorldClient client)
        {
            client.Send(new ReloginTokenStatusMessage(false, new sbyte[0]));
        }
        [MessageHandler]
        public static void HandleAuthentificationTicketMessage(AuthenticationTicketMessage message, WorldClient client)
        {
            lock (m_locker)
            {
                var reader = new BigEndianReader(Encoding.ASCII.GetBytes(message.ticket));
                var count = reader.ReadByte();
                var ticket = reader.ReadUTFBytes(count);

                MessagePool.SendRequest<AccountMessage>(TransitionServerManager.Instance.AuthServer, new AccountRequestMessage
                {
                    Ticket = ticket
                }, delegate (AccountMessage msg)
                {
                    OnAccountReceived(client, msg);
                },
                delegate ()
                {
                    OnAccountReceptionError(client);
                });
            }

        }
        public static void OnAccountReceptionError(WorldClient client)
        {
            client.Disconnect();
        }
        public static void OnAccountReceived(WorldClient client, AccountMessage message)
        {
            WorldServer.Instance.Disconnect(message.Account.Id);

            if (WorldServer.Instance.IsStatus(ServerStatusEnum.ONLINE))
            {
                client.Account = message.Account;
                client.OnAccountReceived();
            }
            else
            {
                client.Disconnect();
            }

        }


    }
}
