


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeStartOkHumanVendorMessage : Message
{

public const ushort Id = 5767;
public override ushort MessageId
{
    get { return Id; }
}

public double sellerId;
        public Types.ObjectItemToSellInHumanVendorShop[] objectsInfos;
        

public ExchangeStartOkHumanVendorMessage()
{
}

public ExchangeStartOkHumanVendorMessage(double sellerId, Types.ObjectItemToSellInHumanVendorShop[] objectsInfos)
        {
            this.sellerId = sellerId;
            this.objectsInfos = objectsInfos;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(sellerId);
            writer.WriteUShort((ushort)objectsInfos.Length);
            foreach (var entry in objectsInfos)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

sellerId = reader.ReadDouble();
            if (sellerId < -9007199254740990 || sellerId > 9007199254740990)
                throw new Exception("Forbidden value on sellerId = " + sellerId + ", it doesn't respect the following condition : sellerId < -9007199254740990 || sellerId > 9007199254740990");
            var limit = reader.ReadUShort();
            objectsInfos = new Types.ObjectItemToSellInHumanVendorShop[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectsInfos[i] = new Types.ObjectItemToSellInHumanVendorShop();
                 objectsInfos[i].Deserialize(reader);
            }
            

}


}


}