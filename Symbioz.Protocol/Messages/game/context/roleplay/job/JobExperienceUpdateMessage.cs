


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class JobExperienceUpdateMessage : Message
{

public const ushort Id = 5654;
public override ushort MessageId
{
    get { return Id; }
}

public Types.JobExperience experiencesUpdate;
        

public JobExperienceUpdateMessage()
{
}

public JobExperienceUpdateMessage(Types.JobExperience experiencesUpdate)
        {
            this.experiencesUpdate = experiencesUpdate;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

experiencesUpdate.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

experiencesUpdate = new Types.JobExperience();
            experiencesUpdate.Deserialize(reader);
            

}


}


}