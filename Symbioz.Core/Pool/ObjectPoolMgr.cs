using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core.Pool
{
    public static class ObjectPoolMgr
    {
        private static Dictionary<long, IObjectPool> m_pools = new Dictionary<long, IObjectPool>();

        public static bool ContainsType<T>()
        {
            return ObjectPoolMgr.m_pools.ContainsKey(ObjectPoolMgr.GetTypePointer<T>());
        }

        public static bool ContainsType(Type t)
        {
            return ObjectPoolMgr.m_pools.ContainsKey(t.TypeHandle.Value.ToInt64());
        }

        public static bool RegisterType<T>(Func<T> func) where T : class
        {
            long typePointer = ObjectPoolMgr.GetTypePointer<T>();
            bool result;
            lock (typeof(ObjectPoolMgr))
            {
                if (!ObjectPoolMgr.m_pools.ContainsKey(typePointer))
                {
                    ObjectPoolMgr.m_pools.Add(typePointer, new ObjectPool<T>(func));
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }

        public static void SetMinimumSize<T>(int minSize) where T : class
        {
            long typePointer = ObjectPoolMgr.GetTypePointer<T>();
            if (ObjectPoolMgr.m_pools.ContainsKey(typePointer))
            {
                ObjectPool<T> objectPool = (ObjectPool<T>)ObjectPoolMgr.m_pools[typePointer];
                objectPool.MinimumSize = minSize;
            }
        }

        public static void ReleaseObject<T>(T obj) where T : class
        {
            long typePointer = ObjectPoolMgr.GetTypePointer<T>();
            IObjectPool objectPool;
            if (ObjectPoolMgr.m_pools.TryGetValue(typePointer, out objectPool))
            {
                objectPool.Recycle(obj);
            }
        }

        public static T ObtainObject<T>() where T : class
        {
            long typePointer = ObjectPoolMgr.GetTypePointer<T>();
            IObjectPool objectPool;
            T result;
            if (ObjectPoolMgr.m_pools.TryGetValue(typePointer, out objectPool))
            {
                result = ((ObjectPool<T>)objectPool).Obtain();
            }
            else
            {
                result = default(T);
            }
            return result;
        }

        public static ObjectPoolInfo GetPoolInfo<T>() where T : class
        {
            long typePointer = ObjectPoolMgr.GetTypePointer<T>();
            IObjectPool objectPool;
            ObjectPoolInfo result;
            if (ObjectPoolMgr.m_pools.TryGetValue(typePointer, out objectPool))
            {
                result = ((ObjectPool<T>)objectPool).Info;
            }
            else
            {
                result = new ObjectPoolInfo(0, 0);
            }
            return result;
        }

        private static long GetTypePointer<T>()
        {
            return typeof(T).TypeHandle.Value.ToInt64();
        }
    }
}
