


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class IdolPartyLostMessage : Message
{

public const ushort Id = 6580;
public override ushort MessageId
{
    get { return Id; }
}

public ushort idolId;
        

public IdolPartyLostMessage()
{
}

public IdolPartyLostMessage(ushort idolId)
        {
            this.idolId = idolId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(idolId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

idolId = reader.ReadVarUhShort();
            if (idolId < 0)
                throw new Exception("Forbidden value on idolId = " + idolId + ", it doesn't respect the following condition : idolId < 0");
            

}


}


}