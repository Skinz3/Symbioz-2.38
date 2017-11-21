using Symbioz.Network;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Network.Servers;
using Symbioz.Auth.Records;
using Symbioz.Auth.Transition;
using System.Net;
using Symbioz.Protocol.Selfmade.Types;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Core;
using SSync.IO;
using SSync.Messages;


namespace Symbioz.Auth.Handlers
{
    class ConnectionHandler
    {
        static object Locker = new object();

        /// <summary>
        /// Procède a l'identification d'un client.
        /// </summary>
        /// <param name="client"></param>
        public static void ProcessIdentification(AuthClient client)
        {
            client.Send(new CredentialsAcknowledgementMessage());

            if (!client.IsVersionUpToDate())
            {
                client.Send(new IdentificationFailedForBadVersionMessage((sbyte)IdentificationFailureReasonEnum.BAD_VERSION, AuthConfiguration.Instance.GetVersionExtended()));
                return;
            }
            BigEndianReader reader = new BigEndianReader(client.IdentificationMessage.credentials.Select(x => (byte)x).ToArray());
            string username = reader.ReadUTF();
            string password = reader.ReadUTF();
            client.AesKey = reader.ReadBytes(32);

            AccountData account = AccountRecord.GetAccountByUsername(username);

            if (account == null || account.Password != password)
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.WRONG_CREDENTIALS));
                return;
            }
            if (account.Banned || BanIpRecord.IsBanned(client.Ip))
            {
                client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.BANNED));
                return;
            }

            client.Account = account;

            if (client.Account.Nickname == null || client.Account.Nickname == string.Empty)
            {
                client.Send(new NicknameRegistrationMessage());
                return;
            }

            Login(client, client.IdentificationMessage.autoconnect);
        }
        /// <summary>
        /// Gère le message d'authentification envoyé par le client (par le biais du RawDataMessage).
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleIdentificationMessage(IdentificationMessage message, AuthClient client)
        {
            lock (Locker)
            {
                if (!AuthQueue.Instance.IsInQueue(client))
                {
                    client.IdentificationMessage = message;
                    AuthQueue.Instance.AddClient(client);
                }
            }
        }
        /// <summary>
        /// Gère la requête de choix de pseudo d'un client.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleNicknameChoiceRequestMessage(NicknameChoiceRequestMessage message, AuthClient client)
        {
            if (client.Account == null)
            {
                return;
            }
            if (client.Account.Username == message.nickname)
            {
                client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.SAME_AS_LOGIN));
                return;
            }
            if (message.nickname == string.Empty || message.nickname.Length >= 15 || message.nickname.Contains('\''))
            {
                client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.INVALID_NICK));
                return;
            }
            if (AccountRecord.NicknameExist(message.nickname))
            {
                client.Send(new NicknameRefusedMessage((sbyte)NicknameErrorEnum.ALREADY_USED));
                return;
            }

            client.Account.Nickname = message.nickname;

            AccountRecord.UpdateAccount(client.Account);
            client.Send(new NicknameAcceptedMessage());

            Login(client, false);


        }
        /// <summary>
        /// Methode appelée lorsque le client est authentifié avec succès.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="autoconnect"></param>
        private static void Login(AuthClient client, bool autoconnect)
        {
            bool wasConnected = ServersManager.Instance.DisconnectClient(client);

            AuthServer.Instance.AddClient(client);

            bool hasRights = client.Account.Role > ServerRoleEnum.Player ? true : false;

            client.Send(new IdentificationSuccessMessage(hasRights,
                wasConnected, client.Account.Username, client.Account.Nickname,
                client.Account.Id, 0, string.Empty, 0, 0, DateTime.Now.DateTimeToUnixTimestamp(), 0));

            if (!autoconnect)
            {
                client.SendServerList();
                return;
            }
            else
            {
                if (client.Account.LastSelectedServerId != 0)
                {
                    ServerRecord server = ServerRecord.GetWorldServer(client.Account.LastSelectedServerId);
                    if (server != null && server.Status == ServerStatusEnum.ONLINE)
                        client.ProcessServerSelection(server);
                    else
                        client.SendServerList();
                }
                else
                    client.SendServerList();
            }

        }
        /// <summary>
        /// Gère la requête de selection de serveur.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleServerSelection(ServerSelectionMessage message, AuthClient client)
        {
            lock (Locker)
                client.ProcessServerSelection(ServerRecord.GetWorldServer(message.serverId));
        }
        /// <summary>
        /// Dangereux! (Flood => Beaucoup de query)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleAcquaintanceSearch(AcquaintanceSearchMessage message, AuthClient client)
        {
            if (client.Account != null)
            {
                lock (Locker)
                {
                    AccountData searchAccount = AccountRecord.GetAccountByNickname(message.nickname);

                    if (searchAccount == null)
                    {
                        client.Send(new AcquaintanceSearchErrorMessage((sbyte)AcquaintanceErrorEnum.NO_RESULT));
                    }
                    else
                    {
                        client.Send(new AcquaintanceServerListMessage(ServerCharacterRecord.GetAccountActiveServers(searchAccount.Id)));

                    }
                }
            }
        }

    }
}
