


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DareCancelRequestMessage : Message
{

public const ushort Id = 6680;
public override ushort MessageId
{
    get { return Id; }
}

public double dareId;
        

public DareCancelRequestMessage()
{
}

public DareCancelRequestMessage(double dareId)
        {
            this.dareId = dareId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(dareId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

dareId = reader.ReadDouble();
            if (dareId < 0 || dareId > 9007199254740990)
                throw new Exception("Forbidden value on dareId = " + dareId + ", it doesn't respect the following condition : dareId < 0 || dareId > 9007199254740990");
            

}


}


}