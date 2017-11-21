


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeOfflineSoldItemsMessage : Message
{

public const ushort Id = 6613;
public override ushort MessageId
{
    get { return Id; }
}

public Types.ObjectItemGenericQuantityPrice[] bidHouseItems;
        public Types.ObjectItemGenericQuantityPrice[] merchantItems;
        

public ExchangeOfflineSoldItemsMessage()
{
}

public ExchangeOfflineSoldItemsMessage(Types.ObjectItemGenericQuantityPrice[] bidHouseItems, Types.ObjectItemGenericQuantityPrice[] merchantItems)
        {
            this.bidHouseItems = bidHouseItems;
            this.merchantItems = merchantItems;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)bidHouseItems.Length);
            foreach (var entry in bidHouseItems)
            {
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)merchantItems.Length);
            foreach (var entry in merchantItems)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            bidHouseItems = new Types.ObjectItemGenericQuantityPrice[limit];
            for (int i = 0; i < limit; i++)
            {
                 bidHouseItems[i] = new Types.ObjectItemGenericQuantityPrice();
                 bidHouseItems[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            merchantItems = new Types.ObjectItemGenericQuantityPrice[limit];
            for (int i = 0; i < limit; i++)
            {
                 merchantItems[i] = new Types.ObjectItemGenericQuantityPrice();
                 merchantItems[i].Deserialize(reader);
            }
            

}


}


}