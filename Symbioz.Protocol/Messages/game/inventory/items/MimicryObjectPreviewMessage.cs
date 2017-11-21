


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MimicryObjectPreviewMessage : Message
{

public const ushort Id = 6458;
public override ushort MessageId
{
    get { return Id; }
}

public Types.ObjectItem result;
        

public MimicryObjectPreviewMessage()
{
}

public MimicryObjectPreviewMessage(Types.ObjectItem result)
        {
            this.result = result;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

result.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

result = new Types.ObjectItem();
            result.Deserialize(reader);
            

}


}


}