


















// Generated on 04/27/2016 01:13:11
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class FightTeamInformations : AbstractFightTeamInformations
{

public const short Id = 33;
public override short TypeId
{
    get { return Id; }
}

public FightTeamMemberInformations[] teamMembers;
        

public FightTeamInformations()
{
}

public FightTeamInformations(sbyte teamId, double leaderId, sbyte teamSide, sbyte teamTypeId, sbyte nbWaves, FightTeamMemberInformations[] teamMembers)
         : base(teamId, leaderId, teamSide, teamTypeId, nbWaves)
        {
            this.teamMembers = teamMembers;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)teamMembers.Length);
            foreach (var entry in teamMembers)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            teamMembers = new FightTeamMemberInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 teamMembers[i] = Types.ProtocolTypeManager.GetInstance<FightTeamMemberInformations>(reader.ReadShort());
                 teamMembers[i].Deserialize(reader);
            }
            

}


}


}