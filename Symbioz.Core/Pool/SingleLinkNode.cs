using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core.Pool
{
    public class SingleLinkNode<T>
    {
        public SingleLinkNode<T> Next;
        public T Item;
    }
}
