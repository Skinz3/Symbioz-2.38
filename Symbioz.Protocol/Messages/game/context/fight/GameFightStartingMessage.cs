


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameFightStartingMessage : Message
{

public const ushort Id = 700;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte fightType;
        public double attackerId;
        public double defenderId;
        

public GameFightStartingMessage()
{
}

public GameFightStartingMessage(sbyte fightType, double attackerId, double defenderId)
        {
            this.fightType = fightType;
            this.attackerId = attackerId;
            this.defenderId = defenderId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(fightType);
            writer.WriteDouble(attackerId);
            writer.WriteDouble(defenderId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

fightType = reader.ReadSByte();
            if (fightType < 0)
                throw new Exception("Forbidden value on fightType = " + fightType + ", it doesn't respect the following condition : fightType < 0");
            attackerId = reader.ReadDouble();
            if (attackerId < -9007199254740990 || attackerId > 9007199254740990)
                throw new Exception("Forbidden value on attackerId = " + attackerId + ", it doesn't respect the following condition : attackerId < -9007199254740990 || attackerId > 9007199254740990");
            defenderId = reader.ReadDouble();
            if (defenderId < -9007199254740990 || defenderId > 9007199254740990)
                throw new Exception("Forbidden value on defenderId = " + defenderId + ", it doesn't respect the following condition : defenderId < -9007199254740990 || defenderId > 9007199254740990");
            

}


}


}