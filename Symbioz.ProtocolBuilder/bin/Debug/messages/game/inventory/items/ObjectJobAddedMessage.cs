


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ObjectJobAddedMessage : Message
{

public const ushort Id = 6014;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte jobId;
        

public ObjectJobAddedMessage()
{
}

public ObjectJobAddedMessage(sbyte jobId)
        {
            this.jobId = jobId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(jobId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

jobId = reader.ReadSByte();
            if (jobId < 0)
                throw new Exception("Forbidden value on jobId = " + jobId + ", it doesn't respect the following condition : jobId < 0");
            

}


}


}