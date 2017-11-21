


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeShopStockMultiMovementUpdatedMessage : Message
{

public const ushort Id = 6038;
public override ushort MessageId
{
    get { return Id; }
}

public Types.ObjectItemToSell[] objectInfoList;
        

public ExchangeShopStockMultiMovementUpdatedMessage()
{
}

public ExchangeShopStockMultiMovementUpdatedMessage(Types.ObjectItemToSell[] objectInfoList)
        {
            this.objectInfoList = objectInfoList;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)objectInfoList.Length);
            foreach (var entry in objectInfoList)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            objectInfoList = new Types.ObjectItemToSell[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectInfoList[i] = new Types.ObjectItemToSell();
                 objectInfoList[i].Deserialize(reader);
            }
            

}


}


}