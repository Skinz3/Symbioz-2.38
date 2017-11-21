


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class BasicLatencyStatsMessage : Message
{

public const ushort Id = 5663;
public override ushort MessageId
{
    get { return Id; }
}

public ushort latency;
        public ushort sampleCount;
        public ushort max;
        

public BasicLatencyStatsMessage()
{
}

public BasicLatencyStatsMessage(ushort latency, ushort sampleCount, ushort max)
        {
            this.latency = latency;
            this.sampleCount = sampleCount;
            this.max = max;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort(latency);
            writer.WriteVarUhShort(sampleCount);
            writer.WriteVarUhShort(max);
            

}

public override void Deserialize(ICustomDataInput reader)
{

latency = reader.ReadUShort();
            if (latency < 0 || latency > 65535)
                throw new Exception("Forbidden value on latency = " + latency + ", it doesn't respect the following condition : latency < 0 || latency > 65535");
            sampleCount = reader.ReadVarUhShort();
            if (sampleCount < 0)
                throw new Exception("Forbidden value on sampleCount = " + sampleCount + ", it doesn't respect the following condition : sampleCount < 0");
            max = reader.ReadVarUhShort();
            if (max < 0)
                throw new Exception("Forbidden value on max = " + max + ", it doesn't respect the following condition : max < 0");
            

}


}


}