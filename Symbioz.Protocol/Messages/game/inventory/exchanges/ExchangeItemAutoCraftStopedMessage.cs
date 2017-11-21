


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeItemAutoCraftStopedMessage : Message
{

public const ushort Id = 5810;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte reason;
        

public ExchangeItemAutoCraftStopedMessage()
{
}

public ExchangeItemAutoCraftStopedMessage(sbyte reason)
        {
            this.reason = reason;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(reason);
            

}

public override void Deserialize(ICustomDataInput reader)
{

reason = reader.ReadSByte();
            

}


}


}