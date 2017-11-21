


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ObjectItemInformationWithQuantity : ObjectItemMinimalInformation
{

public const short Id = 387;
public override short TypeId
{
    get { return Id; }
}

public uint quantity;
        

public ObjectItemInformationWithQuantity()
{
}

public ObjectItemInformationWithQuantity(ushort objectGID, Types.ObjectEffect[] effects, uint quantity)
         : base(objectGID, effects)
        {
            this.quantity = quantity;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(quantity);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            quantity = reader.ReadVarUhInt();
            if (quantity < 0)
                throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
            

}


}


}