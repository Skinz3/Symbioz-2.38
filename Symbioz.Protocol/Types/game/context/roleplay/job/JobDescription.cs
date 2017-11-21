


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class JobDescription
{

public const short Id = 101;
public virtual short TypeId
{
    get { return Id; }
}

public sbyte jobId;
        public Types.SkillActionDescription[] skills;
        

public JobDescription()
{
}

public JobDescription(sbyte jobId, Types.SkillActionDescription[] skills)
        {
            this.jobId = jobId;
            this.skills = skills;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(jobId);
            writer.WriteUShort((ushort)skills.Length);
            foreach (var entry in skills)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

jobId = reader.ReadSByte();
            if (jobId < 0)
                throw new Exception("Forbidden value on jobId = " + jobId + ", it doesn't respect the following condition : jobId < 0");
            var limit = reader.ReadUShort();
            skills = new Types.SkillActionDescription[limit];
            for (int i = 0; i < limit; i++)
            {
                 skills[i] = Types.ProtocolTypeManager.GetInstance<Types.SkillActionDescription>(reader.ReadShort());
                 skills[i].Deserialize(reader);
            }
            

}


}


}