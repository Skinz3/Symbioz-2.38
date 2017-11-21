


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class StorageKamasUpdateMessage : Message
{

public const ushort Id = 5645;
public override ushort MessageId
{
    get { return Id; }
}

public int kamasTotal;
        

public StorageKamasUpdateMessage()
{
}

public StorageKamasUpdateMessage(int kamasTotal)
        {
            this.kamasTotal = kamasTotal;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(kamasTotal);
            

}

public override void Deserialize(ICustomDataInput reader)
{

kamasTotal = reader.ReadInt();
            

}


}


}