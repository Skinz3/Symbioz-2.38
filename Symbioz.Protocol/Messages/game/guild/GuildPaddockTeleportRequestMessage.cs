


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildPaddockTeleportRequestMessage : Message
{

public const ushort Id = 5957;
public override ushort MessageId
{
    get { return Id; }
}

public int paddockId;
        

public GuildPaddockTeleportRequestMessage()
{
}

public GuildPaddockTeleportRequestMessage(int paddockId)
        {
            this.paddockId = paddockId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(paddockId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

paddockId = reader.ReadInt();
            

}


}


}