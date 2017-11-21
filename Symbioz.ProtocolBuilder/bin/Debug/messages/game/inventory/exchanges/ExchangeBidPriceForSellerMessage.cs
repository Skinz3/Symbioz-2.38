


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeBidPriceForSellerMessage : ExchangeBidPriceMessage
{

public const ushort Id = 6464;
public override ushort MessageId
{
    get { return Id; }
}

public bool allIdentical;
        public uint[] minimalPrices;
        

public ExchangeBidPriceForSellerMessage()
{
}

public ExchangeBidPriceForSellerMessage(ushort genericId, int averagePrice, bool allIdentical, uint[] minimalPrices)
         : base(genericId, averagePrice)
        {
            this.allIdentical = allIdentical;
            this.minimalPrices = minimalPrices;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteBoolean(allIdentical);
            writer.WriteUShort((ushort)minimalPrices.Length);
            foreach (var entry in minimalPrices)
            {
                 writer.WriteVarUhInt(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            allIdentical = reader.ReadBoolean();
            var limit = reader.ReadUShort();
            minimalPrices = new uint[limit];
            for (int i = 0; i < limit; i++)
            {
                 minimalPrices[i] = reader.ReadVarUhInt();
            }
            

}


}


}