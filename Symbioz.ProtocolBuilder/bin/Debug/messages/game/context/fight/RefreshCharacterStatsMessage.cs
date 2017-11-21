


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class RefreshCharacterStatsMessage : Message
{

public const ushort Id = 6699;
public override ushort MessageId
{
    get { return Id; }
}

public double fighterId;
        public Types.GameFightMinimalStats stats;
        

public RefreshCharacterStatsMessage()
{
}

public RefreshCharacterStatsMessage(double fighterId, Types.GameFightMinimalStats stats)
        {
            this.fighterId = fighterId;
            this.stats = stats;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(fighterId);
            stats.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

fighterId = reader.ReadDouble();
            if (fighterId < -9007199254740990 || fighterId > 9007199254740990)
                throw new Exception("Forbidden value on fighterId = " + fighterId + ", it doesn't respect the following condition : fighterId < -9007199254740990 || fighterId > 9007199254740990");
            stats = new Types.GameFightMinimalStats();
            stats.Deserialize(reader);
            

}


}


}