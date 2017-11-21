using SSync.Messages;
using Symbioz.Core;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Items;
using Symbioz.World.Network;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.Approach
{
    class StartupActionsHandler
    {
        public const bool ITEM_GENERATION_PERFECT = false;

        static object locker = new object();

        static Logger logger = new Logger();

        [MessageHandler]
        public static void HandleStartupActionsExecute(StartupActionsExecuteMessage message, WorldClient client)
        {
            client.Send(new StartupActionsListMessage(client.StartupActions.ConvertAll<StartupActionAddObject>(x => x.GetStartupActionAddObject()).ToArray()));
        }
        [MessageHandler]
        public static void HandleStartupActionsObjetAttribution(StartupActionsObjetAttributionMessage message, WorldClient client)
        {
            lock (locker)
            {
                if (client.StartupActions != null)
                {
                    StartupActionRecord action = client.StartupActions.FirstOrDefault(x => x.Id == message.actionId);

                    if (action != null)
                    {
                        AttributeAction(client, action, (long)message.characterId);
                        client.StartupActions.Remove(action);
                    }
                    else
                        client.Send(new StartupActionFinishedMessage(false, false, action.Id));
                }
                else
                {
                    logger.Error("StartupActions of client is null");
                    client.Disconnect();
                }
            }
        }
        private static void AttributeAction(WorldClient client, StartupActionRecord action, long characterId)
        {
            if (WorldServer.Instance.IsStatus(ServerStatusEnum.ONLINE))
            {
                try
                {
                    for (int i = 0; i < action.GIds.Count; i++)
                    {
                        ushort gid = action.GIds[i];
                        uint quantity = action.Quantities[i];
                        ItemRecord item = ItemRecord.GetItem(gid);

                        var character = client.GetAccountCharacter(characterId);
                        var characterItem = item.GetCharacterItem(characterId, quantity, ITEM_GENERATION_PERFECT);

                        CharacterItemRecord.AddQuietCharacterItem(character, characterItem);
                    }

                    client.Send(new StartupActionFinishedMessage(true, false, action.Id));
                }
                catch (Exception ex)
                {
                    logger.Error("Unable to attribute action to " + client.Account.Username + " :" + ex);
                    client.Send(new StartupActionFinishedMessage(false, false, action.Id));
                }
                finally
                {
                    action.RemoveInstantElement(); // How, its dangerous!
                }
            }
            else
            {
                client.Disconnect();
            }
        }
    }
}
