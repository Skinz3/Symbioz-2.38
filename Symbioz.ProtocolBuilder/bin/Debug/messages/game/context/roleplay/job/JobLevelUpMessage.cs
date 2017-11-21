


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class JobLevelUpMessage : Message
{

public const ushort Id = 5656;
public override ushort MessageId
{
    get { return Id; }
}

public byte newLevel;
        public Types.JobDescription jobsDescription;
        

public JobLevelUpMessage()
{
}

public JobLevelUpMessage(byte newLevel, Types.JobDescription jobsDescription)
        {
            this.newLevel = newLevel;
            this.jobsDescription = jobsDescription;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteByte(newLevel);
            jobsDescription.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

newLevel = reader.ReadByte();
            if (newLevel < 0 || newLevel > 255)
                throw new Exception("Forbidden value on newLevel = " + newLevel + ", it doesn't respect the following condition : newLevel < 0 || newLevel > 255");
            jobsDescription = new Types.JobDescription();
            jobsDescription.Deserialize(reader);
            

}


}


}