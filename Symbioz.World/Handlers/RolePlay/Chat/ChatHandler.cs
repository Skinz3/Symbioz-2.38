using SSync.Messages;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Handlers.RolePlay.Commands;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Chat
{

    class ChatHandler
    {
        public delegate void ChatHandlerDelegate(WorldClient client, string message);
        public static readonly Dictionary<ChatActivableChannelsEnum, ChatHandlerDelegate> ChatHandlers = new Dictionary<ChatActivableChannelsEnum, ChatHandlerDelegate>();

        [StartupInvoke("Chat Channels", StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var method in typeof(ChatChannels).GetMethods())
            {
                var attributes = method.GetCustomAttributes(typeof(ChatChannelAttribute), false);
                if (attributes.Count() > 0)
                {
                    var attribute = attributes[0] as ChatChannelAttribute;
                    ChatHandlers.Add(attribute.Channel, (ChatHandlerDelegate)Delegate.CreateDelegate(typeof(ChatHandlerDelegate), method));
                }
            }

        }
        static void Handle(WorldClient client, string message, ChatActivableChannelsEnum channel)
        {
            if (message.StartsWith(CommandsHandler.CommandsPrefix))
            {
                CommandsHandler.Handle(message, client);
                return;
            }

            var handler = ChatHandlers.FirstOrDefault(x => x.Key == channel);
            if (handler.Value != null)
                handler.Value(client, message);
            else
                client.Character.Reply("Ce chat n'est pas géré");
        }
        [MessageHandler]
        public static void HandleChatSmileyRequest(ChatSmileyRequestMessage message, WorldClient client)
        {
            client.Character.DisplaySmiley(message.smileyId);
        }
        [MessageHandler]
        public static void HandleChatClientMultiWithObject(ChatClientMultiWithObjectMessage message, WorldClient client)
        {
            client.Character.SendMap(ChatChannels.GetChatServerWithObjectMessage(ChatActivableChannelsEnum.CHANNEL_GLOBAL, message.objects, message.content, client));
        }
        [MessageHandler]
        public static void HandleChatMultiClient(ChatClientMultiMessage message, WorldClient client)
        {
            Handle(client, message.content, (ChatActivableChannelsEnum)message.channel);
        }
        [MessageHandler]
        public static void ChatClientPrivate(ChatClientPrivateMessage message, WorldClient client)
        {
            if (message.receiver == client.Character.Name)
            {
                client.Character.OnChatError(ChatErrorEnum.CHAT_ERROR_INTERIOR_MONOLOGUE);
                return;
            }

            WorldClient target = WorldServer.Instance.GetOnlineClient(message.receiver);

            if (target != null)
            {
                target.Send(ChatChannels.GetChatServerMessage(ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.content, client));
                client.Send(ChatChannels.GetChatServerCopyMessage(ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.content, client, target));
            }
            else
            {
                client.Character.OnChatError(ChatErrorEnum.CHAT_ERROR_RECEIVER_NOT_FOUND);
            }

        }
        public static void SendAnnounceMessage(string value, Color color)
        {
            WorldServer.Instance.Send(new TextInformationMessage(0, 0, new string[] { string.Format("<font color=\"#{0}\">{1}</font>", color.ToArgb().ToString("X"), value) }));
        }
        public static void SendNotificationToAllMessage(string message)
        {
            WorldServer.Instance.Send(new NotificationByServerMessage(24, new string[] { message }, true));
        }


    }
}
