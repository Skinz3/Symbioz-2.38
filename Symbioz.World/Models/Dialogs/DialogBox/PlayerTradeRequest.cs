using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Exchanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Dialogs.DialogBox
{
    public class PlayerTradeRequest : RequestBox
    {
        public PlayerTradeRequest(Character source, Character target)
            : base(source, target)
        {
        }
        protected override void OnOpen()
        {
            this.Send(new ExchangeRequestedTradeMessage((sbyte)ExchangeTypeEnum.PLAYER_TRADE, (ulong)Source.Id, (ulong)Target.Id));
        }
        protected override void OnAccept()
        {
            Source.Dialog = new PlayerTradeExchange(base.Source, base.Target);
            Target.Dialog = new PlayerTradeExchange(base.Target,base.Source);
            Source.Dialog.Open();
        }
        protected override void OnDeny()
        {
            Send(new ExchangeLeaveMessage((sbyte)DialogTypeEnum.DIALOG_EXCHANGE, false));
        }
        protected override void OnCancel()
        {
            base.Deny();
        }
    }
}
