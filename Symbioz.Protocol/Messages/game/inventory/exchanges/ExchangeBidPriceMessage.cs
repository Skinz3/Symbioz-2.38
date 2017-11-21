


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeBidPriceMessage : Message
{

public const ushort Id = 5755;
public override ushort MessageId
{
    get { return Id; }
}

public ushort genericId;
        public int averagePrice;
        

public ExchangeBidPriceMessage()
{
}

public ExchangeBidPriceMessage(ushort genericId, int averagePrice)
        {
            this.genericId = genericId;
            this.averagePrice = averagePrice;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(genericId);
            writer.WriteVarInt(averagePrice);
            

}

public override void Deserialize(ICustomDataInput reader)
{

genericId = reader.ReadVarUhShort();
            if (genericId < 0)
                throw new Exception("Forbidden value on genericId = " + genericId + ", it doesn't respect the following condition : genericId < 0");
            averagePrice = reader.ReadVarInt();
            

}


}


}