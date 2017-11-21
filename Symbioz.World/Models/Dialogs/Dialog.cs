using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Dialogs
{
    public abstract class Dialog
    {
        public Character Character { get; set; }

        public Dialog(Character character)
        {
            this.Character = character;
        }

        public abstract DialogTypeEnum DialogType { get; }

        public abstract void Open();

        public virtual void Close()
        {
            Character.Client.Character.Dialog = null;
        }
        protected void LeaveDialogMessage()
        {
            Character.Client.Send(new LeaveDialogMessage((sbyte)DialogType));
        }
    }
}
