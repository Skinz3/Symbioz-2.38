


















// Generated on 04/27/2016 01:13:08
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class AchievementRewardable
{

public const short Id = 412;
public virtual short TypeId
{
    get { return Id; }
}

public ushort id;
        public byte finishedlevel;
        

public AchievementRewardable()
{
}

public AchievementRewardable(ushort id, byte finishedlevel)
        {
            this.id = id;
            this.finishedlevel = finishedlevel;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(id);
            writer.WriteByte(finishedlevel);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

id = reader.ReadVarUhShort();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
            finishedlevel = reader.ReadByte();
            if (finishedlevel < 0 || finishedlevel > 200)
                throw new Exception("Forbidden value on finishedlevel = " + finishedlevel + ", it doesn't respect the following condition : finishedlevel < 0 || finishedlevel > 200");
            

}


}


}