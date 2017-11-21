


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeStartOkNpcShopMessage : Message
{

public const ushort Id = 5761;
public override ushort MessageId
{
    get { return Id; }
}

public double npcSellerId;
        public ushort tokenId;
        public Types.ObjectItemToSellInNpcShop[] objectsInfos;
        

public ExchangeStartOkNpcShopMessage()
{
}

public ExchangeStartOkNpcShopMessage(double npcSellerId, ushort tokenId, Types.ObjectItemToSellInNpcShop[] objectsInfos)
        {
            this.npcSellerId = npcSellerId;
            this.tokenId = tokenId;
            this.objectsInfos = objectsInfos;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(npcSellerId);
            writer.WriteVarUhShort(tokenId);
            writer.WriteUShort((ushort)objectsInfos.Length);
            foreach (var entry in objectsInfos)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

npcSellerId = reader.ReadDouble();
            if (npcSellerId < -9007199254740990 || npcSellerId > 9007199254740990)
                throw new Exception("Forbidden value on npcSellerId = " + npcSellerId + ", it doesn't respect the following condition : npcSellerId < -9007199254740990 || npcSellerId > 9007199254740990");
            tokenId = reader.ReadVarUhShort();
            if (tokenId < 0)
                throw new Exception("Forbidden value on tokenId = " + tokenId + ", it doesn't respect the following condition : tokenId < 0");
            var limit = reader.ReadUShort();
            objectsInfos = new Types.ObjectItemToSellInNpcShop[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectsInfos[i] = new Types.ObjectItemToSellInNpcShop();
                 objectsInfos[i].Deserialize(reader);
            }
            

}


}


}