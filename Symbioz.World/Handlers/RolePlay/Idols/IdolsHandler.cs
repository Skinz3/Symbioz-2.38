using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Network;
using Symbioz.World.Records.Idols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Idols
{
    public class IdolsHandler
    {
        [MessageHandler]
        public static void HandleIdolPartyRegisterRequestMessage(IdolPartyRegisterRequestMessage message, WorldClient client)
        {
           
        }
        [MessageHandler]
        public static void HandleIdolSelectRequestMessage(IdolSelectRequestMessage message, WorldClient client)
        {
            
        }
    }
}
