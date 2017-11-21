


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class SubscriptionUpdateMessage : Message
{

public const ushort Id = 6616;
public override ushort MessageId
{
    get { return Id; }
}

public double timestamp;
        

public SubscriptionUpdateMessage()
{
}

public SubscriptionUpdateMessage(double timestamp)
        {
            this.timestamp = timestamp;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(timestamp);
            

}

public override void Deserialize(ICustomDataInput reader)
{

timestamp = reader.ReadDouble();
            if (timestamp < -9007199254740990 || timestamp > 9007199254740990)
                throw new Exception("Forbidden value on timestamp = " + timestamp + ", it doesn't respect the following condition : timestamp < -9007199254740990 || timestamp > 9007199254740990");
            

}


}


}