


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameContextReadyMessage : Message
{

public const ushort Id = 6071;
public override ushort MessageId
{
    get { return Id; }
}

public int mapId;
        

public GameContextReadyMessage()
{
}

public GameContextReadyMessage(int mapId)
        {
            this.mapId = mapId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(mapId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

mapId = reader.ReadInt();
            if (mapId < 0)
                throw new Exception("Forbidden value on mapId = " + mapId + ", it doesn't respect the following condition : mapId < 0");
            

}


}


}