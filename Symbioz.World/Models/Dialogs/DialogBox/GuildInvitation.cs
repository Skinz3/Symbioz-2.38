using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Entities;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;

namespace Symbioz.World.Models.Dialogs.DialogBox
{
    public class GuildInvitation : RequestBox
    {
        public GuildInvitation(Character source, Character target) : base(source, target)
        {
        }
        protected override void OnAccept()
        {
            if (Source.Guild != null)
            {
                Source.Guild.Join(Target, false);
            }
            Source.Client.Send(new GuildInvitationStateRecruterMessage(Target.Name, (sbyte)GuildInvitationStateEnum.GUILD_INVITATION_OK));
            Target.Client.Send(new GuildInvitationStateRecruterMessage(Target.Name, (sbyte)GuildInvitationStateEnum.GUILD_INVITATION_OK));
            base.OnAccept();
        }
        protected override void OnDeny()
        {
            Source.Client.Send(new GuildInvitationStateRecruterMessage(Target.Name, (sbyte)GuildInvitationStateEnum.GUILD_INVITATION_CANCELED));
            Target.Client.Send(new GuildInvitationStateRecruterMessage(Target.Name, (sbyte)GuildInvitationStateEnum.GUILD_INVITATION_CANCELED));
            base.OnDeny();
        }
        protected override void OnOpen()
        {
            Source.Client.Send(new GuildInvitationStateRecruterMessage(Target.Name, (sbyte)GuildInvitationStateEnum.GUILD_INVITATION_SENT));
            Target.Client.Send(new GuildInvitationStateRecrutedMessage((sbyte)GuildInvitationStateEnum.GUILD_INVITATION_SENT));
            Target.Client.Send(new GuildInvitedMessage((ulong)Source.Id, Source.Name, Source.Guild.GetBasicGuildInformations()));
            base.OnOpen();
        }
        protected override void OnCancel()
        {
            Source.Client.Send(new GuildInvitationStateRecruterMessage(Target.Name, (sbyte)GuildInvitationStateEnum.GUILD_INVITATION_CANCELED));
            Target.Client.Send(new GuildInvitationStateRecruterMessage(Target.Name, (sbyte)GuildInvitationStateEnum.GUILD_INVITATION_CANCELED));
            base.OnCancel();
        }
    }
}
