


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ObjectItemToSellInNpcShop : ObjectItemMinimalInformation
{

public const short Id = 352;
public override short TypeId
{
    get { return Id; }
}

public uint objectPrice;
        public string buyCriterion;
        

public ObjectItemToSellInNpcShop()
{
}

public ObjectItemToSellInNpcShop(ushort objectGID, Types.ObjectEffect[] effects, uint objectPrice, string buyCriterion)
         : base(objectGID, effects)
        {
            this.objectPrice = objectPrice;
            this.buyCriterion = buyCriterion;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(objectPrice);
            writer.WriteUTF(buyCriterion);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            objectPrice = reader.ReadVarUhInt();
            if (objectPrice < 0)
                throw new Exception("Forbidden value on objectPrice = " + objectPrice + ", it doesn't respect the following condition : objectPrice < 0");
            buyCriterion = reader.ReadUTF();
            

}


}


}