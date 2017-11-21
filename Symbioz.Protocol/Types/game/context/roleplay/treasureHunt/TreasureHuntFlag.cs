


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class TreasureHuntFlag
{

public const short Id = 473;
public virtual short TypeId
{
    get { return Id; }
}

public int mapId;
        public sbyte state;
        

public TreasureHuntFlag()
{
}

public TreasureHuntFlag(int mapId, sbyte state)
        {
            this.mapId = mapId;
            this.state = state;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(mapId);
            writer.WriteSByte(state);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

mapId = reader.ReadInt();
            state = reader.ReadSByte();
            if (state < 0)
                throw new Exception("Forbidden value on state = " + state + ", it doesn't respect the following condition : state < 0");
            

}


}


}