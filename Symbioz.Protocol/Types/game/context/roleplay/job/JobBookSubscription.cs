


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class JobBookSubscription
{

public const short Id = 500;
public virtual short TypeId
{
    get { return Id; }
}

public sbyte jobId;
        public bool subscribed;
        

public JobBookSubscription()
{
}

public JobBookSubscription(sbyte jobId, bool subscribed)
        {
            this.jobId = jobId;
            this.subscribed = subscribed;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(jobId);
            writer.WriteBoolean(subscribed);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

jobId = reader.ReadSByte();
            if (jobId < 0)
                throw new Exception("Forbidden value on jobId = " + jobId + ", it doesn't respect the following condition : jobId < 0");
            subscribed = reader.ReadBoolean();
            

}


}


}