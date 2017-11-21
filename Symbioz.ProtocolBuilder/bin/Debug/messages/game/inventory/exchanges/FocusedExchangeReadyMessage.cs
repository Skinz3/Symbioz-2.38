


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class FocusedExchangeReadyMessage : ExchangeReadyMessage
{

public const ushort Id = 6701;
public override ushort MessageId
{
    get { return Id; }
}

public uint focusActionId;
        

public FocusedExchangeReadyMessage()
{
}

public FocusedExchangeReadyMessage(bool ready, ushort step, uint focusActionId)
         : base(ready, step)
        {
            this.focusActionId = focusActionId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(focusActionId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            focusActionId = reader.ReadVarUhInt();
            if (focusActionId < 0)
                throw new Exception("Forbidden value on focusActionId = " + focusActionId + ", it doesn't respect the following condition : focusActionId < 0");
            

}


}


}