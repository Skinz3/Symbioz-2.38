using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Aspect
{
    class TitlesOrnamentsHandler
    {
        [MessageHandler]
        public static void HandleTitlesAndOrnamentsList(TitlesAndOrnamentsListRequestMessage message, WorldClient client)
        {
            client.Character.SendTitlesAndOrnamentsList();
        }
        [MessageHandler]
        public static void HandleOrnamentSelect(OrnamentSelectRequestMessage message, WorldClient client)
        {
            client.Character.SetOrnament(message.ornamentId);

        }
        [MessageHandler]
        public static void HandleTitleSelect(TitleSelectRequestMessage message, WorldClient client)
        {
            client.Character.SelectTitle(message.titleId);
        }
    }
}
