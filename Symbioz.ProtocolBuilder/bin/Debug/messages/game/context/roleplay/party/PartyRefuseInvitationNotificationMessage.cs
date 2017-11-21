


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyRefuseInvitationNotificationMessage : AbstractPartyEventMessage
{

public const ushort Id = 5596;
public override ushort MessageId
{
    get { return Id; }
}

public ulong guestId;
        

public PartyRefuseInvitationNotificationMessage()
{
}

public PartyRefuseInvitationNotificationMessage(uint partyId, ulong guestId)
         : base(partyId)
        {
            this.guestId = guestId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhLong(guestId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            guestId = reader.ReadVarUhLong();
            if (guestId < 0 || guestId > 9007199254740990)
                throw new Exception("Forbidden value on guestId = " + guestId + ", it doesn't respect the following condition : guestId < 0 || guestId > 9007199254740990");
            

}


}


}