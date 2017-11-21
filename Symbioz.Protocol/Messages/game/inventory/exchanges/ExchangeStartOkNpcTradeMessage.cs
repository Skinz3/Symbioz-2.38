


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeStartOkNpcTradeMessage : Message
{

public const ushort Id = 5785;
public override ushort MessageId
{
    get { return Id; }
}

public double npcId;
        

public ExchangeStartOkNpcTradeMessage()
{
}

public ExchangeStartOkNpcTradeMessage(double npcId)
        {
            this.npcId = npcId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(npcId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

npcId = reader.ReadDouble();
            if (npcId < -9007199254740990 || npcId > 9007199254740990)
                throw new Exception("Forbidden value on npcId = " + npcId + ", it doesn't respect the following condition : npcId < -9007199254740990 || npcId > 9007199254740990");
            

}


}


}