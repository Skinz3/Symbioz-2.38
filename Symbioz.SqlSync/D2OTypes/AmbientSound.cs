using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.D2OTypes
{
    [D2oType("AmbientSound")]
    public class AmbientSound
    {
        [D2OField("id")]
        public int Id { get; set; }

        [D2OField("volume")]
        public int Volume { get; set; }

    }
}
