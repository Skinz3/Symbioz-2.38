
using SSync.Messages;
using SSync.Transition;
using Symbioz.Auth.Records;
using Symbioz.Auth.Transition;
using Symbioz.Core;
using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade;
using Symbioz.Protocol.Selfmade.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symbioz.Auth.Handlers
{
    /// <summary>
    /// Gère les messages transitoires entre Auth <=> World
    /// </summary>
    class TransitionHandler
    {
        static Logger logger = new Logger();

        /// <summary>
        /// Gère le message de connexion d'un serveur world.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleWorldRegistration(WorldRegistrationRequestMessage message, TransitionClient client)
        {
            client.ServerId = message.ServerId;
            ServersManager.Instance.RegisterWorld(message.ServerId, message.Name, message.Type,
                message.Host, message.Port);
        }
        /// <summary>
        /// Gère la requête d'informations de compte d'un serveur World.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleAccountRequestMessage(AccountRequestMessage message, TransitionClient client)
        {

            var account = AuthTicketsManager.Instance.GetAccount(message.Ticket);

            if (account != null)
                client.SendReply(new AccountMessage(account), message.Guid);
        }
        /// <summary>
        /// Gère la requête de création de personnage d'un serveur world.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleCharacterCreationRequest(OnCharacterCreationMessage message, TransitionClient client)
        {
            bool succes;

            try
            {
                succes = ServerCharacterRecord.Add(message.CharacterId, message.AccountId, client.ServerId);

            }
            catch (Exception ex)
            {
                logger.Alert("Unable to create character :" + ex.ToString());
                succes = false;
            }
            client.SendReply(new OnCharacterCreationResultMessage(succes), message.Guid);
        }
        /// <summary>
        /// Gère la requête de reset d'une base de donnée d'un serveur World.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleDatabaseReset(ResetDatabaseMessage message, TransitionClient client)
        {
            ServersManager.Instance.ResetAuthDatabase(client.ServerId);
            logger.Color2("Database Restored for server " + client.ServerId);
        }
        /// <summary>
        /// Gère la requête de modification de status d'un serveur World.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleSetServerStatus(SetServerStatusMessage message, TransitionClient client)
        {
            var server = ServerRecord.GetWorldServer(client.ServerId);
            ServersManager.Instance.SetServerStatus(client.ServerId, message.Status);
            logger.White("Server " + server.Name + " status is now " + message.Status.ToString());
        }
        /// <summary>
        /// Gère la requête de supression d'un personnage d'un serveur World.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleCharacterDeletion(OnCharacterDeletionMessage message, TransitionClient client)
        {
            bool succes = ServerCharacterRecord.DeleteCharacter(message.CharacterId, client.ServerId);
            client.SendReply(new OnCharacterDeletionResultMessage(succes), message.Guid);
        }
        [MessageHandler]
        public static void HandleBanRequest(BanRequestMessage message, TransitionClient client)
        {
            AccountRecord record = AccountRecord.GetAccountRecord(message.AccountId);

            if (record != null)
            {
                try
                {
                    record.Ban();
                    client.SendReply(new BanConfirmMessage(), message.Guid);

                }
                catch (Exception ex)
                {
                    logger.Error("Unable to ban " + ex);
                }
            }
            else
            {
                logger.Error("Unable to ban, account is not finded.");
            }
        }
    }
}
