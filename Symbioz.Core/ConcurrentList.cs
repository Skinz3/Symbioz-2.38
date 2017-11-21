using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core
{
    public class ConcurrentList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
    {
        private readonly List<T> m_underlyingList = new List<T>();
        private readonly object m_syncRoot = new object();
        private readonly ConcurrentQueue<T> m_underlyingQueue;
        private bool m_requiresSync;
        private bool m_isDirty;
        public T this[int index]
        {
            get
            {
                T result;
                lock (this.m_syncRoot)
                {
                    this.UpdateLists();
                    result = this.m_underlyingList[index];
                }
                return result;
            }
            set
            {
                lock (this.m_syncRoot)
                {
                    this.UpdateLists();
                    this.m_underlyingList[index] = value;
                }
            }
        }
        object IList.this[int index]
        {
            get
            {
                return ((IList<T>)this)[index];
            }
            set
            {
                ((IList<T>)this)[index] = (T)((object)value);
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }
        public int Count
        {
            get
            {
                int count;
                lock (this.m_syncRoot)
                {
                    this.UpdateLists();
                    count = this.m_underlyingList.Count;
                }
                return count;
            }
        }
        public object SyncRoot
        {
            get
            {
                return this.m_syncRoot;
            }
        }
        public bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        public ConcurrentList()
        {
            this.m_underlyingQueue = new ConcurrentQueue<T>();
        }

        public ConcurrentList(IEnumerable<T> items)
        {
            this.m_underlyingQueue = new ConcurrentQueue<T>(items);
        }

        private void UpdateLists()
        {
            if (this.m_isDirty)
            {
                lock (this.m_syncRoot)
                {
                    this.m_requiresSync = true;
                    T item;
                    while (this.m_underlyingQueue.TryDequeue(out item))
                    {
                        this.m_underlyingList.Add(item);
                    }
                    this.m_requiresSync = false;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerator<T> result;
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                result = this.m_underlyingList.GetEnumerator();
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            if (this.m_requiresSync)
            {
                lock (this.m_syncRoot)
                {
                    this.m_underlyingQueue.Enqueue(item);
                    this.m_isDirty = true;
                    return;
                }
            }
            this.m_underlyingQueue.Enqueue(item);
        }

        public int Add(object value)
        {
            if (this.m_requiresSync)
            {
                lock (this.m_syncRoot)
                {
                    this.m_underlyingQueue.Enqueue((T)((object)value));
                    goto IL_4F;
                }
            }
            this.m_underlyingQueue.Enqueue((T)((object)value));
        IL_4F:
            this.m_isDirty = true;
            int result;
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                result = this.m_underlyingList.IndexOf((T)((object)value));
            }
            return result;
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (this.m_requiresSync)
            {
                lock (this.m_syncRoot)
                {
                    foreach (T current in items)
                    {
                        this.m_underlyingQueue.Enqueue(current);
                    }
                    this.m_isDirty = true;
                    return;
                }
            }
            foreach (T current in items)
            {
                this.m_underlyingQueue.Enqueue(current);
            }
        }

        public bool Contains(object value)
        {
            bool result;
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                result = this.m_underlyingList.Contains((T)((object)value));
            }
            return result;
        }

        public int IndexOf(object value)
        {
            int result;
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                result = this.m_underlyingList.IndexOf((T)((object)value));
            }
            return result;
        }

        public void Insert(int index, object value)
        {
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                this.m_underlyingList.Insert(index, (T)((object)value));
            }
        }

        public void Remove(object value)
        {
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                this.m_underlyingList.Remove((T)((object)value));
            }
        }

        public void RemoveAt(int index)
        {
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                this.m_underlyingList.RemoveAt(index);
            }
        }

        public void Clear()
        {
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                this.m_underlyingList.Clear();
            }
        }

        public bool Contains(T item)
        {
            bool result;
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                result = this.m_underlyingList.Contains(item);
            }
            return result;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                this.m_underlyingList.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(T item)
        {
            bool result;
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                result = this.m_underlyingList.Remove(item);
            }
            return result;
        }

        public void CopyTo(Array array, int index)
        {
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                this.m_underlyingList.CopyTo((T[])array, index);
            }
        }

        public int IndexOf(T item)
        {
            int result;
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                result = this.m_underlyingList.IndexOf(item);
            }
            return result;
        }

        public void Insert(int index, T item)
        {
            lock (this.m_syncRoot)
            {
                this.UpdateLists();
                this.m_underlyingList.Insert(index, item);
            }
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return new ReadOnlyCollection<T>(this.m_underlyingList);
        }
    }
}
