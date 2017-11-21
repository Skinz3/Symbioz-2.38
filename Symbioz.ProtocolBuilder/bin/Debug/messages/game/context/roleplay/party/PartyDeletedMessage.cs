


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyDeletedMessage : AbstractPartyMessage
{

public const ushort Id = 6261;
public override ushort MessageId
{
    get { return Id; }
}



public PartyDeletedMessage()
{
}

public PartyDeletedMessage(uint partyId)
         : base(partyId)
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