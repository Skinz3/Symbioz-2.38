


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeObjectModifyPricedMessage : ExchangeObjectMovePricedMessage
{

public const ushort Id = 6238;
public override ushort MessageId
{
    get { return Id; }
}



public ExchangeObjectModifyPricedMessage()
{
}

public ExchangeObjectModifyPricedMessage(uint objectUID, int quantity, uint price)
         : base(objectUID, quantity, price)
        {
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            

}


}


}