


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeBidHouseBuyMessage : Message
{

public const ushort Id = 5804;
public override ushort MessageId
{
    get { return Id; }
}

public uint uid;
        public uint qty;
        public uint price;
        

public ExchangeBidHouseBuyMessage()
{
}

public ExchangeBidHouseBuyMessage(uint uid, uint qty, uint price)
        {
            this.uid = uid;
            this.qty = qty;
            this.price = price;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(uid);
            writer.WriteVarUhInt(qty);
            writer.WriteVarUhInt(price);
            

}

public override void Deserialize(ICustomDataInput reader)
{

uid = reader.ReadVarUhInt();
            if (uid < 0)
                throw new Exception("Forbidden value on uid = " + uid + ", it doesn't respect the following condition : uid < 0");
            qty = reader.ReadVarUhInt();
            if (qty < 0)
                throw new Exception("Forbidden value on qty = " + qty + ", it doesn't respect the following condition : qty < 0");
            price = reader.ReadVarUhInt();
            if (price < 0)
                throw new Exception("Forbidden value on price = " + price + ", it doesn't respect the following condition : price < 0");
            

}


}


}