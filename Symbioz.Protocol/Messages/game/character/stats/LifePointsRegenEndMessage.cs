


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class LifePointsRegenEndMessage : UpdateLifePointsMessage
{

public const ushort Id = 5686;
public override ushort MessageId
{
    get { return Id; }
}

public uint lifePointsGained;
        

public LifePointsRegenEndMessage()
{
}

public LifePointsRegenEndMessage(uint lifePoints, uint maxLifePoints, uint lifePointsGained)
         : base(lifePoints, maxLifePoints)
        {
            this.lifePointsGained = lifePointsGained;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(lifePointsGained);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            lifePointsGained = reader.ReadVarUhInt();
            if (lifePointsGained < 0)
                throw new Exception("Forbidden value on lifePointsGained = " + lifePointsGained + ", it doesn't respect the following condition : lifePointsGained < 0");
            

}


}


}