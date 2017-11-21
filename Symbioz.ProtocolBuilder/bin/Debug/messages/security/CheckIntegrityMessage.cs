


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CheckIntegrityMessage : Message
{

public const ushort Id = 6372;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte[] data;
        

public CheckIntegrityMessage()
{
}

public CheckIntegrityMessage(sbyte[] data)
        {
            this.data = data;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)data.Length);
            foreach (var entry in data)
            {
                 writer.WriteSByte(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            data = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 data[i] = reader.ReadSByte();
            }
            

}


}


}