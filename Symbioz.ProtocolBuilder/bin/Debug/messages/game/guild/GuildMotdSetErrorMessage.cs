


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildMotdSetErrorMessage : SocialNoticeSetErrorMessage
{

public const ushort Id = 6591;
public override ushort MessageId
{
    get { return Id; }
}



public GuildMotdSetErrorMessage()
{
}

public GuildMotdSetErrorMessage(sbyte reason)
         : base(reason)
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