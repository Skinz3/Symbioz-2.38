


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class KrosmasterAuthTokenRequestMessage : Message
{

public const ushort Id = 6346;
public override ushort MessageId
{
    get { return Id; }
}



public KrosmasterAuthTokenRequestMessage()
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