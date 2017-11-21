


















// Generated on 04/27/2016 01:13:12
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class AllianceInformations : BasicNamedAllianceInformations
{

public const short Id = 417;
public override short TypeId
{
    get { return Id; }
}

public Types.GuildEmblem allianceEmblem;
        

public AllianceInformations()
{
}

public AllianceInformations(uint allianceId, string allianceTag, string allianceName, Types.GuildEmblem allianceEmblem)
         : base(allianceId, allianceTag, allianceName)
        {
            this.allianceEmblem = allianceEmblem;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            allianceEmblem.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            allianceEmblem = new Types.GuildEmblem();
            allianceEmblem.Deserialize(reader);
            

}


}


}