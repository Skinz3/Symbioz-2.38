


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MapRunningFightDetailsExtendedMessage : MapRunningFightDetailsMessage
{

public const ushort Id = 6500;
public override ushort MessageId
{
    get { return Id; }
}

public Types.NamedPartyTeam[] namedPartyTeams;
        

public MapRunningFightDetailsExtendedMessage()
{
}

public MapRunningFightDetailsExtendedMessage(int fightId, Types.GameFightFighterLightInformations[] attackers, Types.GameFightFighterLightInformations[] defenders, Types.NamedPartyTeam[] namedPartyTeams)
         : base(fightId, attackers, defenders)
        {
            this.namedPartyTeams = namedPartyTeams;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)namedPartyTeams.Length);
            foreach (var entry in namedPartyTeams)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            namedPartyTeams = new Types.NamedPartyTeam[limit];
            for (int i = 0; i < limit; i++)
            {
                 namedPartyTeams[i] = new Types.NamedPartyTeam();
                 namedPartyTeams[i].Deserialize(reader);
            }
            

}


}


}