


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ObjectFeedMessage : Message
{

public const ushort Id = 6290;
public override ushort MessageId
{
    get { return Id; }
}

public uint objectUID;
        public uint foodUID;
        public uint foodQuantity;
        

public ObjectFeedMessage()
{
}

public ObjectFeedMessage(uint objectUID, uint foodUID, uint foodQuantity)
        {
            this.objectUID = objectUID;
            this.foodUID = foodUID;
            this.foodQuantity = foodQuantity;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(objectUID);
            writer.WriteVarUhInt(foodUID);
            writer.WriteVarUhInt(foodQuantity);
            

}

public override void Deserialize(ICustomDataInput reader)
{

objectUID = reader.ReadVarUhInt();
            if (objectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
            foodUID = reader.ReadVarUhInt();
            if (foodUID < 0)
                throw new Exception("Forbidden value on foodUID = " + foodUID + ", it doesn't respect the following condition : foodUID < 0");
            foodQuantity = reader.ReadVarUhInt();
            if (foodQuantity < 0)
                throw new Exception("Forbidden value on foodQuantity = " + foodQuantity + ", it doesn't respect the following condition : foodQuantity < 0");
            

}


}


}