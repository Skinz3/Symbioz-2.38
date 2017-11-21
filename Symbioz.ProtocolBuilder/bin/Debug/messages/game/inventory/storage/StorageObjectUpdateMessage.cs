


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class StorageObjectUpdateMessage : Message
{

public const ushort Id = 5647;
public override ushort MessageId
{
    get { return Id; }
}

public Types.ObjectItem @object;
        

public StorageObjectUpdateMessage()
{
}

public StorageObjectUpdateMessage(Types.ObjectItem @object)
        {
            this.@object = @object;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

@object.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

@object = new Types.ObjectItem();
            @object.Deserialize(reader);
            

}


}


}