


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameFightPlacementSwapPositionsOfferMessage : Message
{

public const ushort Id = 6542;
public override ushort MessageId
{
    get { return Id; }
}

public int requestId;
        public double requesterId;
        public ushort requesterCellId;
        public double requestedId;
        public ushort requestedCellId;
        

public GameFightPlacementSwapPositionsOfferMessage()
{
}

public GameFightPlacementSwapPositionsOfferMessage(int requestId, double requesterId, ushort requesterCellId, double requestedId, ushort requestedCellId)
        {
            this.requestId = requestId;
            this.requesterId = requesterId;
            this.requesterCellId = requesterCellId;
            this.requestedId = requestedId;
            this.requestedCellId = requestedCellId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(requestId);
            writer.WriteDouble(requesterId);
            writer.WriteVarUhShort(requesterCellId);
            writer.WriteDouble(requestedId);
            writer.WriteVarUhShort(requestedCellId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

requestId = reader.ReadInt();
            if (requestId < 0)
                throw new Exception("Forbidden value on requestId = " + requestId + ", it doesn't respect the following condition : requestId < 0");
            requesterId = reader.ReadDouble();
            if (requesterId < -9007199254740990 || requesterId > 9007199254740990)
                throw new Exception("Forbidden value on requesterId = " + requesterId + ", it doesn't respect the following condition : requesterId < -9007199254740990 || requesterId > 9007199254740990");
            requesterCellId = reader.ReadVarUhShort();
            if (requesterCellId < 0 || requesterCellId > 559)
                throw new Exception("Forbidden value on requesterCellId = " + requesterCellId + ", it doesn't respect the following condition : requesterCellId < 0 || requesterCellId > 559");
            requestedId = reader.ReadDouble();
            if (requestedId < -9007199254740990 || requestedId > 9007199254740990)
                throw new Exception("Forbidden value on requestedId = " + requestedId + ", it doesn't respect the following condition : requestedId < -9007199254740990 || requestedId > 9007199254740990");
            requestedCellId = reader.ReadVarUhShort();
            if (requestedCellId < 0 || requestedCellId > 559)
                throw new Exception("Forbidden value on requestedCellId = " + requestedCellId + ", it doesn't respect the following condition : requestedCellId < 0 || requestedCellId > 559");
            

}


}


}