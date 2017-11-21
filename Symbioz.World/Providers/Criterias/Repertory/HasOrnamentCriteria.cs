using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias.Repertory
{
    [Criteria("OR")]
    class HasOrnamentCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            bool obj = client.Character.HasOrnament(ushort.Parse(CriteriaValue));

            return ComparaisonSymbol == '=' ? obj : !obj;
        }
    }

}
