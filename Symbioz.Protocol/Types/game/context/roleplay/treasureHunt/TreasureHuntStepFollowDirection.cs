


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class TreasureHuntStepFollowDirection : TreasureHuntStep
{

public const short Id = 468;
public override short TypeId
{
    get { return Id; }
}

public sbyte direction;
        public ushort mapCount;
        

public TreasureHuntStepFollowDirection()
{
}

public TreasureHuntStepFollowDirection(sbyte direction, ushort mapCount)
        {
            this.direction = direction;
            this.mapCount = mapCount;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(direction);
            writer.WriteVarUhShort(mapCount);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            direction = reader.ReadSByte();
            if (direction < 0)
                throw new Exception("Forbidden value on direction = " + direction + ", it doesn't respect the following condition : direction < 0");
            mapCount = reader.ReadVarUhShort();
            if (mapCount < 0)
                throw new Exception("Forbidden value on mapCount = " + mapCount + ", it doesn't respect the following condition : mapCount < 0");
            

}


}


}