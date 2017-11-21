


















// Generated on 04/27/2016 01:13:19
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GuildVersatileInformations
{

public const short Id = 435;
public virtual short TypeId
{
    get { return Id; }
}

public uint guildId;
        public ulong leaderId;
        public byte guildLevel;
        public byte nbMembers;
        

public GuildVersatileInformations()
{
}

public GuildVersatileInformations(uint guildId, ulong leaderId, byte guildLevel, byte nbMembers)
        {
            this.guildId = guildId;
            this.leaderId = leaderId;
            this.guildLevel = guildLevel;
            this.nbMembers = nbMembers;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(guildId);
            writer.WriteVarUhLong(leaderId);
            writer.WriteByte(guildLevel);
            writer.WriteByte(nbMembers);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

guildId = reader.ReadVarUhInt();
            if (guildId < 0)
                throw new Exception("Forbidden value on guildId = " + guildId + ", it doesn't respect the following condition : guildId < 0");
            leaderId = reader.ReadVarUhLong();
            if (leaderId < 0 || leaderId > 9007199254740990)
                throw new Exception("Forbidden value on leaderId = " + leaderId + ", it doesn't respect the following condition : leaderId < 0 || leaderId > 9007199254740990");
            guildLevel = reader.ReadByte();
            if (guildLevel < 1 || guildLevel > 200)
                throw new Exception("Forbidden value on guildLevel = " + guildLevel + ", it doesn't respect the following condition : guildLevel < 1 || guildLevel > 200");
            nbMembers = reader.ReadByte();
            if (nbMembers < 1 || nbMembers > 240)
                throw new Exception("Forbidden value on nbMembers = " + nbMembers + ", it doesn't respect the following condition : nbMembers < 1 || nbMembers > 240");
            

}


}


}