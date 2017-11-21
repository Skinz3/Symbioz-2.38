using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symbioz.Core.Pool
{
    public class ObjectPool<T> : IObjectPool where T : class
    {
        private bool m_isBalanced;
        private readonly LockFreeQueue<object> m_queue = new LockFreeQueue<object>();
        private volatile int m_minSize = 25;
        private volatile int m_hardReferences = 0;
        private volatile int m_obtainedReferenceCount;
        private readonly Func<T> m_createObj;
        public int HardReferenceCount
        {
            get
            {
                return this.m_hardReferences;
            }
        }
        public int MinimumSize
        {
            get
            {
                return this.m_minSize;
            }
            set
            {
                this.m_minSize = value;
            }
        }
        public int AvailableCount
        {
            get
            {
                return this.m_queue.Count;
            }
        }
        public int ObtainedCount
        {
            get
            {
                return this.m_obtainedReferenceCount;
            }
        }
        public ObjectPoolInfo Info
        {
            get
            {
                ObjectPoolInfo result;
                result.HardReferences = this.m_hardReferences;
                result.WeakReferences = this.m_queue.Count - this.m_hardReferences;
                return result;
            }
        }
        public bool IsBalanced
        {
            get
            {
                return this.m_isBalanced;
            }
            set
            {
                this.m_isBalanced = value;
            }
        }

        public ObjectPool(Func<T> func)
            : this(func, false)
        {
        }

        public ObjectPool(Func<T> func, bool isBalanced)
        {
            this.IsBalanced = isBalanced;
            this.m_createObj = func;
        }

        public void Recycle(T obj)
        {
            if (obj is IPooledObject)
            {
                ((IPooledObject)((object)obj)).Cleanup();
            }
            if (this.m_hardReferences >= this.m_minSize)
            {
                this.m_queue.Enqueue(new WeakReference(obj));
            }
            else
            {
                this.m_queue.Enqueue(obj);
                Interlocked.Increment(ref this.m_hardReferences);
            }
            if (this.m_isBalanced)
            {
                this.OnRecycle();
            }
        }

        public void Recycle(object obj)
        {
            if (obj is T)
            {
                if (obj is IPooledObject)
                {
                    ((IPooledObject)obj).Cleanup();
                }
                if (this.m_hardReferences >= this.m_minSize)
                {
                    this.m_queue.Enqueue(new WeakReference(obj));
                }
                else
                {
                    this.m_queue.Enqueue(obj);
                    Interlocked.Increment(ref this.m_hardReferences);
                }
                if (this.m_isBalanced)
                {
                    this.OnRecycle();
                }
            }
        }

        private void OnRecycle()
        {
            if (Interlocked.Decrement(ref this.m_obtainedReferenceCount) < 0)
            {
                throw new InvalidOperationException("Objects in Pool have been recycled too often: " + this);
            }
        }

        public T Obtain()
        {
            if (this.m_isBalanced)
            {
                Interlocked.Increment(ref this.m_obtainedReferenceCount);
            }
            object obj;
            T result;
            while (this.m_queue.TryDequeue(out obj))
            {
                if (obj is WeakReference)
                {
                    object target = ((WeakReference)obj).Target;
                    if (target == null)
                    {
                        continue;
                    }
                    result = (target as T);
                }
                else
                {
                    Interlocked.Decrement(ref this.m_hardReferences);
                    result = (obj as T);
                }
                return result;
            }
            result = this.m_createObj();
            return result;
        }

        public object ObtainObj()
        {
            if (this.m_isBalanced)
            {
                Interlocked.Increment(ref this.m_obtainedReferenceCount);
            }
            object obj;
            object result;
            while (this.m_queue.TryDequeue(out obj))
            {
                WeakReference weakReference = obj as WeakReference;
                if (weakReference != null)
                {
                    object target = weakReference.Target;
                    if (target == null)
                    {
                        continue;
                    }
                    result = target;
                }
                else
                {
                    Interlocked.Decrement(ref this.m_hardReferences);
                    result = obj;
                }
                return result;
            }
            result = this.m_createObj();
            return result;
        }

        public override string ToString()
        {
            return base.GetType().Name + " for " + typeof(T).FullName;
        }
    }
}
