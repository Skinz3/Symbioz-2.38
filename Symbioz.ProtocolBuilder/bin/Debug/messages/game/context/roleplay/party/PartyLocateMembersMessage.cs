


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyLocateMembersMessage : AbstractPartyMessage
{

public const ushort Id = 5595;
public override ushort MessageId
{
    get { return Id; }
}

public Types.PartyMemberGeoPosition[] geopositions;
        

public PartyLocateMembersMessage()
{
}

public PartyLocateMembersMessage(uint partyId, Types.PartyMemberGeoPosition[] geopositions)
         : base(partyId)
        {
            this.geopositions = geopositions;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)geopositions.Length);
            foreach (var entry in geopositions)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            geopositions = new Types.PartyMemberGeoPosition[limit];
            for (int i = 0; i < limit; i++)
            {
                 geopositions[i] = new Types.PartyMemberGeoPosition();
                 geopositions[i].Deserialize(reader);
            }
            

}


}


}