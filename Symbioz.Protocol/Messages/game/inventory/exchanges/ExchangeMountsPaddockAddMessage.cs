


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeMountsPaddockAddMessage : Message
{

public const ushort Id = 6561;
public override ushort MessageId
{
    get { return Id; }
}

public Types.MountClientData[] mountDescription;
        

public ExchangeMountsPaddockAddMessage()
{
}

public ExchangeMountsPaddockAddMessage(Types.MountClientData[] mountDescription)
        {
            this.mountDescription = mountDescription;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)mountDescription.Length);
            foreach (var entry in mountDescription)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            mountDescription = new Types.MountClientData[limit];
            for (int i = 0; i < limit; i++)
            {
                 mountDescription[i] = new Types.MountClientData();
                 mountDescription[i].Deserialize(reader);
            }
            

}


}


}