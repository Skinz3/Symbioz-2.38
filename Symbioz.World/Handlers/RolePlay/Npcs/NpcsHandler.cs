using SSync.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Protocol.Messages;
using System.Threading.Tasks;
using Symbioz.World.Network;
using Symbioz.World.Models.Entities;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Dialogs;

namespace Symbioz.World.Handlers.RolePlay.Npcs
{
    class NpcsHandler
    {
        [MessageHandler]
        public static void HandleNpcGenericActionRequest(NpcGenericActionRequestMessage message, WorldClient client)
        {
            if (message.npcMapId == client.Character.Map.Id)
            {
                Npc npc = client.Character.Map.Instance.GetEntity<Npc>((long)message.npcId);

                if (npc != null)
                {
                    npc.InteractWith(client.Character, (NpcActionTypeEnum)message.npcActionId);
                }
            }
            else
            {
                client.Character.ReplyError("Entity is not on map...");
            }
        }
        [MessageHandler]
        public static void HandleNpcDialogReplyMessage(NpcDialogReplyMessage message, WorldClient client)
        {
            if (client.Character.Dialog is NpcTalkDialog)
            {
                client.Character.GetDialog<NpcTalkDialog>().Reply(message.replyId);
            }
        }
    }
}
