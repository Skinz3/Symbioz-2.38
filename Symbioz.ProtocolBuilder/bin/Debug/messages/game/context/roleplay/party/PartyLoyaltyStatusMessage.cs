


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyLoyaltyStatusMessage : AbstractPartyMessage
{

public const ushort Id = 6270;
public override ushort MessageId
{
    get { return Id; }
}

public bool loyal;
        

public PartyLoyaltyStatusMessage()
{
}

public PartyLoyaltyStatusMessage(uint partyId, bool loyal)
         : base(partyId)
        {
            this.loyal = loyal;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteBoolean(loyal);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            loyal = reader.ReadBoolean();
            

}


}


}