


















// Generated on 04/27/2016 01:13:13
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GuildInAllianceInformations : GuildInformations
{

public const short Id = 420;
public override short TypeId
{
    get { return Id; }
}

public byte nbMembers;
        public bool enabled;
        

public GuildInAllianceInformations()
{
}

public GuildInAllianceInformations(uint guildId, string guildName, byte guildLevel, Types.GuildEmblem guildEmblem, byte nbMembers, bool enabled)
         : base(guildId, guildName, guildLevel, guildEmblem)
        {
            this.nbMembers = nbMembers;
            this.enabled = enabled;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteByte(nbMembers);
            writer.WriteBoolean(enabled);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            nbMembers = reader.ReadByte();
            if (nbMembers < 1 || nbMembers > 240)
                throw new Exception("Forbidden value on nbMembers = " + nbMembers + ", it doesn't respect the following condition : nbMembers < 1 || nbMembers > 240");
            enabled = reader.ReadBoolean();
            

}


}


}