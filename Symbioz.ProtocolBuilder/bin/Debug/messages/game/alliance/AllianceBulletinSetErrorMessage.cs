


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceBulletinSetErrorMessage : SocialNoticeSetErrorMessage
{

public const ushort Id = 6692;
public override ushort MessageId
{
    get { return Id; }
}



public AllianceBulletinSetErrorMessage()
{
}

public AllianceBulletinSetErrorMessage(sbyte reason)
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