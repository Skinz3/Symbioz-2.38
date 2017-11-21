using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core.Pool
{
    public interface IObjectPool
    {
        int AvailableCount
        {
            get;
        }
        int ObtainedCount
        {
            get;
        }

        void Recycle(object obj);

        object ObtainObj();
    }
}
