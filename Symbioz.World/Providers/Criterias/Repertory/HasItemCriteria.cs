using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias.Repertory
{
    [Criteria("PO")]
    public class HasItemCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            var criteria = CriteriaFull.Remove(0, 3).Split(',');
            uint quantity = 1;
            ushort gid = ushort.Parse(criteria[0]);

            if (criteria.Length > 1)
            {
                quantity = uint.Parse(criteria[1]);
            }
            return client.Character.Inventory.Exist(gid, quantity);
        }
    }
    [Criteria("PT")]
    public class HasItemOfTypeCriteria : AbstractCriteria
    {

        public override bool Eval(WorldClient client)
        {
            var type = (ItemTypeEnum)int.Parse(CriteriaValue);
            var item = client.Character.Inventory.GetFirstItem(type);
            return item != null;
        }
    }
}
