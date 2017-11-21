


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class WarnOnPermaDeathMessage : Message
{

public const ushort Id = 6512;
public override ushort MessageId
{
    get { return Id; }
}

public bool enable;
        

public WarnOnPermaDeathMessage()
{
}

public WarnOnPermaDeathMessage(bool enable)
        {
            this.enable = enable;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(enable);
            

}

public override void Deserialize(ICustomDataInput reader)
{

enable = reader.ReadBoolean();
            

}


}


}