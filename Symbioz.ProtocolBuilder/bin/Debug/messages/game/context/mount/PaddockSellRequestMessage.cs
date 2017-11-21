


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PaddockSellRequestMessage : Message
{

public const ushort Id = 5953;
public override ushort MessageId
{
    get { return Id; }
}

public uint price;
        public bool forSale;
        

public PaddockSellRequestMessage()
{
}

public PaddockSellRequestMessage(uint price, bool forSale)
        {
            this.price = price;
            this.forSale = forSale;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(price);
            writer.WriteBoolean(forSale);
            

}

public override void Deserialize(ICustomDataInput reader)
{

price = reader.ReadVarUhInt();
            if (price < 0)
                throw new Exception("Forbidden value on price = " + price + ", it doesn't respect the following condition : price < 0");
            forSale = reader.ReadBoolean();
            

}


}


}