


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyCompanionUpdateLightMessage : PartyUpdateLightMessage
{

public const ushort Id = 6472;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte indexId;
        

public PartyCompanionUpdateLightMessage()
{
}

public PartyCompanionUpdateLightMessage(uint partyId, ulong id, uint lifePoints, uint maxLifePoints, ushort prospecting, byte regenRate, sbyte indexId)
         : base(partyId, id, lifePoints, maxLifePoints, prospecting, regenRate)
        {
            this.indexId = indexId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(indexId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            indexId = reader.ReadSByte();
            if (indexId < 0)
                throw new Exception("Forbidden value on indexId = " + indexId + ", it doesn't respect the following condition : indexId < 0");
            

}


}


}