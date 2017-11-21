


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CharacterSelectedErrorMessage : Message
{

public const ushort Id = 5836;
public override ushort MessageId
{
    get { return Id; }
}



public CharacterSelectedErrorMessage()
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