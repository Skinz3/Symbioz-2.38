


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ObjectDeletedMessage : Message
{

public const ushort Id = 3024;
public override ushort MessageId
{
    get { return Id; }
}

public uint objectUID;
        

public ObjectDeletedMessage()
{
}

public ObjectDeletedMessage(uint objectUID)
        {
            this.objectUID = objectUID;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(objectUID);
            

}

public override void Deserialize(ICustomDataInput reader)
{

objectUID = reader.ReadVarUhInt();
            if (objectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
            

}


}


}