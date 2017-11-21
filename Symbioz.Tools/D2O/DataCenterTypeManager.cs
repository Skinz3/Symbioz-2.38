using System;
using System.Collections.Generic;
using System.Reflection;

namespace Symbioz.Tools.D2O
{
    class DataCenterTypeManager
    {
        #region Attributs
        private static readonly Dictionary<string, Type> m_Types = new Dictionary<string, Type>();
        private static readonly Dictionary<string, Func<object>> m_TypesConstructors = new Dictionary<string, Func<object>>();
        #endregion

        #region Constructeurs
        static DataCenterTypeManager()
        {
            Assembly asm = Assembly.GetAssembly(typeof(DataCenterTypeManager));

            foreach (Type type in asm.GetTypes())
            {
                if (type.Namespace == null || !type.Namespace.StartsWith(typeof(DataCenterTypeManager).Namespace))
                    continue;

                if ((type.Name == "IDataCenter") || (type.Name == "DataCenterTypeManager"))
                    continue;

                m_Types.Add(type.Name, type);

                ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);

                if (ctor == null)
                    throw new Exception(string.Format("'{0}' doesn't implemented a parameterless constructor", type));

                m_TypesConstructors.Add(type.Name, ctor.CreateDelegate<Func<object>>());
            }
        }
        #endregion

        #region Méthodes publiques
        public static T GetInstance<T>(string name) where T : class
        {
            if (!m_Types.ContainsKey(name))
            {
                throw new Exception("Type " + name + " doesn't exist");
            }

            return m_TypesConstructors[name]() as T;
        }
        #endregion
    }
}