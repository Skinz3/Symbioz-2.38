


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class PaddockItem : ObjectItemInRolePlay
{

public const short Id = 185;
public override short TypeId
{
    get { return Id; }
}

public Types.ItemDurability durability;
        

public PaddockItem()
{
}

public PaddockItem(ushort cellId, ushort objectGID, Types.ItemDurability durability)
         : base(cellId, objectGID)
        {
            this.durability = durability;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            durability.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            durability = new Types.ItemDurability();
            durability.Deserialize(reader);
            

}


}


}