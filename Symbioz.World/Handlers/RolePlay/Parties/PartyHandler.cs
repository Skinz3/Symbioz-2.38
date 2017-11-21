using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Dialogs.DialogBox;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Parties;
using Symbioz.World.Network;
using Symbioz.World.Providers.Parties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Parties
{
    /// <summary>
    /// Représente les handlers du systeme de groupe Dofus.
    /// </summary>
    public class PartyHandler
    {
        [MessageHandler]
        public static void HandlePartyInvitationRequest(PartyInvitationRequestMessage message, WorldClient client)
        {
            WorldClient target = WorldServer.Instance.GetOnlineClient(message.name);

            if (target != null)
            {
                client.Character.InviteParty(target.Character);
            }
            else
            {
                client.Character.OnPartyJoinError(PartyJoinErrorEnum.PARTY_JOIN_ERROR_PLAYER_NOT_FOUND);
            }
        }
        [MessageHandler]
        public static void HandlePartyInvitationDetailsRequest(PartyInvitationDetailsRequestMessage message, WorldClient client)
        {
            AbstractParty party = PartyProvider.Instance.Parties.Find(x => x.Id == message.partyId);
            if (party != null)
            {
                client.Send(new PartyInvitationDetailsMessage((uint)party.Id, (sbyte)party.Type, party.PartyName, (ulong)party.Leader.Id, party.Leader.Name, (ulong)party.Leader.Id, party.GetPartyInvitationMembersInformations(), party.GetPartyGuestsInformations()));
            }
            else
            {
                client.Send(new PartyCannotJoinErrorMessage(message.partyId, (sbyte)PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_NOT_FOUND));
            }
        }
        [MessageHandler]
        public static void HandlePartyAcceptInvitation(PartyAcceptInvitationMessage message, WorldClient client)
        {
            var party = client.Character.GuestedParties.Find(x => x.Id == message.partyId);
            if (party != null)
            {
                party.AcceptInvitation(client.Character);
            }
            else
            {
                client.Send(new PartyCannotJoinErrorMessage(0, (sbyte)PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_NOT_FOUND));
            }
        }
        [MessageHandler]
        public static void HandlePartyRefuseInvitation(PartyRefuseInvitationMessage message, WorldClient client)
        {
            AbstractParty party = PartyProvider.Instance.Parties.Find(x => x.Id == message.partyId);
            if (party != null)
            {
                party.RefuseInvation(client.Character);
            }
        }
        [MessageHandler]
        public static void HandlePartyCancelInvitation(PartyCancelInvitationMessage message, WorldClient client)
        {
            if (client.Character.HasParty())
            {
                client.Character.Party.CancelInvitation(client.Character, (int)message.guestId);
            }
        }
        [MessageHandler]
        public static void HandlePartyLeaveRequest(PartyLeaveRequestMessage message, WorldClient client)
        {
            if (client.Character.HasParty())
            {
                client.Character.Party.Leave(client.Character);
            }
            else
            {
                client.Send(new PartyCannotJoinErrorMessage(0, (sbyte)PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_NOT_FOUND));
            }
        }
        [MessageHandler]
        public static void HandlePartyAbdicateThrone(PartyAbdicateThroneMessage message, WorldClient client)
        {
            if (client.Character.HasParty())
            {
                var target = client.Character.Party.GetMember((long)message.playerId);

                if (target != null)
                    client.Character.Party.Abdicate(target);
            }
            else
            {
                client.Send(new PartyCannotJoinErrorMessage(0, (sbyte)PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_NOT_FOUND));
            }
        }
        [MessageHandler]
        public static void HandlePartyKickRequest(PartyKickRequestMessage message, WorldClient client)
        {
            if (client.Character.HasParty())
            {
                var target = client.Character.Party.GetMember((long)message.playerId);

                if (target != null)
                    client.Character.Party.Kick(target, client.Character);
            }
            else
            {
                client.Send(new PartyCannotJoinErrorMessage(0, (sbyte)PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_NOT_FOUND));
            }
        }
    }
}
