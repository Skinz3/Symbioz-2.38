using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights;
using Symbioz.World.Providers.Fights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Dialogs.DialogBox
{
    public class DualRequest : RequestBox
    {
        public DualRequest(Character source, Character target)
            : base(source, target)
        {

        }
        protected override void OnOpen()
        {
            Source.Client.Send(new GameRolePlayPlayerFightFriendlyRequestedMessage((int)Target.Id, (ulong)Source.Id, (ulong)Target.Id));
            Target.Client.Send(new GameRolePlayPlayerFightFriendlyRequestedMessage((int)Source.Id, (ulong)Source.Id, (ulong)Target.Id));
        }
        protected override void OnAccept()
        {
            Source.Client.Send(new GameRolePlayPlayerFightFriendlyAnsweredMessage((int)base.Target.Id, (ulong)base.Source.Id,
                (ulong)base.Target.Id, true));

            FightDual fight = FightProvider.Instance.CreateFightDual(Source, Target, (short)Source.CellId);

            fight.RedTeam.AddFighter(Target.CreateFighter(fight.RedTeam));

            fight.BlueTeam.AddFighter(Source.CreateFighter(fight.BlueTeam));

            fight.StartPlacement();
        }
        protected override void OnDeny()
        {
            Source.Client.Send(new GameRolePlayPlayerFightFriendlyAnsweredMessage((int)Target.Id, (ulong)Source.Id, (ulong)Target.Id, false));
        }
        protected override void OnCancel()
        {
            Target.Client.Send(new GameRolePlayPlayerFightFriendlyAnsweredMessage((int)Source.Id,
                (ulong)Source.Id, (ulong)Target.Id, false));

        }
    }
}
