using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.SqlSync.Attributes;
using Symbioz.Tools.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.D2OTypes
{
    public class D2oTypeManager
    {
        static Logger logger = new Logger();

        static List<Type> D2oTypes = new List<Type>();

        [StartupInvoke("D2oTypeManager", StartupInvokePriority.First)]
        public static void Initialize()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var attribute = type.GetCustomAttribute<D2oTypeAttribute>();

                if (attribute != null)
                {
                    D2oTypes.Add(type);
                }
            }
        }
        public static Type GetType(DataClass obj)
        {
            return D2oTypes.Find(x => x.GetCustomAttribute<D2oTypeAttribute>().Type == obj.Name);
        }
        public static object Handle(DataClass obj)
        {

            Type type = D2oTypes.Find(x => x.GetCustomAttribute<D2oTypeAttribute>().Type == obj.Name);

            if (type != null)
            {
                var instance = Activator.CreateInstance(type);

                foreach (var property in type.GetProperties())
                {
                    var attribute = property.GetCustomAttribute<D2OFieldAttribute>();

                    if (attribute != null)
                    {
                        if (!obj.Fields.ContainsKey(attribute.FieldName))
                        {
                            logger.Color2(obj.Name + " dosent contain field " + attribute.FieldName+" here are types fields:");

                            foreach (var field in obj.Fields)
                            {
                                logger.Color1("-" + field.Key);
                            }
                            continue;
                        }
                        var d2oValue = obj.Fields[attribute.FieldName];

                        if (d2oValue is string)
                        {
                            d2oValue = d2oValue.ToString().Replace(',', '#');
                        }

                        property.SetValue(instance, Convert.ChangeType(d2oValue, property.PropertyType));
                    }
                }
                return instance;
            }
            else
            {
                logger.White("Type " + obj.Name + " not handled...");
                return "Undefined";
            }
        }
    }
}
