


















// Generated on 04/27/2016 01:13:17
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class Idol
{

public const short Id = 489;
public virtual short TypeId
{
    get { return Id; }
}

public ushort id;
        public ushort xpBonusPercent;
        public ushort dropBonusPercent;
        

public Idol()
{
}

public Idol(ushort id, ushort xpBonusPercent, ushort dropBonusPercent)
        {
            this.id = id;
            this.xpBonusPercent = xpBonusPercent;
            this.dropBonusPercent = dropBonusPercent;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(id);
            writer.WriteVarUhShort(xpBonusPercent);
            writer.WriteVarUhShort(dropBonusPercent);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

id = reader.ReadVarUhShort();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
            xpBonusPercent = reader.ReadVarUhShort();
            if (xpBonusPercent < 0)
                throw new Exception("Forbidden value on xpBonusPercent = " + xpBonusPercent + ", it doesn't respect the following condition : xpBonusPercent < 0");
            dropBonusPercent = reader.ReadVarUhShort();
            if (dropBonusPercent < 0)
                throw new Exception("Forbidden value on dropBonusPercent = " + dropBonusPercent + ", it doesn't respect the following condition : dropBonusPercent < 0");
            

}


}


}