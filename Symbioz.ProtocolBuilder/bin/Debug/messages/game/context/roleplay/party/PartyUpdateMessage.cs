


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyUpdateMessage : AbstractPartyEventMessage
{

public const ushort Id = 5575;
public override ushort MessageId
{
    get { return Id; }
}

public Types.PartyMemberInformations memberInformations;
        

public PartyUpdateMessage()
{
}

public PartyUpdateMessage(uint partyId, Types.PartyMemberInformations memberInformations)
         : base(partyId)
        {
            this.memberInformations = memberInformations;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteShort(memberInformations.TypeId);
            memberInformations.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            memberInformations = ProtocolTypeManager.GetInstance<Types.PartyMemberInformations>(reader.ReadShort());
            memberInformations.Deserialize(reader);
            

}


}


}