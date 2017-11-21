


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class JobDescriptionMessage : Message
{

public const ushort Id = 5655;
public override ushort MessageId
{
    get { return Id; }
}

public Types.JobDescription[] jobsDescription;
        

public JobDescriptionMessage()
{
}

public JobDescriptionMessage(Types.JobDescription[] jobsDescription)
        {
            this.jobsDescription = jobsDescription;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)jobsDescription.Length);
            foreach (var entry in jobsDescription)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            jobsDescription = new Types.JobDescription[limit];
            for (int i = 0; i < limit; i++)
            {
                 jobsDescription[i] = new Types.JobDescription();
                 jobsDescription[i].Deserialize(reader);
            }
            

}


}


}