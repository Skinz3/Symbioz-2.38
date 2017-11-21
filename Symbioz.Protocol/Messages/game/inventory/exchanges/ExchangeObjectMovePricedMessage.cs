


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeObjectMovePricedMessage : ExchangeObjectMoveMessage
{

public const ushort Id = 5514;
public override ushort MessageId
{
    get { return Id; }
}

public uint price;
        

public ExchangeObjectMovePricedMessage()
{
}

public ExchangeObjectMovePricedMessage(uint objectUID, int quantity, uint price)
         : base(objectUID, quantity)
        {
            this.price = price;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(price);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            price = reader.ReadVarUhInt();
            if (price < 0)
                throw new Exception("Forbidden value on price = " + price + ", it doesn't respect the following condition : price < 0");
            

}


}


}