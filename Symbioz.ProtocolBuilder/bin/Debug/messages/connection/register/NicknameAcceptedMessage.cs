


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class NicknameAcceptedMessage : Message
{

public const ushort Id = 5641;
public override ushort MessageId
{
    get { return Id; }
}



public NicknameAcceptedMessage()
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