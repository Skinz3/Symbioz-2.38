using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Dialogs;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Exchanges
{
    public abstract class Exchange : Dialog
    {
        public override DialogTypeEnum DialogType
        {
            get { return DialogTypeEnum.DIALOG_EXCHANGE; }
        }
        public abstract ExchangeTypeEnum ExchangeType { get; }

        protected bool Succes = false;

        public Exchange(Character character)
            : base(character)
        {

        }

        public abstract void MoveItem(uint uid, int quantity);

        public abstract void Ready(bool ready, ushort step);

        public abstract void MoveKamas(int quantity);

        public override void Close()
        {
            Character.Client.Send(new ExchangeLeaveMessage((sbyte)this.DialogType, this.Succes));
            base.Close();

        }
    }
}
