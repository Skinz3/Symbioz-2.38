


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AccessoryPreviewMessage : Message
{

public const ushort Id = 6517;
public override ushort MessageId
{
    get { return Id; }
}

public Types.EntityLook look;
        

public AccessoryPreviewMessage()
{
}

public AccessoryPreviewMessage(Types.EntityLook look)
        {
            this.look = look;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

look.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

look = new Types.EntityLook();
            look.Deserialize(reader);
            

}


}


}