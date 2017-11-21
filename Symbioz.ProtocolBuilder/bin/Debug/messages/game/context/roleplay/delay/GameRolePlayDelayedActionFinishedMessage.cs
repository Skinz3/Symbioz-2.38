


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayDelayedActionFinishedMessage : Message
{

public const ushort Id = 6150;
public override ushort MessageId
{
    get { return Id; }
}

public double delayedCharacterId;
        public sbyte delayTypeId;
        

public GameRolePlayDelayedActionFinishedMessage()
{
}

public GameRolePlayDelayedActionFinishedMessage(double delayedCharacterId, sbyte delayTypeId)
        {
            this.delayedCharacterId = delayedCharacterId;
            this.delayTypeId = delayTypeId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(delayedCharacterId);
            writer.WriteSByte(delayTypeId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

delayedCharacterId = reader.ReadDouble();
            if (delayedCharacterId < -9007199254740990 || delayedCharacterId > 9007199254740990)
                throw new Exception("Forbidden value on delayedCharacterId = " + delayedCharacterId + ", it doesn't respect the following condition : delayedCharacterId < -9007199254740990 || delayedCharacterId > 9007199254740990");
            delayTypeId = reader.ReadSByte();
            if (delayTypeId < 0)
                throw new Exception("Forbidden value on delayTypeId = " + delayTypeId + ", it doesn't respect the following condition : delayTypeId < 0");
            

}


}


}