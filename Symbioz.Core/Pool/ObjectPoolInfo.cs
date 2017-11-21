using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core.Pool
{
    public struct ObjectPoolInfo
    {
        public int HardReferences;
        public int WeakReferences;

        public ObjectPoolInfo(int weak, int hard)
        {
            this.HardReferences = hard;
            this.WeakReferences = weak;
        }
    }
}
