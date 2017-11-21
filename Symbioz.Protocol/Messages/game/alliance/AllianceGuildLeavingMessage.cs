


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceGuildLeavingMessage : Message
{

public const ushort Id = 6399;
public override ushort MessageId
{
    get { return Id; }
}

public bool kicked;
        public uint guildId;
        

public AllianceGuildLeavingMessage()
{
}

public AllianceGuildLeavingMessage(bool kicked, uint guildId)
        {
            this.kicked = kicked;
            this.guildId = guildId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(kicked);
            writer.WriteVarUhInt(guildId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

kicked = reader.ReadBoolean();
            guildId = reader.ReadVarUhInt();
            if (guildId < 0)
                throw new Exception("Forbidden value on guildId = " + guildId + ", it doesn't respect the following condition : guildId < 0");
            

}


}


}