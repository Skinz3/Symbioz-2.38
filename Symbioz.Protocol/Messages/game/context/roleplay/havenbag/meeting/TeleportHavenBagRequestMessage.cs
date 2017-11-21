


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class TeleportHavenBagRequestMessage : Message
{

public const ushort Id = 6647;
public override ushort MessageId
{
    get { return Id; }
}

public ulong guestId;
        

public TeleportHavenBagRequestMessage()
{
}

public TeleportHavenBagRequestMessage(ulong guestId)
        {
            this.guestId = guestId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(guestId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

guestId = reader.ReadVarUhLong();
            if (guestId < 0 || guestId > 9007199254740990)
                throw new Exception("Forbidden value on guestId = " + guestId + ", it doesn't respect the following condition : guestId < 0 || guestId > 9007199254740990");
            

}


}


}