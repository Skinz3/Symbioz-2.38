using Symbioz.World.Network;
using Symbioz.World.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias.Repertory
{
    [Criteria("Pw")]
    public class HasGuildCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            return !client.Character.HasGuild;
        }
    }
    [Criteria("Py")]
    public class GuildLevelCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            if (!client.Character.HasGuild)
                return false;
            else
                return BasicEval(CriteriaValue, ComparaisonSymbol, ExperienceRecord.GetLevelFromGuildExperience(client.Character.Guild.Record.Experience));
        }
    }
}
