using SSync;
using SSync.IO;
using Symbioz.Auth;
using Symbioz.Auth.Records;
using Symbioz.Auth.Transition;
using Symbioz.Core;
using Symbioz.Network.Servers;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Network
{
    public class AuthClient : SSyncClient
    {
        public byte[] AesKey { get; set; }

        public AccountData Account { get; set; }

        public IdentificationMessage IdentificationMessage { get; set; }

        public AuthClient(Socket socket)
            : base(socket)
        {
            base.Send(new ProtocolRequired(AuthConfiguration.Instance.DofusProtocolVersion, AuthConfiguration.Instance.DofusProtocolVersion));
            base.Send(new RawDataMessage(RawDataManager.GetRawData("auth")));
        }
        public override void OnClosed()
        {
            AuthServer.Instance.RemoveClient(this);
            base.OnClosed();
        }
        public void SendServerList()
        {
            Send(new ServersListMessage(ServerRecord.GetGameServerInformations(Account), 0, true));
        }
        public void ProcessServerSelection(ServerRecord server)
        {
            if (server.Status == ServerStatusEnum.ONLINE)
            {
                AuthTicketsManager.Instance.CacheToGame(this);
                var ticket = this.EncryptTicket();
                this.Account.LastSelectedServerId = server.Id;
                AccountRecord.UpdateAccount(this.Account);
                this.Send(new SelectedServerDataMessage(server.Id, server.Host, (ushort)server.Port, true, ticket));
                this.Disconnect();
            }
            else
            {
                this.Disconnect();
            }
        }
        public void SystemMessage(string str)
        {
            this.Send(new SystemMessageDisplayMessage(true, 61, new string[] { str }));
        }
        sbyte[] EncryptTicket()
        {
            var writer = new BigEndianWriter();
            writer.WriteByte((byte)Account.Ticket.Length);
            writer.WriteUTFBytes(Account.Ticket);
            return Array.ConvertAll<byte, sbyte>(Cryptography.AESEncrypt(writer.Data, AesKey), x => (sbyte)x);
        }
        public bool IsVersionUpToDate()
        {
            var version = AuthConfiguration.Instance.GetVersionExtended();
            var clientVersion = IdentificationMessage.version;

            if (clientVersion.major == version.major && clientVersion.minor == version.minor && clientVersion.patch == version.patch)
                return true;
            return false;
        }

    }
}
