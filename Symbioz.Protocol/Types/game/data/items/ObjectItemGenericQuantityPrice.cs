


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ObjectItemGenericQuantityPrice : ObjectItemGenericQuantity
{

public const short Id = 494;
public override short TypeId
{
    get { return Id; }
}

public uint price;
        

public ObjectItemGenericQuantityPrice()
{
}

public ObjectItemGenericQuantityPrice(ushort objectGID, uint quantity, uint price)
         : base(objectGID, quantity)
        {
            this.price = price;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(price);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            price = reader.ReadVarUhInt();
            if (price < 0)
                throw new Exception("Forbidden value on price = " + price + ", it doesn't respect the following condition : price < 0");
            

}


}


}