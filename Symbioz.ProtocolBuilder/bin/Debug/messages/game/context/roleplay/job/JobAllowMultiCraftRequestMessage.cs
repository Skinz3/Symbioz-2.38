


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class JobAllowMultiCraftRequestMessage : Message
{

public const ushort Id = 5748;
public override ushort MessageId
{
    get { return Id; }
}

public bool enabled;
        

public JobAllowMultiCraftRequestMessage()
{
}

public JobAllowMultiCraftRequestMessage(bool enabled)
        {
            this.enabled = enabled;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(enabled);
            

}

public override void Deserialize(ICustomDataInput reader)
{

enabled = reader.ReadBoolean();
            

}


}


}