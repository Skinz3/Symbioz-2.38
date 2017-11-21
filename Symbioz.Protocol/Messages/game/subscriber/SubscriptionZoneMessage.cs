


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class SubscriptionZoneMessage : Message
{

public const ushort Id = 5573;
public override ushort MessageId
{
    get { return Id; }
}

public bool active;
        

public SubscriptionZoneMessage()
{
}

public SubscriptionZoneMessage(bool active)
        {
            this.active = active;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(active);
            

}

public override void Deserialize(ICustomDataInput reader)
{

active = reader.ReadBoolean();
            

}


}


}