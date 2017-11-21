


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayDelayedActionMessage : Message
{

public const ushort Id = 6153;
public override ushort MessageId
{
    get { return Id; }
}

public double delayedCharacterId;
        public sbyte delayTypeId;
        public double delayEndTime;
        

public GameRolePlayDelayedActionMessage()
{
}

public GameRolePlayDelayedActionMessage(double delayedCharacterId, sbyte delayTypeId, double delayEndTime)
        {
            this.delayedCharacterId = delayedCharacterId;
            this.delayTypeId = delayTypeId;
            this.delayEndTime = delayEndTime;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(delayedCharacterId);
            writer.WriteSByte(delayTypeId);
            writer.WriteDouble(delayEndTime);
            

}

public override void Deserialize(ICustomDataInput reader)
{

delayedCharacterId = reader.ReadDouble();
            if (delayedCharacterId < -9007199254740990 || delayedCharacterId > 9007199254740990)
                throw new Exception("Forbidden value on delayedCharacterId = " + delayedCharacterId + ", it doesn't respect the following condition : delayedCharacterId < -9007199254740990 || delayedCharacterId > 9007199254740990");
            delayTypeId = reader.ReadSByte();
            if (delayTypeId < 0)
                throw new Exception("Forbidden value on delayTypeId = " + delayTypeId + ", it doesn't respect the following condition : delayTypeId < 0");
            delayEndTime = reader.ReadDouble();
            if (delayEndTime < 0 || delayEndTime > 9007199254740990)
                throw new Exception("Forbidden value on delayEndTime = " + delayEndTime + ", it doesn't respect the following condition : delayEndTime < 0 || delayEndTime > 9007199254740990");
            

}


}


}