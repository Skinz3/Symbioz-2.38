using SSync;
using SSync.Messages;
using SSync.Transition;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Messages;
using Symbioz.Protocol.Selfmade.Types;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;
using System.Net;
using Symbioz.World.Records.Breeds;
using Symbioz.Protocol.Enums;

namespace Symbioz.World.Network
{
    public class WorldClient : SSyncClient
    {
        public AccountData Account
        {
            get;
            set;
        }

        public AccountInformationsRecord AccountInformations
        {
            get;
            set;
        }
        public bool InGame
        {
            get
            {
                return Character != null;
            }
        }
        public Character Character
        {
            get;
            set;
        }

        public List<CharacterRecord> Characters
        {
            get;
            set;
        }

        public bool HasStartupActions
        {
            get
            {
                return StartupActions.Count > 0;
            }
        }

        public List<StartupActionRecord> StartupActions
        {
            get;
            private set;
        }

        public WorldClient(Socket socket)
            : base(socket)
        {
            base.OnMessageHandleFailed += WorldClient_OnMessageHandleFailed;
            base.Send(new HelloGameMessage());
            WorldServer.Instance.AddClient(this);
        }
        void WorldClient_OnMessageHandleFailed(Message message)
        {
            if (Character != null && message != null)
                Character.ReplyError("Impossible d'executer l'action (" + message.ToString() + ").");
        }
        public override void OnClosed()
        {
            try
            {
                WorldServer.Instance.RemoveClient(this);
                if (Character != null)
                    Character.OnDisconnected();
                base.OnClosed();
            }
            catch (Exception ex)
            {
                Logger.Write<WorldClient>("Cannot disconnect client..." + ex.ToString(), ConsoleColor.Red);
            }
        }

        public void OnAccountReceived()
        {
            AccountInformations = AccountInformationsRecord.Load(Account.Id);
            Characters = CharacterRecord.GetCharactersByAccountId(Account.Id);
            StartupActions = StartupActionRecord.GetStartupActions(Account.Id);
            Send(new AuthenticationTicketAcceptedMessage());
            Send(new AccountCapabilitiesMessage(true, true, Account.Id, BreedRecord.AvailableBreedsFlags,
                  BreedRecord.AvailableBreedsFlags, 1));
            Send(new TrustStatusMessage(true, true));
            Send(new ServerSettingsMessage("fr", 1, 0, 0));
            Send(new ServerOptionalFeaturesMessage(new sbyte[] { 1, 2, 3, 4 }));
        }

        public CharacterRecord GetAccountCharacter(long id)
        {
            return Characters.FirstOrDefault(x => x.Id == id);
        }
        public void SendRaw(byte[] rawData)
        {
            this.Send(new RawDataMessage(rawData));
        }
        public void SendCharactersList()
        {
            CharacterBaseInformations[] characters = Characters.ConvertAll(x => (CharacterBaseInformations)x.GetCharacterBaseInformations()).ToArray();


            CharacterToRemodelInformations[] characterToRemodel =
                Characters.FindAll(x => x.RemodelingMaskEnum != CharacterRemodelingEnum.CHARACTER_REMODELING_NOT_APPLICABLE).
                ConvertAll(x => x.GetCharacterToRemodelInformations()).ToArray();

            if (characterToRemodel.Count() > 0)
            {
                Send(new CharactersListWithRemodelingMessage(characters, HasStartupActions, characterToRemodel));
            }
            else
            {
                Send(new CharactersListMessage(characters, HasStartupActions));
            }
        }


        public bool Ban()
        {
            bool result = false;

            if (Account != null)
            {
                MessagePool.SendRequest<BanConfirmMessage>(TransitionServerManager.Instance.AuthServer, new BanRequestMessage
                {
                    AccountId = Account.Id,
                }, delegate (BanConfirmMessage msg)
                {
                    result = true;
                    this.Disconnect();
                },
                delegate ()
                {
                });
            }
            return result;

        }

    }
}
