


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangePodsModifiedMessage : ExchangeObjectMessage
{

public const ushort Id = 6670;
public override ushort MessageId
{
    get { return Id; }
}

public uint currentWeight;
        public uint maxWeight;
        

public ExchangePodsModifiedMessage()
{
}

public ExchangePodsModifiedMessage(bool remote, uint currentWeight, uint maxWeight)
         : base(remote)
        {
            this.currentWeight = currentWeight;
            this.maxWeight = maxWeight;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(currentWeight);
            writer.WriteVarUhInt(maxWeight);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            currentWeight = reader.ReadVarUhInt();
            if (currentWeight < 0)
                throw new Exception("Forbidden value on currentWeight = " + currentWeight + ", it doesn't respect the following condition : currentWeight < 0");
            maxWeight = reader.ReadVarUhInt();
            if (maxWeight < 0)
                throw new Exception("Forbidden value on maxWeight = " + maxWeight + ", it doesn't respect the following condition : maxWeight < 0");
            

}


}


}