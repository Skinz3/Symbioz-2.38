


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class IdolPartyRefreshMessage : Message
{

public const ushort Id = 6583;
public override ushort MessageId
{
    get { return Id; }
}

public Types.PartyIdol partyIdol;
        

public IdolPartyRefreshMessage()
{
}

public IdolPartyRefreshMessage(Types.PartyIdol partyIdol)
        {
            this.partyIdol = partyIdol;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

partyIdol.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

partyIdol = new Types.PartyIdol();
            partyIdol.Deserialize(reader);
            

}


}


}