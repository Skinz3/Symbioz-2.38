


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeBidHouseSearchMessage : Message
{

public const ushort Id = 5806;
public override ushort MessageId
{
    get { return Id; }
}

public uint type;
        public ushort genId;
        

public ExchangeBidHouseSearchMessage()
{
}

public ExchangeBidHouseSearchMessage(uint type, ushort genId)
        {
            this.type = type;
            this.genId = genId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(type);
            writer.WriteVarUhShort(genId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

type = reader.ReadVarUhInt();
            if (type < 0)
                throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
            genId = reader.ReadVarUhShort();
            if (genId < 0)
                throw new Exception("Forbidden value on genId = " + genId + ", it doesn't respect the following condition : genId < 0");
            

}


}


}