using Symbioz.Core.DesignPattern.StartupEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Symbioz.Protocol.Selfmade.RawMessages;
using SSync.IO;
using Symbioz.Core;
using Symbioz.World.Network;

namespace Symbioz.Modules
{
    public class RawDataMessageHandlerAttribute : Attribute
    {

    }
    public class RawDataProtocolManager
    {
        static Logger logger = new Logger();

        static Dictionary<short, Type> Messages = new Dictionary<short, Type>();

        static Dictionary<short, MethodInfo> Handlers = new Dictionary<short, MethodInfo>();

        [StartupInvoke("RawDataProtocol", StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var type in Assembly.GetAssembly(typeof(RawMessage)).GetTypes())
            {
                if (type.BaseType == typeof(RawMessage))
                {
                    Messages.Add((short)type.GetField("Id").GetValue(null), type);
                }
            }
            foreach (var type in Assembly.GetAssembly(typeof(RawDataProtocolManager)).GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    var attribute = method.GetCustomAttribute<RawDataMessageHandlerAttribute>();

                    if (attribute != null)
                    {
                        short messageId = (short)method.GetParameters()[0].ParameterType.GetField("Id").GetValue(null);

                        Handlers.Add(messageId, method);
                    }
                }
            }
        }
        public static RawMessage Build(BigEndianReader reader)
        {
            try
            {
                short id = reader.ReadShort();
                var message = (RawMessage)Activator.CreateInstance(Messages[id]);
                message.Deserialize(reader);
                return message;
            }
            catch (Exception ex)
            {
                logger.Alert(ex.ToString());
                return null;
            }
        }
        public static void Handle(RawMessage message, WorldClient client)
        {
            try
            {
                Handlers[message.GetMessageId()].Invoke(null, new object[] { message, client });
            }
            catch (Exception ex)
            {
                logger.Alert(ex.ToString());
            }
        }

    }
}
