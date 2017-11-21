using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias.Repertory
{
    [Criteria("cw")]
    public class WisdomCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
           return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats.Wisdom.Additional);
        }
    }
    [Criteria("CW")]
    public class TotalWisdomCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats.Wisdom.Total());
        }
    }
}
