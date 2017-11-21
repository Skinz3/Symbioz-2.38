


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ServerExperienceModificatorMessage : Message
{

public const ushort Id = 6237;
public override ushort MessageId
{
    get { return Id; }
}

public ushort experiencePercent;
        

public ServerExperienceModificatorMessage()
{
}

public ServerExperienceModificatorMessage(ushort experiencePercent)
        {
            this.experiencePercent = experiencePercent;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(experiencePercent);
            

}

public override void Deserialize(ICustomDataInput reader)
{

experiencePercent = reader.ReadVarUhShort();
            if (experiencePercent < 0)
                throw new Exception("Forbidden value on experiencePercent = " + experiencePercent + ", it doesn't respect the following condition : experiencePercent < 0");
            

}


}


}