using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias.Repertory
{
    [Criteria("Pk")]
    public class ItemSetCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            if (client.Character.Inventory.MaximumItemSetCount() < int.Parse(CriteriaValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
