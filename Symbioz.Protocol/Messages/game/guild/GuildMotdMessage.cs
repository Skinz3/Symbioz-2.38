


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildMotdMessage : Message
{

public const ushort Id = 6590;
public override ushort MessageId
{
    get { return Id; }
}

public string content;
        public int timestamp;
        public ulong memberId;
        public string memberName;
        

public GuildMotdMessage()
{
}

public GuildMotdMessage(string content, int timestamp, ulong memberId, string memberName)
        {
            this.content = content;
            this.timestamp = timestamp;
            this.memberId = memberId;
            this.memberName = memberName;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(content);
            writer.WriteInt(timestamp);
            writer.WriteVarUhLong(memberId);
            writer.WriteUTF(memberName);
            

}

public override void Deserialize(ICustomDataInput reader)
{

content = reader.ReadUTF();
            timestamp = reader.ReadInt();
            if (timestamp < 0)
                throw new Exception("Forbidden value on timestamp = " + timestamp + ", it doesn't respect the following condition : timestamp < 0");
            memberId = reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9007199254740990)
                throw new Exception("Forbidden value on memberId = " + memberId + ", it doesn't respect the following condition : memberId < 0 || memberId > 9007199254740990");
            memberName = reader.ReadUTF();
            

}


}


}