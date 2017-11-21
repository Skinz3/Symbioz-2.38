


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameFightJoinRequestMessage : Message
{

public const ushort Id = 701;
public override ushort MessageId
{
    get { return Id; }
}

public double fighterId;
        public int fightId;
        

public GameFightJoinRequestMessage()
{
}

public GameFightJoinRequestMessage(double fighterId, int fightId)
        {
            this.fighterId = fighterId;
            this.fightId = fightId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(fighterId);
            writer.WriteInt(fightId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

fighterId = reader.ReadDouble();
            if (fighterId < -9007199254740990 || fighterId > 9007199254740990)
                throw new Exception("Forbidden value on fighterId = " + fighterId + ", it doesn't respect the following condition : fighterId < -9007199254740990 || fighterId > 9007199254740990");
            fightId = reader.ReadInt();
            

}


}


}