


















// Generated on 04/27/2016 01:13:19
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GuildFactSheetInformations : GuildInformations
{

public const short Id = 424;
public override short TypeId
{
    get { return Id; }
}

public ulong leaderId;
        public ushort nbMembers;
        

public GuildFactSheetInformations()
{
}

public GuildFactSheetInformations(uint guildId, string guildName, byte guildLevel, Types.GuildEmblem guildEmblem, ulong leaderId, ushort nbMembers)
         : base(guildId, guildName, guildLevel, guildEmblem)
        {
            this.leaderId = leaderId;
            this.nbMembers = nbMembers;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhLong(leaderId);
            writer.WriteVarUhShort(nbMembers);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            leaderId = reader.ReadVarUhLong();
            if (leaderId < 0 || leaderId > 9007199254740990)
                throw new Exception("Forbidden value on leaderId = " + leaderId + ", it doesn't respect the following condition : leaderId < 0 || leaderId > 9007199254740990");
            nbMembers = reader.ReadVarUhShort();
            if (nbMembers < 0)
                throw new Exception("Forbidden value on nbMembers = " + nbMembers + ", it doesn't respect the following condition : nbMembers < 0");
            

}


}


}