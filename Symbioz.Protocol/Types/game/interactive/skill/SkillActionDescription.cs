


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class SkillActionDescription
{

public const short Id = 102;
public virtual short TypeId
{
    get { return Id; }
}

public ushort skillId;
        

public SkillActionDescription()
{
}

public SkillActionDescription(ushort skillId)
        {
            this.skillId = skillId;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(skillId);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

skillId = reader.ReadVarUhShort();
            if (skillId < 0)
                throw new Exception("Forbidden value on skillId = " + skillId + ", it doesn't respect the following condition : skillId < 0");
            

}


}


}