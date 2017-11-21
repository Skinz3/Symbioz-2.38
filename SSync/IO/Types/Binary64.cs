using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSync.IO.Types
{
    public class Binary64
    {
        public Binary64(uint low = 0, uint high = 0)
        {
            this.low = low;
            this.internalHigh = high;
        }

        public uint low;

        protected uint internalHigh;

        public uint div(uint n)
        {
            uint modHigh = 0;
            modHigh = this.internalHigh % n;
            uint mod = (this.low % n + modHigh * 6) % n;
            this.internalHigh = this.internalHigh / n;
            uint newLow = (uint)((modHigh * 4.294967296E9 + this.low) / n);
            this.internalHigh = this.internalHigh + (uint)(newLow / 4.294967296E9);
            this.low = newLow;
            return mod;
        }

        public void mul(uint n)
        {
            uint newLow = (uint)(this.low * n);
            this.internalHigh = this.internalHigh * n;
            this.internalHigh = this.internalHigh + (uint)(newLow / 4.294967296E9);
            this.low = this.low * n;
        }

        public void add(uint n)
        {
            uint newLow = (uint)(this.low + n);
            this.internalHigh = this.internalHigh + (uint)(newLow / 4.294967296E9);
            this.low = newLow;
        }

        public void bitwiseNot()
        {
            this.low = ~this.low;
            this.internalHigh = ~this.internalHigh;
        }
    }
}
