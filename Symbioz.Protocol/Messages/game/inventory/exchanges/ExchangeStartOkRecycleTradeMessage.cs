


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeStartOkRecycleTradeMessage : Message
{

public const ushort Id = 6600;
public override ushort MessageId
{
    get { return Id; }
}

public short percentToPrism;
        public short percentToPlayer;
        

public ExchangeStartOkRecycleTradeMessage()
{
}

public ExchangeStartOkRecycleTradeMessage(short percentToPrism, short percentToPlayer)
        {
            this.percentToPrism = percentToPrism;
            this.percentToPlayer = percentToPlayer;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteShort(percentToPrism);
            writer.WriteShort(percentToPlayer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

percentToPrism = reader.ReadShort();
            if (percentToPrism < 0)
                throw new Exception("Forbidden value on percentToPrism = " + percentToPrism + ", it doesn't respect the following condition : percentToPrism < 0");
            percentToPlayer = reader.ReadShort();
            if (percentToPlayer < 0)
                throw new Exception("Forbidden value on percentToPlayer = " + percentToPlayer + ", it doesn't respect the following condition : percentToPlayer < 0");
            

}


}


}