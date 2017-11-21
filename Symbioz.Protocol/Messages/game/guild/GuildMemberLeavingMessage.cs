


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildMemberLeavingMessage : Message
{

public const ushort Id = 5923;
public override ushort MessageId
{
    get { return Id; }
}

public bool kicked;
        public ulong memberId;
        

public GuildMemberLeavingMessage()
{
}

public GuildMemberLeavingMessage(bool kicked, ulong memberId)
        {
            this.kicked = kicked;
            this.memberId = memberId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(kicked);
            writer.WriteVarUhLong(memberId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

kicked = reader.ReadBoolean();
            memberId = reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9007199254740990)
                throw new Exception("Forbidden value on memberId = " + memberId + ", it doesn't respect the following condition : memberId < 0 || memberId > 9007199254740990");
            

}


}


}