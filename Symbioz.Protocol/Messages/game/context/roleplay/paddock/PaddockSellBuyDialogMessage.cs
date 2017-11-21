


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PaddockSellBuyDialogMessage : Message
{

public const ushort Id = 6018;
public override ushort MessageId
{
    get { return Id; }
}

public bool bsell;
        public uint ownerId;
        public uint price;
        

public PaddockSellBuyDialogMessage()
{
}

public PaddockSellBuyDialogMessage(bool bsell, uint ownerId, uint price)
        {
            this.bsell = bsell;
            this.ownerId = ownerId;
            this.price = price;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(bsell);
            writer.WriteVarUhInt(ownerId);
            writer.WriteVarUhInt(price);
            

}

public override void Deserialize(ICustomDataInput reader)
{

bsell = reader.ReadBoolean();
            ownerId = reader.ReadVarUhInt();
            if (ownerId < 0)
                throw new Exception("Forbidden value on ownerId = " + ownerId + ", it doesn't respect the following condition : ownerId < 0");
            price = reader.ReadVarUhInt();
            if (price < 0)
                throw new Exception("Forbidden value on price = " + price + ", it doesn't respect the following condition : price < 0");
            

}


}


}