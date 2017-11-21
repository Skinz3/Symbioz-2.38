


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ItemDurability
{

public const short Id = 168;
public virtual short TypeId
{
    get { return Id; }
}

public short durability;
        public short durabilityMax;
        

public ItemDurability()
{
}

public ItemDurability(short durability, short durabilityMax)
        {
            this.durability = durability;
            this.durabilityMax = durabilityMax;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteShort(durability);
            writer.WriteShort(durabilityMax);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

durability = reader.ReadShort();
            durabilityMax = reader.ReadShort();
            

}


}


}