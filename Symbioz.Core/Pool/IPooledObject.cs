using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core.Pool
{
    public interface IPooledObject
    {
        void Cleanup();
    }
}
