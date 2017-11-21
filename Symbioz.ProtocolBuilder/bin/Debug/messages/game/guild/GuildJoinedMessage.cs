


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildJoinedMessage : Message
{

public const ushort Id = 5564;
public override ushort MessageId
{
    get { return Id; }
}

public Types.GuildInformations guildInfo;
        public uint memberRights;
        public bool enabled;
        

public GuildJoinedMessage()
{
}

public GuildJoinedMessage(Types.GuildInformations guildInfo, uint memberRights, bool enabled)
        {
            this.guildInfo = guildInfo;
            this.memberRights = memberRights;
            this.enabled = enabled;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

guildInfo.Serialize(writer);
            writer.WriteVarUhInt(memberRights);
            writer.WriteBoolean(enabled);
            

}

public override void Deserialize(ICustomDataInput reader)
{

guildInfo = new Types.GuildInformations();
            guildInfo.Deserialize(reader);
            memberRights = reader.ReadVarUhInt();
            if (memberRights < 0)
                throw new Exception("Forbidden value on memberRights = " + memberRights + ", it doesn't respect the following condition : memberRights < 0");
            enabled = reader.ReadBoolean();
            

}


}


}