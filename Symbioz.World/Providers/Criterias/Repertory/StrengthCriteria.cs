using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias.Repertory
{
    [Criteria("cs")]
    public class StrengthCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats.Strength.Additional);
        }
    }
    [Criteria("CS")]
    public class TotalStrengthCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            return BasicEval(CriteriaValue, ComparaisonSymbol, client.Character.Record.Stats.Strength.Total());
        }
    }
}
