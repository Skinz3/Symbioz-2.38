


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ObjectItemQuantity : Item
{

public const short Id = 119;
public override short TypeId
{
    get { return Id; }
}

public uint objectUID;
        public uint quantity;
        

public ObjectItemQuantity()
{
}

public ObjectItemQuantity(uint objectUID, uint quantity)
        {
            this.objectUID = objectUID;
            this.quantity = quantity;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(objectUID);
            writer.WriteVarUhInt(quantity);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            objectUID = reader.ReadVarUhInt();
            if (objectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
            quantity = reader.ReadVarUhInt();
            if (quantity < 0)
                throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
            

}


}


}