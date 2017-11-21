using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias.Repertory
{
    [Criteria("PG")]
    public class BreedCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            return client.Character.Record.BreedId == sbyte.Parse(CriteriaValue);
             
        }
    }
}
