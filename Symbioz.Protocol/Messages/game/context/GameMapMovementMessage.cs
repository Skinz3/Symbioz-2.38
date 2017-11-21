


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameMapMovementMessage : Message
{

public const ushort Id = 951;
public override ushort MessageId
{
    get { return Id; }
}

public short[] keyMovements;
        public double actorId;
        

public GameMapMovementMessage()
{
}

public GameMapMovementMessage(short[] keyMovements, double actorId)
        {
            this.keyMovements = keyMovements;
            this.actorId = actorId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)keyMovements.Length);
            foreach (var entry in keyMovements)
            {
                 writer.WriteShort(entry);
            }
            writer.WriteDouble(actorId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            keyMovements = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 keyMovements[i] = reader.ReadShort();
            }
            actorId = reader.ReadDouble();
            if (actorId < -9007199254740990 || actorId > 9007199254740990)
                throw new Exception("Forbidden value on actorId = " + actorId + ", it doesn't respect the following condition : actorId < -9007199254740990 || actorId > 9007199254740990");
            

}


}


}