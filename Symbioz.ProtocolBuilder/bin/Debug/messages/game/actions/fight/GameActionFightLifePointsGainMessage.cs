


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightLifePointsGainMessage : AbstractGameActionMessage
{

public const ushort Id = 6311;
public override ushort MessageId
{
    get { return Id; }
}

public double targetId;
        public uint delta;
        

public GameActionFightLifePointsGainMessage()
{
}

public GameActionFightLifePointsGainMessage(ushort actionId, double sourceId, double targetId, uint delta)
         : base(actionId, sourceId)
        {
            this.targetId = targetId;
            this.delta = delta;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteDouble(targetId);
            writer.WriteVarUhInt(delta);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            targetId = reader.ReadDouble();
            if (targetId < -9007199254740990 || targetId > 9007199254740990)
                throw new Exception("Forbidden value on targetId = " + targetId + ", it doesn't respect the following condition : targetId < -9007199254740990 || targetId > 9007199254740990");
            delta = reader.ReadVarUhInt();
            if (delta < 0)
                throw new Exception("Forbidden value on delta = " + delta + ", it doesn't respect the following condition : delta < 0");
            

}


}


}