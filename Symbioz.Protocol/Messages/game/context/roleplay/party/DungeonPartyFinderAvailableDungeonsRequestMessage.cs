


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DungeonPartyFinderAvailableDungeonsRequestMessage : Message
{

public const ushort Id = 6240;
public override ushort MessageId
{
    get { return Id; }
}



public DungeonPartyFinderAvailableDungeonsRequestMessage()
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