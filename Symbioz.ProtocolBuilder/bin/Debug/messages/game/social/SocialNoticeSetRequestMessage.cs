


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class SocialNoticeSetRequestMessage : Message
{

public const ushort Id = 6686;
public override ushort MessageId
{
    get { return Id; }
}



public SocialNoticeSetRequestMessage()
{
}



public override void Serialize(ICustomDataOutput writer)
{



}

public override void Deserialize(ICustomDataInput reader)
{



}


}


}