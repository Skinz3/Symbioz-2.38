using Symbioz.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Chat
{
    public class ChatChannelAttribute : Attribute
    {
        public ChatActivableChannelsEnum Channel { get; set; }

        public ChatChannelAttribute(ChatActivableChannelsEnum channel)
        {
            this.Channel = channel;
        }
    }
}
