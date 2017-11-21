


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildMemberOnlineStatusMessage : Message
{

public const ushort Id = 6061;
public override ushort MessageId
{
    get { return Id; }
}

public ulong memberId;
        public bool online;
        

public GuildMemberOnlineStatusMessage()
{
}

public GuildMemberOnlineStatusMessage(ulong memberId, bool online)
        {
            this.memberId = memberId;
            this.online = online;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(memberId);
            writer.WriteBoolean(online);
            

}

public override void Deserialize(ICustomDataInput reader)
{

memberId = reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9007199254740990)
                throw new Exception("Forbidden value on memberId = " + memberId + ", it doesn't respect the following condition : memberId < 0 || memberId > 9007199254740990");
            online = reader.ReadBoolean();
            

}


}


}