


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyInvitationMessage : AbstractPartyMessage
{

public const ushort Id = 5586;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte partyType;
        public string partyName;
        public sbyte maxParticipants;
        public ulong fromId;
        public string fromName;
        public ulong toId;
        

public PartyInvitationMessage()
{
}

public PartyInvitationMessage(uint partyId, sbyte partyType, string partyName, sbyte maxParticipants, ulong fromId, string fromName, ulong toId)
         : base(partyId)
        {
            this.partyType = partyType;
            this.partyName = partyName;
            this.maxParticipants = maxParticipants;
            this.fromId = fromId;
            this.fromName = fromName;
            this.toId = toId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(partyType);
            writer.WriteUTF(partyName);
            writer.WriteSByte(maxParticipants);
            writer.WriteVarUhLong(fromId);
            writer.WriteUTF(fromName);
            writer.WriteVarUhLong(toId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            partyType = reader.ReadSByte();
            if (partyType < 0)
                throw new Exception("Forbidden value on partyType = " + partyType + ", it doesn't respect the following condition : partyType < 0");
            partyName = reader.ReadUTF();
            maxParticipants = reader.ReadSByte();
            if (maxParticipants < 0)
                throw new Exception("Forbidden value on maxParticipants = " + maxParticipants + ", it doesn't respect the following condition : maxParticipants < 0");
            fromId = reader.ReadVarUhLong();
            if (fromId < 0 || fromId > 9007199254740990)
                throw new Exception("Forbidden value on fromId = " + fromId + ", it doesn't respect the following condition : fromId < 0 || fromId > 9007199254740990");
            fromName = reader.ReadUTF();
            toId = reader.ReadVarUhLong();
            if (toId < 0 || toId > 9007199254740990)
                throw new Exception("Forbidden value on toId = " + toId + ", it doesn't respect the following condition : toId < 0 || toId > 9007199254740990");
            

}


}


}