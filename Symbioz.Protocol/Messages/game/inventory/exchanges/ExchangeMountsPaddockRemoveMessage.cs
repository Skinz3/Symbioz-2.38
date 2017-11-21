


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeMountsPaddockRemoveMessage : Message
{

public const ushort Id = 6559;
public override ushort MessageId
{
    get { return Id; }
}

public int[] mountsId;
        

public ExchangeMountsPaddockRemoveMessage()
{
}

public ExchangeMountsPaddockRemoveMessage(int[] mountsId)
        {
            this.mountsId = mountsId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)mountsId.Length);
            foreach (var entry in mountsId)
            {
                 writer.WriteVarInt(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            mountsId = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 mountsId[i] = reader.ReadVarInt();
            }
            

}


}


}