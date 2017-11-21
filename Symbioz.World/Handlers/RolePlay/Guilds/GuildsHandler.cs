using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Network;
using Symbioz.World.Providers.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.World.Models.Dialogs.DialogBox;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Guilds;

namespace Symbioz.World.Handlers.RolePlay.Guilds
{
    class GuildsHandler
    {
        [MessageHandler]
        public static void HandleGuildMotdSetRequest(GuildMotdSetRequestMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
              //  message.content = message.content.Replace('"', ' ');
             //   message.content = message.content.Replace('\'', ' ');
                client.Character.Guild.SetMotd(client.Character.GuildMember, message.content);
            }
        }
        [MessageHandler]
        public static void HandleGuildCreationRequest(GuildCreationValidMessage message, WorldClient client)
        {
            GuildCreationResultEnum result = GuildProvider.Instance.CreateGuild(client.Character, message.guildName, message.guildEmblem);
            client.Character.OnGuildCreated(result);
        }
        [MessageHandler]
        public static void HandleGuildInvitationMessage(GuildInvitationMessage message, WorldClient client)
        {
            if (client.Character.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_INVITE_NEW_MEMBERS))
            {
                var target = WorldServer.Instance.GetOnlineClient((long)message.targetId);

                if (target == null)
                    client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 208);
                else if (target.Character.HasGuild)
                    client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 206);
                else if (target.Character.Busy)
                    client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 209);
                else if (!client.Character.Guild.CanAddMember())
                    client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 55, GuildProvider.MAX_MEMBERS_COUNT);
                else
                {
                    target.Character.OpenRequestBox(new GuildInvitation(client.Character, target.Character));
                }
            }
        }
        [MessageHandler]
        public static void HandleGuildKickRequestMessage(GuildKickRequestMessage message, WorldClient client)
        {
            if (!client.Character.HasGuild)
                return;

            var target = client.Character.Guild.GetMember((long)message.kickedId);

            if (client.Character.Guild == target.Guild)
            {
                bool kicked = target.Id != client.Character.Id;
                target.Guild.Leave(target, kicked);
            }
        }
        [MessageHandler]
        public static void HandleGuildInvitationAnswerMessage(GuildInvitationAnswerMessage message, WorldClient client)
        {
            if (!client.Character.HasGuild && client.Character.RequestBox is GuildInvitation)
            {
                if (message.accept)
                    client.Character.RequestBox.Accept();
                else
                    client.Character.RequestBox.Deny();
            }
        }
        [MessageHandler]
        public static void HandleGuildChangeMemberParameters(GuildChangeMemberParametersMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                GuildMemberInstance member = client.Character.Guild.GetMember((long)message.memberId);

                if (member != null && member.Guild == client.Character.Guild)
                {
                    member.ChangeParameters(client.Character.GuildMember, message.rights, message.rank, message.experienceGivenPercent);

                }
            }
        }
        [MessageHandler]
        public static void HandleGuildGetInformations(GuildGetInformationsMessage message, WorldClient client)
        {
            switch ((GuildInformationsTypeEnum)message.infoType)
            {
                case GuildInformationsTypeEnum.INFO_GENERAL:
                    SendGuildInformationsGeneral(client);
                    break;
                case GuildInformationsTypeEnum.INFO_MEMBERS:
                    SendGuildInformationsMembers(client);
                    break;
                case GuildInformationsTypeEnum.INFO_BOOSTS:
                    break;
                case GuildInformationsTypeEnum.INFO_PADDOCKS:
                    break;
                case GuildInformationsTypeEnum.INFO_HOUSES:
                    break;
                case GuildInformationsTypeEnum.INFO_TAX_COLLECTOR_GUILD_ONLY:
                    break;
                case GuildInformationsTypeEnum.INFO_TAX_COLLECTOR_ALLIANCE:
                    break;
                case GuildInformationsTypeEnum.INFO_TAX_COLLECTOR_LEAVE:
                    break;
            }
        }


        public static void SendGuildInformationsMembers(WorldClient client)
        {
            client.Send(client.Character.Guild.GetGuildInformationsMembersMessage());
        }

        public static void SendGuildInformationsGeneral(WorldClient client)
        {
            client.Send(client.Character.Guild.GetGuildInformationsGeneralMessage());
        }


    }
}
