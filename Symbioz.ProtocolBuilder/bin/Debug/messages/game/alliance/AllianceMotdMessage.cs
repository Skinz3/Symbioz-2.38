


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceMotdMessage : SocialNoticeMessage
{

public const ushort Id = 6685;
public override ushort MessageId
{
    get { return Id; }
}



public AllianceMotdMessage()
{
}

public AllianceMotdMessage(string content, int timestamp, ulong memberId, string memberName)
         : base(content, timestamp, memberId, memberName)
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