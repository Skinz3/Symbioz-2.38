


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class StorageObjectsUpdateMessage : Message
{

public const ushort Id = 6036;
public override ushort MessageId
{
    get { return Id; }
}

public Types.ObjectItem[] objectList;
        

public StorageObjectsUpdateMessage()
{
}

public StorageObjectsUpdateMessage(Types.ObjectItem[] objectList)
        {
            this.objectList = objectList;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)objectList.Length);
            foreach (var entry in objectList)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            objectList = new Types.ObjectItem[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectList[i] = new Types.ObjectItem();
                 objectList[i].Deserialize(reader);
            }
            

}


}


}