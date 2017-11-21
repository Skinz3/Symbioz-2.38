


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildFightJoinRequestMessage : Message
{

public const ushort Id = 5717;
public override ushort MessageId
{
    get { return Id; }
}

public int taxCollectorId;
        

public GuildFightJoinRequestMessage()
{
}

public GuildFightJoinRequestMessage(int taxCollectorId)
        {
            this.taxCollectorId = taxCollectorId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(taxCollectorId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

taxCollectorId = reader.ReadInt();
            

}


}


}