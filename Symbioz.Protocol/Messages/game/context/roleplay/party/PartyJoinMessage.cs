


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyJoinMessage : AbstractPartyMessage
{

public const ushort Id = 5576;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte partyType;
        public ulong partyLeaderId;
        public sbyte maxParticipants;
        public Types.PartyMemberInformations[] members;
        public Types.PartyGuestInformations[] guests;
        public bool restricted;
        public string partyName;
        

public PartyJoinMessage()
{
}

public PartyJoinMessage(uint partyId, sbyte partyType, ulong partyLeaderId, sbyte maxParticipants, Types.PartyMemberInformations[] members, Types.PartyGuestInformations[] guests, bool restricted, string partyName)
         : base(partyId)
        {
            this.partyType = partyType;
            this.partyLeaderId = partyLeaderId;
            this.maxParticipants = maxParticipants;
            this.members = members;
            this.guests = guests;
            this.restricted = restricted;
            this.partyName = partyName;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(partyType);
            writer.WriteVarUhLong(partyLeaderId);
            writer.WriteSByte(maxParticipants);
            writer.WriteUShort((ushort)members.Length);
            foreach (var entry in members)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)guests.Length);
            foreach (var entry in guests)
            {
                 entry.Serialize(writer);
            }
            writer.WriteBoolean(restricted);
            writer.WriteUTF(partyName);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            partyType = reader.ReadSByte();
            if (partyType < 0)
                throw new Exception("Forbidden value on partyType = " + partyType + ", it doesn't respect the following condition : partyType < 0");
            partyLeaderId = reader.ReadVarUhLong();
            if (partyLeaderId < 0 || partyLeaderId > 9007199254740990)
                throw new Exception("Forbidden value on partyLeaderId = " + partyLeaderId + ", it doesn't respect the following condition : partyLeaderId < 0 || partyLeaderId > 9007199254740990");
            maxParticipants = reader.ReadSByte();
            if (maxParticipants < 0)
                throw new Exception("Forbidden value on maxParticipants = " + maxParticipants + ", it doesn't respect the following condition : maxParticipants < 0");
            var limit = reader.ReadUShort();
            members = new Types.PartyMemberInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 members[i] = Types.ProtocolTypeManager.GetInstance<Types.PartyMemberInformations>(reader.ReadShort());
                 members[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            guests = new Types.PartyGuestInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 guests[i] = new Types.PartyGuestInformations();
                 guests[i].Deserialize(reader);
            }
            restricted = reader.ReadBoolean();
            partyName = reader.ReadUTF();
            

}


}


}