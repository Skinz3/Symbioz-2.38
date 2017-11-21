using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Social
{
    public class SocialHandler
    {
        [MessageHandler]
        public static void HandleFriendGetList(FriendsGetListMessage message, WorldClient client)
        {
            client.Send(new FriendsListMessage(new FriendInformations[0]));
        }
        [MessageHandler]
        public static void IgnoredGetList(IgnoredGetListMessage message,WorldClient client)
        {
            client.Send(new IgnoredListMessage(new IgnoredInformations[0]));
           
        }
    }
}
