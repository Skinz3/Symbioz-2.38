using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias.Repertory
{
    [Criteria("PE")]
    public class EmoteCriteria : AbstractCriteria
    {
        public override bool Eval(WorldClient client)
        {
            return !client.Character.Record.KnownEmotes.Contains((byte)Int32.Parse(CriteriaValue));
        }
    }
}
