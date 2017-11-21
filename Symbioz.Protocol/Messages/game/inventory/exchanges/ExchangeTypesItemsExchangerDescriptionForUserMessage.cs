


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeTypesItemsExchangerDescriptionForUserMessage : Message
{

public const ushort Id = 5752;
public override ushort MessageId
{
    get { return Id; }
}

public Types.BidExchangerObjectInfo[] itemTypeDescriptions;
        

public ExchangeTypesItemsExchangerDescriptionForUserMessage()
{
}

public ExchangeTypesItemsExchangerDescriptionForUserMessage(Types.BidExchangerObjectInfo[] itemTypeDescriptions)
        {
            this.itemTypeDescriptions = itemTypeDescriptions;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)itemTypeDescriptions.Length);
            foreach (var entry in itemTypeDescriptions)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            itemTypeDescriptions = new Types.BidExchangerObjectInfo[limit];
            for (int i = 0; i < limit; i++)
            {
                 itemTypeDescriptions[i] = new Types.BidExchangerObjectInfo();
                 itemTypeDescriptions[i].Deserialize(reader);
            }
            

}


}


}