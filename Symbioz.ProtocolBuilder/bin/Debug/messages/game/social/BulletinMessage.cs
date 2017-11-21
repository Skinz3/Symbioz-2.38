


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class BulletinMessage : SocialNoticeMessage
{

public const ushort Id = 6695;
public override ushort MessageId
{
    get { return Id; }
}

public int lastNotifiedTimestamp;
        

public BulletinMessage()
{
}

public BulletinMessage(string content, int timestamp, ulong memberId, string memberName, int lastNotifiedTimestamp)
         : base(content, timestamp, memberId, memberName)
        {
            this.lastNotifiedTimestamp = lastNotifiedTimestamp;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteInt(lastNotifiedTimestamp);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            lastNotifiedTimestamp = reader.ReadInt();
            if (lastNotifiedTimestamp < 0)
                throw new Exception("Forbidden value on lastNotifiedTimestamp = " + lastNotifiedTimestamp + ", it doesn't respect the following condition : lastNotifiedTimestamp < 0");
            

}


}


}