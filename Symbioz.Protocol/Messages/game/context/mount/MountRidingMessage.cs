


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MountRidingMessage : Message
{

public const ushort Id = 5967;
public override ushort MessageId
{
    get { return Id; }
}

public bool isRiding;
        

public MountRidingMessage()
{
}

public MountRidingMessage(bool isRiding)
        {
            this.isRiding = isRiding;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(isRiding);
            

}

public override void Deserialize(ICustomDataInput reader)
{

isRiding = reader.ReadBoolean();
            

}


}


}