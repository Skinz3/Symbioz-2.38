


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class BasicStatMessage : Message
{

public const ushort Id = 6530;
public override ushort MessageId
{
    get { return Id; }
}

public double timeSpent;
        public ushort statId;
        

public BasicStatMessage()
{
}

public BasicStatMessage(double timeSpent, ushort statId)
        {
            this.timeSpent = timeSpent;
            this.statId = statId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(timeSpent);
            writer.WriteVarUhShort(statId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

timeSpent = reader.ReadDouble();
            if (timeSpent < 0 || timeSpent > 9007199254740990)
                throw new Exception("Forbidden value on timeSpent = " + timeSpent + ", it doesn't respect the following condition : timeSpent < 0 || timeSpent > 9007199254740990");
            statId = reader.ReadVarUhShort();
            if (statId < 0)
                throw new Exception("Forbidden value on statId = " + statId + ", it doesn't respect the following condition : statId < 0");
            

}


}


}