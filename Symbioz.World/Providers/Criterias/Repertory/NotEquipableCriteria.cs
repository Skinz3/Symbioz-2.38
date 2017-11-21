using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Network;
using Symbioz.Protocol.Selfmade.Enums;

namespace Symbioz.World.Providers.Criterias.Repertory
{
    [Criteria("BI")]
    public class NotEquipableCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            return client.Account.Role == ServerRoleEnum.Fondator;
        }
    }
}
