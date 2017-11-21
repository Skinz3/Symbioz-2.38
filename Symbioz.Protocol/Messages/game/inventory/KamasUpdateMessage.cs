


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class KamasUpdateMessage : Message
{

public const ushort Id = 5537;
public override ushort MessageId
{
    get { return Id; }
}

public int kamasTotal;
        

public KamasUpdateMessage()
{
}

public KamasUpdateMessage(int kamasTotal)
        {
            this.kamasTotal = kamasTotal;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarInt(kamasTotal);
            

}

public override void Deserialize(ICustomDataInput reader)
{

kamasTotal = reader.ReadVarInt();
            

}


}


}