


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class FriendSetWarnOnLevelGainMessage : Message
{

public const ushort Id = 6077;
public override ushort MessageId
{
    get { return Id; }
}

public bool enable;
        

public FriendSetWarnOnLevelGainMessage()
{
}

public FriendSetWarnOnLevelGainMessage(bool enable)
        {
            this.enable = enable;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(enable);
            

}

public override void Deserialize(ICustomDataInput reader)
{

enable = reader.ReadBoolean();
            

}


}


}