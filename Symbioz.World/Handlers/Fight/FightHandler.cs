using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Network;
using Symbioz.World.Providers.Maps.Path;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.Fight
{
    class FightHandler
    {
        [MessageHandler]
        public static void HandleTurnReady(GameFightTurnReadyMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
                client.Character.FighterMaster.ToggleSyncReady(message.isReady);
        }
        [MessageHandler]
        public static void HandleFightTurnFinishMessage(GameFightTurnFinishMessage message, WorldClient client)
        {
            if (client.Character.Fighting && !client.Character.Fighter.Fight.Ended && !client.Character.Fighter.Fight.SequencesManager.Sequencing)
            {
                client.Character.Fighter.PassTurn();
            }
        }
        [MessageHandler]
        public static void HandleGameActionAcknowledgement(GameActionAcknowledgementMessage message, WorldClient client)
        {


        }
        [MessageHandler]
        public static void HandleGameActionFightSpellCastRequest(GameActionFightCastRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                CharacterSpell spell = client.Character.Fighter.GetSpell(message.spellId);

                if (spell != null)
                    client.Character.Fighter.CastSpell(spell.Template, spell.Grade, message.cellId);
            }
        }
        /// <summary>
        /// A voir
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        [MessageHandler]
        public static void HandleGameActionFightCastOnTargetRequest(GameActionFightCastOnTargetRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                CharacterSpell spell = client.Character.Fighter.GetSpell(message.spellId);
                Fighter fighter = client.Character.Fighter.Fight.GetFighter((int)message.targetId);

                if (spell != null && fighter != null)
                {
                    if (fighter.CanBeTargeted(client.Character))
                        client.Character.Fighter.CastSpell(spell.Template, spell.Grade, fighter.CellId, fighter.Id);
                }
                else
                {
                    client.Character.ReplyError("Fatal error while casting spell!!");
                }
            }
        }
        [MessageHandler]
        public static void HandleChallengeTargetsListRequest(ChallengeTargetsListRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                client.Character.Fighter.ShowChallengeTargetsList(message.challengeId);
            }
        }
        [MessageHandler]
        public static void HandleShowCellRequest(ShowCellRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                client.Character.Fighter.Team.ShowCell(client.Character.Fighter, message.cellId);
            }
        }
    }
}
