


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangePlayerRequestMessage : ExchangeRequestMessage
{

public const ushort Id = 5773;
public override ushort MessageId
{
    get { return Id; }
}

public ulong target;
        

public ExchangePlayerRequestMessage()
{
}

public ExchangePlayerRequestMessage(sbyte exchangeType, ulong target)
         : base(exchangeType)
        {
            this.target = target;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhLong(target);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            target = reader.ReadVarUhLong();
            if (target < 0 || target > 9007199254740990)
                throw new Exception("Forbidden value on target = " + target + ", it doesn't respect the following condition : target < 0 || target > 9007199254740990");
            

}


}


}