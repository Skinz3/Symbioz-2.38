


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CurrentMapMessage : Message
{

public const ushort Id = 220;
public override ushort MessageId
{
    get { return Id; }
}

public int mapId;
        public string mapKey;
        

public CurrentMapMessage()
{
}

public CurrentMapMessage(int mapId, string mapKey)
        {
            this.mapId = mapId;
            this.mapKey = mapKey;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(mapId);
            writer.WriteUTF(mapKey);
            

}

public override void Deserialize(ICustomDataInput reader)
{

mapId = reader.ReadInt();
            if (mapId < 0)
                throw new Exception("Forbidden value on mapId = " + mapId + ", it doesn't respect the following condition : mapId < 0");
            mapKey = reader.ReadUTF();
            

}


}


}