


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayFreeSoulRequestMessage : Message
{

public const ushort Id = 745;
public override ushort MessageId
{
    get { return Id; }
}



public GameRolePlayFreeSoulRequestMessage()
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