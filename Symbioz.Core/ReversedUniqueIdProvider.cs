using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symbioz.Core
{
    public class ReversedUniqueIdProvider : UniqueIdProvider
    {
        public ReversedUniqueIdProvider()
        {
        }

        public ReversedUniqueIdProvider(int lastId)
            : base(lastId)
        {
        }

        public ReversedUniqueIdProvider(IEnumerable<int> freeIds)
            : base(freeIds)
        {
        }

        protected override int Next()
        {
            return Interlocked.Decrement(ref this.m_highestId);
        }
    }
}
