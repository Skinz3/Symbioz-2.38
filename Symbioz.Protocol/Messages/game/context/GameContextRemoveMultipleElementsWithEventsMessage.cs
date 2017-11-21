


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameContextRemoveMultipleElementsWithEventsMessage : GameContextRemoveMultipleElementsMessage
{

public const ushort Id = 6416;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte[] elementEventIds;
        

public GameContextRemoveMultipleElementsWithEventsMessage()
{
}

public GameContextRemoveMultipleElementsWithEventsMessage(double[] id, sbyte[] elementEventIds)
         : base(id)
        {
            this.elementEventIds = elementEventIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)elementEventIds.Length);
            foreach (var entry in elementEventIds)
            {
                 writer.WriteSByte(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            elementEventIds = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 elementEventIds[i] = reader.ReadSByte();
            }
            

}


}


}