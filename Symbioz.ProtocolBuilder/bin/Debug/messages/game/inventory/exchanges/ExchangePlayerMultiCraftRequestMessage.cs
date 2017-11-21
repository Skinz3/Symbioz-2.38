


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangePlayerMultiCraftRequestMessage : ExchangeRequestMessage
{

public const ushort Id = 5784;
public override ushort MessageId
{
    get { return Id; }
}

public ulong target;
        public uint skillId;
        

public ExchangePlayerMultiCraftRequestMessage()
{
}

public ExchangePlayerMultiCraftRequestMessage(sbyte exchangeType, ulong target, uint skillId)
         : base(exchangeType)
        {
            this.target = target;
            this.skillId = skillId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhLong(target);
            writer.WriteVarUhInt(skillId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            target = reader.ReadVarUhLong();
            if (target < 0 || target > 9007199254740990)
                throw new Exception("Forbidden value on target = " + target + ", it doesn't respect the following condition : target < 0 || target > 9007199254740990");
            skillId = reader.ReadVarUhInt();
            if (skillId < 0)
                throw new Exception("Forbidden value on skillId = " + skillId + ", it doesn't respect the following condition : skillId < 0");
            

}


}


}