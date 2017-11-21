


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyLeaderUpdateMessage : AbstractPartyEventMessage
{

public const ushort Id = 5578;
public override ushort MessageId
{
    get { return Id; }
}

public ulong partyLeaderId;
        

public PartyLeaderUpdateMessage()
{
}

public PartyLeaderUpdateMessage(uint partyId, ulong partyLeaderId)
         : base(partyId)
        {
            this.partyLeaderId = partyLeaderId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhLong(partyLeaderId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            partyLeaderId = reader.ReadVarUhLong();
            if (partyLeaderId < 0 || partyLeaderId > 9007199254740990)
                throw new Exception("Forbidden value on partyLeaderId = " + partyLeaderId + ", it doesn't respect the following condition : partyLeaderId < 0 || partyLeaderId > 9007199254740990");
            

}


}


}