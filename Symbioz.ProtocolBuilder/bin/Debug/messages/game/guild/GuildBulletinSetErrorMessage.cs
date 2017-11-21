


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildBulletinSetErrorMessage : SocialNoticeSetErrorMessage
{

public const ushort Id = 6691;
public override ushort MessageId
{
    get { return Id; }
}



public GuildBulletinSetErrorMessage()
{
}

public GuildBulletinSetErrorMessage(sbyte reason)
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