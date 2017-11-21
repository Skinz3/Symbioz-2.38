


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class EditHavenBagFinishedMessage : Message
{

public const ushort Id = 6628;
public override ushort MessageId
{
    get { return Id; }
}



public EditHavenBagFinishedMessage()
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