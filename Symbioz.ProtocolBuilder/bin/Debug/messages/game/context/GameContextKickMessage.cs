


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameContextKickMessage : Message
{

public const ushort Id = 6081;
public override ushort MessageId
{
    get { return Id; }
}

public double targetId;
        

public GameContextKickMessage()
{
}

public GameContextKickMessage(double targetId)
        {
            this.targetId = targetId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(targetId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

targetId = reader.ReadDouble();
            if (targetId < -9007199254740990 || targetId > 9007199254740990)
                throw new Exception("Forbidden value on targetId = " + targetId + ", it doesn't respect the following condition : targetId < -9007199254740990 || targetId > 9007199254740990");
            

}


}


}