


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class TreasureHuntStepFollowDirectionToHint : TreasureHuntStep
{

public const short Id = 472;
public override short TypeId
{
    get { return Id; }
}

public sbyte direction;
        public ushort npcId;
        

public TreasureHuntStepFollowDirectionToHint()
{
}

public TreasureHuntStepFollowDirectionToHint(sbyte direction, ushort npcId)
        {
            this.direction = direction;
            this.npcId = npcId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(direction);
            writer.WriteVarUhShort(npcId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            direction = reader.ReadSByte();
            if (direction < 0)
                throw new Exception("Forbidden value on direction = " + direction + ", it doesn't respect the following condition : direction < 0");
            npcId = reader.ReadVarUhShort();
            if (npcId < 0)
                throw new Exception("Forbidden value on npcId = " + npcId + ", it doesn't respect the following condition : npcId < 0");
            

}


}


}