using SSync.Messages;
using SSync.Transition;
using Symbioz.Core;
using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Selfmade.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Network;
using Symbioz.World.Providers;
using Symbioz.World.Records;
using Symbioz.World.Records.Breeds;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Guilds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.Approach
{
    class CharacterHandler
    {
        static Logger logger = new Logger();

        public static char[] UnauthorizedNameContent = new char[] { '(', ')', '[', '{', '}', ']', '\'', ':', '<', '>', '?', '!' };

        [MessageHandler]
        public static void HandleCharacterList(CharactersListRequestMessage message, WorldClient client)
        {
            client.SendCharactersList();

        }
        [MessageHandler]
        public static void HandleCharacterDeletionRequest(CharacterDeletionRequestMessage message, WorldClient client)
        {
            if (!WorldServer.Instance.IsStatus(ServerStatusEnum.ONLINE))
            {
                client.Send(new CharacterDeletionErrorMessage((sbyte)CharacterDeletionErrorEnum.DEL_ERR_NO_REASON));
                return;
            }
            if (!client.InGame)
            {
                MessagePool.SendRequest<OnCharacterDeletionResultMessage>(TransitionServerManager.Instance.AuthServer, new OnCharacterDeletionMessage
                {
                    CharacterId = (long)message.characterId,
                },
                delegate (OnCharacterDeletionResultMessage msg)
                {
                    ProcessDeletion(msg.Succes, client, (long)message.characterId);
                });

            }
        }
        public static void ProcessDeletion(bool succes, WorldClient client, long characterId)
        {

            if (!succes)
            {
                client.Send(new CharacterDeletionErrorMessage((sbyte)CharacterDeletionErrorEnum.DEL_ERR_NO_REASON));
                return;
            }

            var record = client.GetAccountCharacter(characterId);
            if (record == null)
            {
                client.Send(new CharacterDeletionErrorMessage((sbyte)CharacterDeletionErrorEnum.DEL_ERR_NO_REASON));
                return;
            }
            client.Characters.Remove(record);

            if (record.GuildId != 0)
            {
                GuildRecord.RemoveWhereId(record);
            }

            DatabaseManager.GetInstance().RemoveWhereIdMethod((long)characterId);

            client.SendCharactersList();
        }
        [MessageHandler]
        public static void HandleCharacterNameSuggestionRequest(CharacterNameSuggestionRequestMessage message, WorldClient client)
        {
            if (!client.InGame)
                client.Send(new CharacterNameSuggestionSuccessMessage(StringUtils.RandomName()));
        }
        [MessageHandler]
        public static void HandleCharacterCreation(CharacterCreationRequestMessage message, WorldClient client)
        {
            if (!WorldServer.Instance.IsStatus(ServerStatusEnum.ONLINE))
            {
                client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.ERR_NO_REASON));
                return;
            }
            if (!client.InGame)
            {
                if (client.Characters.Count() == client.Account.CharacterSlots)
                {
                    client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.ERR_TOO_MANY_CHARACTERS));
                    return;
                }
                if (CharacterRecord.NameExist(message.name))
                {

                    client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.ERR_NAME_ALREADY_EXISTS));
                    return;
                }

                if (client.Account.Role < ServerRoleEnum.Animator)
                {
                    foreach (var value in message.name)
                    {
                        if (UnauthorizedNameContent.Contains(value))
                        {
                            client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.ERR_INVALID_NAME));
                            return;
                        }
                    }
                }
                if (message.name.Split(null).Count() > 1)
                {
                    client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.ERR_INVALID_NAME));
                    return;
                }


                long nextId = CharacterRecord.Characters.DynamicPop(x => x.Id);

                MessagePool.SendRequest<OnCharacterCreationResultMessage>(TransitionServerManager.Instance.AuthServer, new OnCharacterCreationMessage
                {
                    AccountId = client.Account.Id,
                    CharacterId = nextId,

                }, delegate (OnCharacterCreationResultMessage result)
                {
                    CreateCharacter(message, client, result.Succes, nextId);
                });
            }

        }
        static void CreateCharacter(CharacterCreationRequestMessage message, WorldClient client, bool succes, long id)
        {
            if (!succes)
            {
                client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.ERR_NO_REASON));
                return;
            }
            ContextActorLook look = BreedRecord.GetBreedLook(message.breed, message.sex, message.cosmeticId, message.colors);
            CharacterRecord record = CharacterRecord.New(id, message.name, client.Account.Id, look, message.breed, message.cosmeticId, message.sex);

            record.AddInstantElement();
            client.Character = new Character(client, record, true);
            logger.White("Character " + record.Name + " created");
            ProcessSelection(client);
        }
        static void ProcessSelection(WorldClient client)
        {
            client.Send(new NotificationListMessage(new int[] { 2147483647 }));
            client.Send(new CharacterSelectedSuccessMessage(client.Character.Record.GetCharacterBaseInformations(),
               false));
            client.Send(new CharacterCapabilitiesMessage(4095));
            client.Send(new SequenceNumberRequestMessage());
            client.Character.RefreshEmotes();
            client.Character.RefreshSpells();
            client.Character.Inventory.Refresh();
            client.Character.RefreshShortcuts();
            client.Character.RefreshJobs();
            client.Character.RefreshAlignment();
            client.Character.SetRestrictions();
            client.Character.RefreshArenaInfos();
            client.Character.OnConnected();
            client.Character.RefreshGuild();
            client.Send(new EnabledChannelsMessage(new sbyte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13 },
               new sbyte[0]));
            client.Character.SafeConnection();

            client.Send(new CharacterLoadingCompleteMessage());

        }
        [MessageHandler]
        public static void HandleCharacterSelection(CharacterSelectionMessage message, WorldClient client)
        {
            if (!client.InGame)
            {
                client.Character = new Character(client, client.GetAccountCharacter((long)message.id), false);
                ProcessSelection(client);
            }
        }
        [MessageHandler]
        public static void HandleCharacterSelectionWithRemodel(CharacterSelectionWithRemodelMessage message, WorldClient client)
        {
            if (!client.InGame)
            {
                CharacterRecord record = client.GetAccountCharacter((long)message.id);

                if (record.RemodelingMaskEnum.HasFlag(CharacterRemodelingEnum.CHARACTER_REMODELING_NAME))
                {
                    if (!CharacterRemodelingProvider.Instance.RemodelName(record, message.remodel.name))
                    {
                        client.Send(new CharacterCreationResultMessage((sbyte)CharacterCreationResultEnum.ERR_NAME_ALREADY_EXISTS));
                        return;
                    }
                }

                if (record.RemodelingMaskEnum.HasFlag(CharacterRemodelingEnum.CHARACTER_REMODELING_BREED))
                {
                    CharacterRemodelingProvider.Instance.RemodelBreed(record, message.remodel.breed, message.remodel.cosmeticId);
                }

                if (record.RemodelingMaskEnum.HasFlag(CharacterRemodelingEnum.CHARACTER_REMODELING_GENDER))
                {

                }

                if (record.RemodelingMaskEnum.HasFlag(CharacterRemodelingEnum.CHARACTER_REMODELING_COSMETIC))
                {
                    CharacterRemodelingProvider.Instance.RemodelCosmetic(record, message.remodel.cosmeticId);
                }

                if (record.RemodelingMaskEnum.HasFlag(CharacterRemodelingEnum.CHARACTER_REMODELING_COLORS))
                {
                    CharacterRemodelingProvider.Instance.RemodelColors(record, message.remodel.colors);

                }

                record.RemodelingMaskEnum = CharacterRemodelingEnum.CHARACTER_REMODELING_NOT_APPLICABLE;
                client.Character = new Character(client, record, false);
                ProcessSelection(client);
            }
        }
    }
}
