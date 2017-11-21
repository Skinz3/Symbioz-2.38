


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AbstractGameActionMessage : Message
{

public const ushort Id = 1000;
public override ushort MessageId
{
    get { return Id; }
}

public ushort actionId;
        public double sourceId;
        

public AbstractGameActionMessage()
{
}

public AbstractGameActionMessage(ushort actionId, double sourceId)
        {
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(actionId);
            writer.WriteDouble(sourceId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

actionId = reader.ReadVarUhShort();
            if (actionId < 0)
                throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
            sourceId = reader.ReadDouble();
            if (sourceId < -9007199254740990 || sourceId > 9007199254740990)
                throw new Exception("Forbidden value on sourceId = " + sourceId + ", it doesn't respect the following condition : sourceId < -9007199254740990 || sourceId > 9007199254740990");
            

}


}


}