


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ContactLookErrorMessage : Message
{

public const ushort Id = 6045;
public override ushort MessageId
{
    get { return Id; }
}

public uint requestId;
        

public ContactLookErrorMessage()
{
}

public ContactLookErrorMessage(uint requestId)
        {
            this.requestId = requestId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(requestId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

requestId = reader.ReadVarUhInt();
            if (requestId < 0)
                throw new Exception("Forbidden value on requestId = " + requestId + ", it doesn't respect the following condition : requestId < 0");
            

}


}


}