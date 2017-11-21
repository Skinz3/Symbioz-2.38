


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameContextMoveMultipleElementsMessage : Message
{

public const ushort Id = 254;
public override ushort MessageId
{
    get { return Id; }
}

public Types.EntityMovementInformations[] movements;
        

public GameContextMoveMultipleElementsMessage()
{
}

public GameContextMoveMultipleElementsMessage(Types.EntityMovementInformations[] movements)
        {
            this.movements = movements;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)movements.Length);
            foreach (var entry in movements)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            movements = new Types.EntityMovementInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 movements[i] = new Types.EntityMovementInformations();
                 movements[i].Deserialize(reader);
            }
            

}


}


}