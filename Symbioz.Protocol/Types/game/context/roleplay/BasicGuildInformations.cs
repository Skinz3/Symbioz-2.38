


















// Generated on 04/27/2016 01:13:12
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class BasicGuildInformations : AbstractSocialGroupInfos
{

public const short Id = 365;
public override short TypeId
{
    get { return Id; }
}

public uint guildId;
        public string guildName;
        public byte guildLevel;
        

public BasicGuildInformations()
{
}

public BasicGuildInformations(uint guildId, string guildName, byte guildLevel)
        {
            this.guildId = guildId;
            this.guildName = guildName;
            this.guildLevel = guildLevel;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(guildId);
            writer.WriteUTF(guildName);
            writer.WriteByte(guildLevel);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            guildId = reader.ReadVarUhInt();
            if (guildId < 0)
                throw new Exception("Forbidden value on guildId = " + guildId + ", it doesn't respect the following condition : guildId < 0");
            guildName = reader.ReadUTF();
            guildLevel = reader.ReadByte();
            if (guildLevel < 0 || guildLevel > 200)
                throw new Exception("Forbidden value on guildLevel = " + guildLevel + ", it doesn't respect the following condition : guildLevel < 0 || guildLevel > 200");
            

}


}


}