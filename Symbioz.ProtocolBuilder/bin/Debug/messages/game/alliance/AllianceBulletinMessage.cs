


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceBulletinMessage : BulletinMessage
{

public const ushort Id = 6690;
public override ushort MessageId
{
    get { return Id; }
}



public AllianceBulletinMessage()
{
}

public AllianceBulletinMessage(string content, int timestamp, ulong memberId, string memberName, int lastNotifiedTimestamp)
         : base(content, timestamp, memberId, memberName, lastNotifiedTimestamp)
        {
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            

}


}


}