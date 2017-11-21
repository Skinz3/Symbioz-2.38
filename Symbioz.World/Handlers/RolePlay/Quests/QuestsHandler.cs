using SSync.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Messages;
using Symbioz.World.Network;

namespace Symbioz.World.Handlers.RolePlay.Quests
{
    class QuestsHandler
    {
        [MessageHandler]
        public static void HandleQuestListRequest(QuestListRequestMessage message, WorldClient client)
        {
          //  client.Send(new QuestListMessage(new ushort[0], new ushort[0], new Protocol.Types.QuestActiveInformations[0],
            //    new ushort[0]));
        }
    }
}
