


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameFightTurnFinishMessage : Message
{

public const ushort Id = 718;
public override ushort MessageId
{
    get { return Id; }
}

public bool isAfk;
        

public GameFightTurnFinishMessage()
{
}

public GameFightTurnFinishMessage(bool isAfk)
        {
            this.isAfk = isAfk;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(isAfk);
            

}

public override void Deserialize(ICustomDataInput reader)
{

isAfk = reader.ReadBoolean();
            

}


}


}