


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameCautiousMapMovementRequestMessage : GameMapMovementRequestMessage
{

public const ushort Id = 6496;
public override ushort MessageId
{
    get { return Id; }
}



public GameCautiousMapMovementRequestMessage()
{
}

public GameCautiousMapMovementRequestMessage(short[] keyMovements, int mapId)
         : base(keyMovements, mapId)
        {
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            

}


}


}