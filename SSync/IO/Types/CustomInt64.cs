using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSync.IO.Types
{
    public class CustomInt64 : Binary64
    {
        public CustomInt64()
            : base()
        { }

        public CustomInt64(uint low = 0, uint high = 0)
            : base(low, high)
        { }

        public static CustomInt64 fromNumber(long n)
        {
            return new CustomInt64((uint)n, (uint)Math.Floor(n / 4.294967296E9));
        }

        public uint high
        {
            get { return internalHigh; }
            set { internalHigh = value; }
        }

        public long toNumber()
        {
            return (long)(this.high * 4.294967296E9 + low);
        }
    }
}