


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameFightTurnReadyMessage : Message
{

public const ushort Id = 716;
public override ushort MessageId
{
    get { return Id; }
}

public bool isReady;
        

public GameFightTurnReadyMessage()
{
}

public GameFightTurnReadyMessage(bool isReady)
        {
            this.isReady = isReady;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(isReady);
            

}

public override void Deserialize(ICustomDataInput reader)
{

isReady = reader.ReadBoolean();
            

}


}


}