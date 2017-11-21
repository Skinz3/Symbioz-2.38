


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyInvitationArenaRequestMessage : PartyInvitationRequestMessage
{

public const ushort Id = 6283;
public override ushort MessageId
{
    get { return Id; }
}



public PartyInvitationArenaRequestMessage()
{
}

public PartyInvitationArenaRequestMessage(string name)
         : base(name)
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