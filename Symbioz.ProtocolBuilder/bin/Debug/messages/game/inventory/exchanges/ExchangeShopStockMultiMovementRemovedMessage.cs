


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeShopStockMultiMovementRemovedMessage : Message
{

public const ushort Id = 6037;
public override ushort MessageId
{
    get { return Id; }
}

public uint[] objectIdList;
        

public ExchangeShopStockMultiMovementRemovedMessage()
{
}

public ExchangeShopStockMultiMovementRemovedMessage(uint[] objectIdList)
        {
            this.objectIdList = objectIdList;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)objectIdList.Length);
            foreach (var entry in objectIdList)
            {
                 writer.WriteVarUhInt(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            objectIdList = new uint[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectIdList[i] = reader.ReadVarUhInt();
            }
            

}


}


}