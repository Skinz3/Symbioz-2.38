


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class MonsterBoosts
{

public const short Id = 497;
public virtual short TypeId
{
    get { return Id; }
}

public uint id;
        public ushort xpBoost;
        public ushort dropBoost;
        

public MonsterBoosts()
{
}

public MonsterBoosts(uint id, ushort xpBoost, ushort dropBoost)
        {
            this.id = id;
            this.xpBoost = xpBoost;
            this.dropBoost = dropBoost;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(id);
            writer.WriteVarUhShort(xpBoost);
            writer.WriteVarUhShort(dropBoost);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

id = reader.ReadVarUhInt();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
            xpBoost = reader.ReadVarUhShort();
            if (xpBoost < 0)
                throw new Exception("Forbidden value on xpBoost = " + xpBoost + ", it doesn't respect the following condition : xpBoost < 0");
            dropBoost = reader.ReadVarUhShort();
            if (dropBoost < 0)
                throw new Exception("Forbidden value on dropBoost = " + dropBoost + ", it doesn't respect the following condition : dropBoost < 0");
            

}


}


}