


















// Generated on 04/27/2016 01:13:11
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class FightTeamMemberWithAllianceCharacterInformations : FightTeamMemberCharacterInformations
{

public const short Id = 426;
public override short TypeId
{
    get { return Id; }
}

public Types.BasicAllianceInformations allianceInfos;
        

public FightTeamMemberWithAllianceCharacterInformations()
{
}

public FightTeamMemberWithAllianceCharacterInformations(double id, string name, byte level, Types.BasicAllianceInformations allianceInfos)
         : base(id, name, level)
        {
            this.allianceInfos = allianceInfos;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            allianceInfos.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            allianceInfos = new Types.BasicAllianceInformations();
            allianceInfos.Deserialize(reader);
            

}


}


}