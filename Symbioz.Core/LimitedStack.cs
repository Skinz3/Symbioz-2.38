using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core
{
    [Serializable]
    public class LimitedStack<T> : LinkedList<T>
    {
        private int m_maxItems;
        public int MaxItems
        {
            get
            {
                return this.m_maxItems;
            }
            set
            {
                while (base.Count > value)
                {
                    base.RemoveFirst();
                }
                this.m_maxItems = value;
            }
        }

        public LimitedStack(int num)
        {
            this.m_maxItems = num;
        }

        public T Peek()
        {
            return base.Last.Value;
        }

        public T Pop()
        {
            LinkedListNode<T> last = base.Last;
            base.RemoveLast();
            return last.Value;
        }

        public void Push(T value)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(value);
            base.AddLast(node);
            if (base.Count > this.m_maxItems)
            {
                base.RemoveFirst();
            }
        }
    }
}
