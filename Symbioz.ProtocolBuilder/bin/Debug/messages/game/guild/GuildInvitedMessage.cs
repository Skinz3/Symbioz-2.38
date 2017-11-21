


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildInvitedMessage : Message
{

public const ushort Id = 5552;
public override ushort MessageId
{
    get { return Id; }
}

public ulong recruterId;
        public string recruterName;
        public Types.BasicGuildInformations guildInfo;
        

public GuildInvitedMessage()
{
}

public GuildInvitedMessage(ulong recruterId, string recruterName, Types.BasicGuildInformations guildInfo)
        {
            this.recruterId = recruterId;
            this.recruterName = recruterName;
            this.guildInfo = guildInfo;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(recruterId);
            writer.WriteUTF(recruterName);
            guildInfo.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

recruterId = reader.ReadVarUhLong();
            if (recruterId < 0 || recruterId > 9007199254740990)
                throw new Exception("Forbidden value on recruterId = " + recruterId + ", it doesn't respect the following condition : recruterId < 0 || recruterId > 9007199254740990");
            recruterName = reader.ReadUTF();
            guildInfo = new Types.BasicGuildInformations();
            guildInfo.Deserialize(reader);
            

}


}


}