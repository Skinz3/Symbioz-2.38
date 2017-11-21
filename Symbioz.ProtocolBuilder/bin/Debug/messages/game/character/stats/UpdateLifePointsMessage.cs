


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class UpdateLifePointsMessage : Message
{

public const ushort Id = 5658;
public override ushort MessageId
{
    get { return Id; }
}

public uint lifePoints;
        public uint maxLifePoints;
        

public UpdateLifePointsMessage()
{
}

public UpdateLifePointsMessage(uint lifePoints, uint maxLifePoints)
        {
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(lifePoints);
            writer.WriteVarUhInt(maxLifePoints);
            

}

public override void Deserialize(ICustomDataInput reader)
{

lifePoints = reader.ReadVarUhInt();
            if (lifePoints < 0)
                throw new Exception("Forbidden value on lifePoints = " + lifePoints + ", it doesn't respect the following condition : lifePoints < 0");
            maxLifePoints = reader.ReadVarUhInt();
            if (maxLifePoints < 0)
                throw new Exception("Forbidden value on maxLifePoints = " + maxLifePoints + ", it doesn't respect the following condition : maxLifePoints < 0");
            

}


}


}