


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartsListMessage : Message
{

public const ushort Id = 1502;
public override ushort MessageId
{
    get { return Id; }
}

public Types.ContentPart[] parts;
        

public PartsListMessage()
{
}

public PartsListMessage(Types.ContentPart[] parts)
        {
            this.parts = parts;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)parts.Length);
            foreach (var entry in parts)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            parts = new Types.ContentPart[limit];
            for (int i = 0; i < limit; i++)
            {
                 parts[i] = new Types.ContentPart();
                 parts[i].Deserialize(reader);
            }
            

}


}


}