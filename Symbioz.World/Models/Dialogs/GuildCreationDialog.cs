using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Dialogs
{
    public class GuildCreationDialog : Dialog
    {
        public override DialogTypeEnum DialogType
        {
            get
            {
                return DialogTypeEnum.DIALOG_GUILD_CREATE;
            }
        }

        public GuildCreationDialog(Character character)
            : base(character)
        {

        }
        public override void Open()
        {
            Character.Client.Send(new GuildCreationStartedMessage());
        }
    }
}
