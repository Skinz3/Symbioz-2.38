using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.Protocol.Messages;
using Symbioz.Core;
using Symbioz.Protocol.Types;
using Symbioz.World.Providers.Parties;
using SSync.Messages;

namespace Symbioz.World.Models.Parties
{
    /// <summary>
    /// Représente un groupe abstrait de joueur dans un contexte RolePlay.
    /// </summary>
    public abstract class AbstractParty
    {
        public int Id
        {
            get;
            private set;
        }
        private bool Restricted
        {
            get;
            set;
        }
        public abstract PartyTypeEnum Type
        {
            get;
        }
        public abstract sbyte MaxParticipants
        {
            get;
        }
        public Character Leader
        {
            get;
            private set;
        }
        public string PartyName
        {
            get;
            private set;
        }
        public List<Character> Members
        {
            get;
            private set;
        }
        public List<Character> Guests
        {
            get;
            private set;
        }
        public bool IsFull
        {
            get
            {
                if (Count < MaxParticipants)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public int Count
        {
            get
            {
                return Members.Count() + Guests.Count() ;
            }
        }
        public AbstractParty(int partyId, Character leader)
        {
            this.Members = new List<Character>();
            this.Guests = new List<Character>();
            this.Leader = leader;
            this.Id = Id;
            this.PartyName = "";
        }

        public PartyMemberInformations[] GetPartyMembersInformations()
        {
            return Array.ConvertAll(Members.ToArray(), x => GetPartyMemberInformations(x));
        }

        public PartyInvitationMemberInformations[] GetPartyInvitationMembersInformations()
        {
            return Array.ConvertAll(Members.ToArray(), x => GetPartyInvitationMemberInformations(x));
        }

        public PartyGuestInformations[] GetPartyGuestsInformations()
        {
            return Array.ConvertAll(Guests.ToArray(), x => GetPartyGuestInformations(x));
        }

        public PartyMemberInformations GetPartyMemberInformations(Character character)
        {
            return new PartyMemberInformations((ulong)character.Id, character.Name, (byte)character.Level, character.Look.ToEntityLook(), (sbyte)character.Breed.Id, character.Record.Sex, (uint)character.Record.Stats.LifePoints, (uint)character.Record.Stats.MaxLifePoints, (ushort)character.Record.Stats.Prospecting.Total(), 1, (ushort)character.Record.Stats.Initiative.Total(), (sbyte)character.Record.Alignment.Side, character.Map.X, character.Map.Y, character.Map.Id, character.Map.SubAreaId, character.GetPlayerStatus(), new PartyCompanionMemberInformations[0]);
        }
        public PartyInvitationMemberInformations GetPartyInvitationMemberInformations(Character character)
        {
            return new PartyInvitationMemberInformations((ulong)character.Id, character.Name, (byte)character.Level, character.Look.ToEntityLook(), (sbyte)character.Breed.Id, character.Record.Sex, character.Map.X, character.Map.Y, character.Map.Id, character.Map.SubAreaId, new PartyCompanionMemberInformations[0]);
        }
        public PartyGuestInformations GetPartyGuestInformations(Character character)
        {
            return new PartyGuestInformations((ulong)character.Id, (ulong)Leader.Id, character.Name, character.Look.ToEntityLook(), character.Breed.Id, character.Record.Sex, character.GetPlayerStatus(), new PartyCompanionBaseInformations[0]);
        }

        public void Create(Character Creator, Character Invited)
        {
            Members.Add(Creator);
            Creator.Party = this;
            this.Leader = Creator;
            AbstractParty.SendPartyJoinMessage(Creator);
            Creator.Client.Send(new PartyUpdateMessage((uint)this.Id, this.GetPartyMemberInformations(Creator)));
            AbstractParty.SendPartyInvitationMessage(Invited, Creator, this);
        }

        public void AcceptInvitation(Character character)
        {
            if (IsFull)
            {
                character.Client.Send(new PartyCannotJoinErrorMessage((uint)this.Id, (sbyte)PartyJoinErrorEnum.PARTY_JOIN_ERROR_PARTY_FULL));
                return;
            }
            if (character.HasParty())
            {
                character.Party.Leave(character);
            }
            AddMember(character);
        }

        public void RefuseInvation(Character character)
        {
            if (Guests.Contains(character))
            {
                SendMembers(new PartyRefuseInvitationNotificationMessage((uint)this.Id, (ulong)character.Id));
                RemoveGuest(character);
            }
        }

        public void CancelInvitation(Character Canceller, int GuestId)
        {
            if (Canceller == Leader)
            {
                Character Guest = Guests.Find(x => x.Id == GuestId);
                if (Guest != null)
                {
                    Guest.Client.Send(new PartyInvitationCancelledForGuestMessage((uint)this.Id, (ulong)Canceller.Id));
                    RemoveGuest(Guest);
                    if (Count > 1)
                    {
                        SendMembers(new PartyCancelInvitationNotificationMessage((uint)Id, (ulong)Canceller.Id, (ulong)Guest.Id));
                    }
                }
            }
        }

        public void Abdicate(Character character = null)
        {
            if (Count <= 1)
                return;
            if (Leader == character)
                return;
            if (character == null)
            {
                this.Leader = Members.First(x => x.Id != this.Leader.Id);
                SendMembers(new PartyLeaderUpdateMessage((uint)this.Id, (ulong)this.Leader.Id));
            }
            else
            {
                if (Members.Contains(character))
                {
                    this.Leader = character;
                    SendMembers(new PartyLeaderUpdateMessage((uint)this.Id, (ulong)this.Leader.Id));
                }
            }
        }

        public bool Leave(Character character)
        {
            RemoveMember(character);

            if (!VerifiyIntegrity())
            {
                return false;
            }

            if (character == this.Leader && Count <= 1)
            {
                this.Abdicate();
            }
            SendMembers(new PartyMemberRemoveMessage((uint)this.Id, (ulong)character.Id));
            return true;
        }



        public void Delete()
        {
            SendToAll(new PartyLeaveMessage((uint)this.Id));

            foreach (Character character in Members)
            {
                character.Party = null;
            }
            foreach (Character character in Guests)
            {
                character.GuestedParties.Remove(this);
            }
            PartyProvider.Instance.Parties.Remove(this);
        }

        public void AddMember(Character character)
        {
            if (!Members.Contains(character))
            {
                Members.Add(character);
                character.Party = this;
                RemoveGuest(character);
                AbstractParty.SendPartyJoinMessage(character);
                character.Client.Send(new PartyUpdateMessage((uint)this.Id, this.GetPartyMemberInformations(character)));
                UpdateMember(character);
                RemoveGuest(character);


            }
        }
        public void UpdateMember(Character character)
        {
            if (character.Party != this)
                return;
            SendMembers(new PartyNewMemberMessage((uint)this.Id, this.GetPartyMemberInformations(character)));
        }
        public void RemoveMember(Character character)
        {
            if (Members.Contains(character))
            {
                Members.Remove(character);
                character.Party = null;
                character.Client.Send(new PartyLeaveMessage((uint)this.Id));
            }
        }

        public void AddGuest(Character character)
        {
            if (!Guests.Contains(character) && !Members.Contains(character))
            {
                Guests.Add(character);
                character.GuestedParties.Add(this);
                SendMembers(new PartyNewGuestMessage((uint)this.Id, this.GetPartyGuestInformations(character)));
            }
        }

        public void RemoveGuest(Character character)
        {
            if (Guests.Contains(character))
            {
                Guests.Remove(character);
                character.GuestedParties.Remove(this);
                VerifiyIntegrity();
            }
        }
        private bool VerifiyIntegrity()
        {
            if (Count <= 1)
            {
                this.Delete();
                return false;
            }
            else
            {
                return true;
            }
        }
        public void Kick(Character Kicked, Character Kicker)
        {
            if (Kicker == Leader)
            {
                RemoveMember(Kicked);
                Kicked.Client.Send(new PartyKickedByMessage((uint)this.Id, (ulong)Kicker.Id));
                SendMembers(new PartyMemberEjectedMessage((uint)this.Id, (ulong)Kicked.Id, (ulong)Kicker.Id));
                VerifiyIntegrity();
            }
        }

        public Character GetMember(long characterId)
        {
            return this.Members.Find(x => x.Id == characterId);
        }

        public static void SendPartyJoinMessage(Character character)
        {
            character.Client.Send(new PartyJoinMessage((uint)character.Party.Id, (sbyte)character.Party.Type,
                (ulong)character.Party.Leader.Id, character.Party.MaxParticipants, character.Party.GetPartyMembersInformations(),
                character.Party.GetPartyGuestsInformations(), character.Party.Restricted, character.Party.PartyName));
        }
        public static void SendPartyInvitationMessage(Character Invited, Character Invitor, AbstractParty Party)
        {
            Invited.Client.Send(new PartyInvitationMessage((uint)Party.Id, (sbyte)Party.Type, Party.PartyName, Party.MaxParticipants, (ulong)Invitor.Id, Invitor.Name, (ulong)Invited.Id));
            Party.AddGuest(Invited);
        }



        public void SendMembers(Message message)
        {
            foreach (Character character in Members)
            {
                character.Client.Send(message);
            }
        }
        public void SendGuests(Message message)
        {
            foreach (Character character in Guests)
            {
                character.Client.Send(message);
            }
        }

        public void SendToAll(Message message)
        {
            SendMembers(message);
            SendGuests(message);
        }
    }
}
