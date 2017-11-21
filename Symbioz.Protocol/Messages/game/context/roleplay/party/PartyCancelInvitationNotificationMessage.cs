


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyCancelInvitationNotificationMessage : AbstractPartyEventMessage
{

public const ushort Id = 6251;
public override ushort MessageId
{
    get { return Id; }
}

public ulong cancelerId;
        public ulong guestId;
        

public PartyCancelInvitationNotificationMessage()
{
}

public PartyCancelInvitationNotificationMessage(uint partyId, ulong cancelerId, ulong guestId)
         : base(partyId)
        {
            this.cancelerId = cancelerId;
            this.guestId = guestId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhLong(cancelerId);
            writer.WriteVarUhLong(guestId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            cancelerId = reader.ReadVarUhLong();
            if (cancelerId < 0 || cancelerId > 9007199254740990)
                throw new Exception("Forbidden value on cancelerId = " + cancelerId + ", it doesn't respect the following condition : cancelerId < 0 || cancelerId > 9007199254740990");
            guestId = reader.ReadVarUhLong();
            if (guestId < 0 || guestId > 9007199254740990)
                throw new Exception("Forbidden value on guestId = " + guestId + ", it doesn't respect the following condition : guestId < 0 || guestId > 9007199254740990");
            

}


}


}