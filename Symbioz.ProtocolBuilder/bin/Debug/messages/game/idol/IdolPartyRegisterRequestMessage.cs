


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class IdolPartyRegisterRequestMessage : Message
{

public const ushort Id = 6582;
public override ushort MessageId
{
    get { return Id; }
}

public bool register;
        

public IdolPartyRegisterRequestMessage()
{
}

public IdolPartyRegisterRequestMessage(bool register)
        {
            this.register = register;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(register);
            

}

public override void Deserialize(ICustomDataInput reader)
{

register = reader.ReadBoolean();
            

}


}


}