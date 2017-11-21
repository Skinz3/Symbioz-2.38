


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeStartOkMulticraftCustomerMessage : Message
{

public const ushort Id = 5817;
public override ushort MessageId
{
    get { return Id; }
}

public uint skillId;
        public byte crafterJobLevel;
        

public ExchangeStartOkMulticraftCustomerMessage()
{
}

public ExchangeStartOkMulticraftCustomerMessage(uint skillId, byte crafterJobLevel)
        {
            this.skillId = skillId;
            this.crafterJobLevel = crafterJobLevel;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(skillId);
            writer.WriteByte(crafterJobLevel);
            

}

public override void Deserialize(ICustomDataInput reader)
{

skillId = reader.ReadVarUhInt();
            if (skillId < 0)
                throw new Exception("Forbidden value on skillId = " + skillId + ", it doesn't respect the following condition : skillId < 0");
            crafterJobLevel = reader.ReadByte();
            if (crafterJobLevel < 0 || crafterJobLevel > 255)
                throw new Exception("Forbidden value on crafterJobLevel = " + crafterJobLevel + ", it doesn't respect the following condition : crafterJobLevel < 0 || crafterJobLevel > 255");
            

}


}


}