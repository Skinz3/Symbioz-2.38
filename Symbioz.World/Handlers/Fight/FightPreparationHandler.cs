using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Dialogs.DialogBox;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Network;
using Symbioz.World.Providers.Fights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.Fight
{
    class FightPreparationHandler
    {
        [MessageHandler]
        public static void HandleGameFightPlacementSwapPositionsRequest(GameFightPlacementSwapPositionsRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting && !client.Character.FighterMaster.IsReady && !client.Character.FighterMaster.Fight.Started)
            {
                var target = client.Character.FighterMaster.Fight.GetFighter((int)message.requestedId);

                if (target.CellId == message.cellId)
                {
                    target.PlacementSwap(client.Character.FighterMaster);
                }
            }
        }
        [MessageHandler]
        public static void HandleGameFightPlacementPositionRequest(GameFightPlacementPositionRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting && !client.Character.FighterMaster.IsReady && client.Character.Fighter.Fight.IsCellFree((short)message.cellId))
            {
                client.Character.FighterMaster.ChangePlacementPosition((short)message.cellId);
            }
        }
        [MessageHandler]
        public static void HandleGameContextQuitMessage(GameContextQuitMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
                client.Character.FighterMaster.Leave(true);
        }
        [MessageHandler]
        public static void HandleGameFightJoinRequestMessage(GameFightJoinRequestMessage message, WorldClient client)
        {
            var fight = client.Character.Map.Instance.GetFight(message.fightId);

            if (fight != null)
            {
                fight.TryJoin(client.Character, (int)message.fighterId);
            }
        }
        [MessageHandler]
        public static void HandleGameContextKickMessage(GameContextKickMessage message, WorldClient client)
        {
            var target = client.Character.Fighter.Fight.GetFighter((int)message.targetId);


            if (client.Character.Fighting && target != null && client.Character.Fighter.IsTeamLeader && client.Character.Fighter.IsFriendly(target))
            {
                target.Kick();
            }
        }
        [MessageHandler]
        public static void HandleFightOptionToggle(GameFightOptionToggleMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
                client.Character.Fighter.Team.Options.ToggleOption((FightOptionsEnum)message.option);
        }
        [MessageHandler]
        public static void HandleGameFightReady(GameFightReadyMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
                client.Character.FighterMaster.ToggleReady(message.isReady);
        }
        /// <summary>
        /// Stump => HandleGameRolePlayPlayerFightRequestMessage
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleGameRolePlayPlayerFightRequest(GameRolePlayPlayerFightRequestMessage message, WorldClient client)
        {
            Character target = client.Character.Map.Instance.GetEntity<Character>((long)message.targetId);

            if (target != null)
            {
                if (message.friendly)
                {
                    FighterRefusedReasonEnum fighterRefusedReasonEnum = client.Character.CanRequestFight(target);
                    if (fighterRefusedReasonEnum != FighterRefusedReasonEnum.FIGHTER_ACCEPTED)
                    {
                        client.Send(new ChallengeFightJoinRefusedMessage((ulong)client.Character.Id, (sbyte)fighterRefusedReasonEnum));
                    }
                    else
                    {
                        target.OpenRequestBox(new DualRequest(client.Character, target));
                    }
                }
                else
                {
                    FighterRefusedReasonEnum fighterRefusedReasonEnum = client.Character.CanAgress(target);
                    if (fighterRefusedReasonEnum != FighterRefusedReasonEnum.FIGHTER_ACCEPTED)
                    {
                        client.Send(new ChallengeFightJoinRefusedMessage((ulong)client.Character.Id, (sbyte)fighterRefusedReasonEnum));
                    }
                    else
                    {
                        FightAgression fight = FightProvider.Instance.CreateFightAgression(client.Character, target, (short)client.Character.CellId);

                        fight.RedTeam.AddFighter(target.CreateFighter(fight.RedTeam));

                        fight.BlueTeam.AddFighter(client.Character.CreateFighter(fight.BlueTeam));

                        fight.StartPlacement();
                    }
                }
            }
        }
        [MessageHandler]
        public static void HandleGameRolePlayPlayerFightFriendlyAnswer(GameRolePlayPlayerFightFriendlyAnswerMessage message, WorldClient client)
        {
            if (client.Character.IsInRequest() && client.Character.RequestBox is DualRequest)
            {
                if (message.accept)
                {
                    client.Character.RequestBox.Accept();
                }
                else
                {
                    if (client.Character == client.Character.RequestBox.Target)
                    {
                        client.Character.RequestBox.Deny();
                    }
                    else
                    {
                        client.Character.RequestBox.Cancel();
                    }
                }
            }

        }
    }
}
