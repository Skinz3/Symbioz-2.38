


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AlliancePrismDialogQuestionMessage : Message
{

public const ushort Id = 6448;
public override ushort MessageId
{
    get { return Id; }
}



public AlliancePrismDialogQuestionMessage()
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