


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeShopStockStartedMessage : Message
{

public const ushort Id = 5910;
public override ushort MessageId
{
    get { return Id; }
}

public Types.ObjectItemToSell[] objectsInfos;
        

public ExchangeShopStockStartedMessage()
{
}

public ExchangeShopStockStartedMessage(Types.ObjectItemToSell[] objectsInfos)
        {
            this.objectsInfos = objectsInfos;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)objectsInfos.Length);
            foreach (var entry in objectsInfos)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            objectsInfos = new Types.ObjectItemToSell[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectsInfos[i] = new Types.ObjectItemToSell();
                 objectsInfos[i].Deserialize(reader);
            }
            

}


}


}