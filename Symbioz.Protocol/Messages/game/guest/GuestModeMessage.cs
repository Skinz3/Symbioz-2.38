


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuestModeMessage : Message
{

public const ushort Id = 6505;
public override ushort MessageId
{
    get { return Id; }
}

public bool active;
        

public GuestModeMessage()
{
}

public GuestModeMessage(bool active)
        {
            this.active = active;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(active);
            

}

public override void Deserialize(ICustomDataInput reader)
{

active = reader.ReadBoolean();
            

}


}


}