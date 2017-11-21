


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceKickRequestMessage : Message
{

public const ushort Id = 6400;
public override ushort MessageId
{
    get { return Id; }
}

public uint kickedId;
        

public AllianceKickRequestMessage()
{
}

public AllianceKickRequestMessage(uint kickedId)
        {
            this.kickedId = kickedId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(kickedId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

kickedId = reader.ReadVarUhInt();
            if (kickedId < 0)
                throw new Exception("Forbidden value on kickedId = " + kickedId + ", it doesn't respect the following condition : kickedId < 0");
            

}


}


}