


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class EmoteListMessage : Message
{

public const ushort Id = 5689;
public override ushort MessageId
{
    get { return Id; }
}

public byte[] emoteIds;
        

public EmoteListMessage()
{
}

public EmoteListMessage(byte[] emoteIds)
        {
            this.emoteIds = emoteIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)emoteIds.Length);
            foreach (var entry in emoteIds)
            {
                 writer.WriteByte(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            emoteIds = new byte[limit];
            for (int i = 0; i < limit; i++)
            {
                 emoteIds[i] = reader.ReadByte();
            }
            

}


}


}