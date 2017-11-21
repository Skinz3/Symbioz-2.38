


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceInsiderInfoRequestMessage : Message
{

public const ushort Id = 6417;
public override ushort MessageId
{
    get { return Id; }
}



public AllianceInsiderInfoRequestMessage()
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