


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeBidHouseInListRemovedMessage : Message
{

public const ushort Id = 5950;
public override ushort MessageId
{
    get { return Id; }
}

public int itemUID;
        

public ExchangeBidHouseInListRemovedMessage()
{
}

public ExchangeBidHouseInListRemovedMessage(int itemUID)
        {
            this.itemUID = itemUID;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(itemUID);
            

}

public override void Deserialize(ICustomDataInput reader)
{

itemUID = reader.ReadInt();
            

}


}


}