using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Damages
{
    public class Jet
    {
        public short DeltaMin
        {
            get;
            set;
        }
        public short DeltaMax
        {
            get;
            set;
        }
        public short Delta
        {
            get;
            set;
        }
        public Jet(short deltaMin, short deltaMax, short delta)
        {
            this.DeltaMin = deltaMin;
            this.DeltaMax = deltaMax;
            this.Delta = delta;
        }
        public Jet Clone()
        {
            return new Jet(DeltaMin, DeltaMax, Delta);
        }

    }
}
