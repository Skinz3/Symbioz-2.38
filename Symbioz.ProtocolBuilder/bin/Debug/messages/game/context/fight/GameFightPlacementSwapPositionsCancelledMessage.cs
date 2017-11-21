


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameFightPlacementSwapPositionsCancelledMessage : Message
{

public const ushort Id = 6546;
public override ushort MessageId
{
    get { return Id; }
}

public int requestId;
        public double cancellerId;
        

public GameFightPlacementSwapPositionsCancelledMessage()
{
}

public GameFightPlacementSwapPositionsCancelledMessage(int requestId, double cancellerId)
        {
            this.requestId = requestId;
            this.cancellerId = cancellerId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(requestId);
            writer.WriteDouble(cancellerId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

requestId = reader.ReadInt();
            if (requestId < 0)
                throw new Exception("Forbidden value on requestId = " + requestId + ", it doesn't respect the following condition : requestId < 0");
            cancellerId = reader.ReadDouble();
            if (cancellerId < -9007199254740990 || cancellerId > 9007199254740990)
                throw new Exception("Forbidden value on cancellerId = " + cancellerId + ", it doesn't respect the following condition : cancellerId < -9007199254740990 || cancellerId > 9007199254740990");
            

}


}


}