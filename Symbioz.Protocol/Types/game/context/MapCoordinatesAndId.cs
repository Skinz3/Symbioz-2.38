


















// Generated on 04/27/2016 01:13:11
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class MapCoordinatesAndId : MapCoordinates
{

public const short Id = 392;
public override short TypeId
{
    get { return Id; }
}

public int mapId;
        

public MapCoordinatesAndId()
{
}

public MapCoordinatesAndId(short worldX, short worldY, int mapId)
         : base(worldX, worldY)
        {
            this.mapId = mapId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteInt(mapId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            mapId = reader.ReadInt();
            

}


}


}