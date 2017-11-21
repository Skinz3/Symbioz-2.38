using Symbioz.Core;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Parties;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Chat
{
    public class ChatChannels
    {
        public static ChatServerWithObjectMessage GetChatServerWithObjectMessage(ChatActivableChannelsEnum channel, ObjectItem[] items, string message, WorldClient client)
        {
            return new ChatServerWithObjectMessage((sbyte)channel, message, (int)DateExtensions.DateTimeToUnixTimestamp(DateTime.Now), string.Empty, client.Character.Id,
                client.Character.Name, client.Account.Id, items);
        }
        public static ChatServerMessage GetChatServerMessage(ChatActivableChannelsEnum channel, string message, WorldClient client)
        {
            return new ChatServerMessage((sbyte)channel, message, (int)DateExtensions.DateTimeToUnixTimestamp(DateTime.Now),
                string.Empty, client.Character.Id, client.Character.Name, client.Account.Id);
        }
        public static ChatServerCopyMessage GetChatServerCopyMessage(ChatActivableChannelsEnum channel, string message, WorldClient client, WorldClient target)
        {
            return new ChatServerCopyMessage((sbyte)ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message,
                (int)DateExtensions.DateTimeToUnixTimestamp(DateTime.Now), client.Character.Record.Name,
                (uint)target.Character.Record.Id, target.Character.Record.Name);
        }
        [ChatChannel(ChatActivableChannelsEnum.CHANNEL_GUILD)]
        public static void Guild(WorldClient client, string message)
        {
            if (client.Character.HasGuild)
            {
                var chatServerMessage = GetChatServerMessage(ChatActivableChannelsEnum.CHANNEL_GUILD, message, client);
                client.Character.Guild.Send(chatServerMessage);
            }
        }
        [ChatChannel(ChatActivableChannelsEnum.CHANNEL_SEEK)]
        public static void Seek(WorldClient client, string message)
        {
            if (client.Character.Record.Muted)
            {
                client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 107);
                return;
            }
            var chatServerMessage = GetChatServerMessage(ChatActivableChannelsEnum.CHANNEL_SEEK, message, client);
            WorldServer.Instance.SendOnSubarea(chatServerMessage, client.Character.SubareaId);
        }
        
        [ChatChannel(ChatActivableChannelsEnum.CHANNEL_SALES)]
        public static void Sales(WorldClient client, string message)
        {
            if (client.Character.Record.Muted)
            {
                client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 107);
                return;
            }
            var chatServerMessage = GetChatServerMessage(ChatActivableChannelsEnum.CHANNEL_SALES, message, client);
            WorldServer.Instance.SendOnSubarea(chatServerMessage, client.Character.SubareaId);
        }

        [ChatChannel(ChatActivableChannelsEnum.CHANNEL_GLOBAL)]
        public static void Global(WorldClient client, string message)
        {
            if (client.Character.Record.Muted)
            {
                client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 107);
                return;
            }
            if (!client.Character.Fighting)
            {
                if (client.Character.Map != null)
                {
                    if (client.Character.Map.Instance.Mute && client.Account.Role == ServerRoleEnum.Player)
                    {
                        client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 113);
                        return;
                    }
                    client.Character.SendMap(GetChatServerMessage(ChatActivableChannelsEnum.CHANNEL_GLOBAL, message, client));
                }
                else
                    client.Character.OnChatError(ChatErrorEnum.CHAT_ERROR_INVALID_MAP);
            }
            else
            {
                client.Character.Fighter.Fight.Send(GetChatServerMessage(ChatActivableChannelsEnum.CHANNEL_GLOBAL, message, client));
                client.Character.Fighter.Fight.CheckFightEnd();

            }
        }
        [ChatChannel(ChatActivableChannelsEnum.CHANNEL_PARTY)]
        public static void Party(WorldClient client, string message)
        {
            if (client.Character.HasParty())
            {
                client.Character.Party.SendMembers(GetChatServerMessage(ChatActivableChannelsEnum.CHANNEL_PARTY, message, client));
            }
        }

        [ChatChannel(ChatActivableChannelsEnum.CHANNEL_ADMIN)]
        public static void Admin(WorldClient client, string message)
        {
            if (client.Character.Record.Muted)
            {
                client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 107);
                return;
            }
            if (client.Account.Role >= ServerRoleEnum.Animator)
            {
                WorldServer.Instance.Send(GetChatServerMessage(ChatActivableChannelsEnum.CHANNEL_ADMIN, message, client));
            }
            else
                client.Character.Reply("Vous n'avez pas les droits pour utiliser ce chat");
        }
        [ChatChannel(ChatActivableChannelsEnum.CHANNEL_NOOB)]
        public static void Noob(WorldClient client, string message)
        {
            if (client.Character.Record.Muted)
            {
                client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 107);
                return;
            }
            WorldServer.Instance.Send(GetChatServerMessage(ChatActivableChannelsEnum.CHANNEL_NOOB, message, client));
        }
        [ChatChannel(ChatActivableChannelsEnum.CHANNEL_TEAM)]
        public static void Team(WorldClient client, string message)
        {
            if (client.Character.Fighting)
            {
                client.Character.FighterMaster.Team.Send(GetChatServerMessage(ChatActivableChannelsEnum.CHANNEL_TEAM, message, client));
            }
        }

    }
}
