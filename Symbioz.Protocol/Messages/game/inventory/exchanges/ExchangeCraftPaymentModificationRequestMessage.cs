


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeCraftPaymentModificationRequestMessage : Message
{

public const ushort Id = 6579;
public override ushort MessageId
{
    get { return Id; }
}

public uint quantity;
        

public ExchangeCraftPaymentModificationRequestMessage()
{
}

public ExchangeCraftPaymentModificationRequestMessage(uint quantity)
        {
            this.quantity = quantity;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(quantity);
            

}

public override void Deserialize(ICustomDataInput reader)
{

quantity = reader.ReadVarUhInt();
            if (quantity < 0)
                throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
            

}


}


}