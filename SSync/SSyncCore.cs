using SSync.Arc;
using SSync.IO;
using SSync.Messages;
using SSync.Sockets;
using Symbioz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SSync
{
    /// <summary>
    /// SSync Library written by Skinz 2016 All Rights Reserved => Dofus 2.34
    /// This class is the core of SSync, it handle message & theirs handlers registration
    /// </summary>
        public class SSyncCore
        {
            static Logger logger = new Logger();
            /// <summary>
            /// Says if the protocol had been loaded or not
            /// </summary>
            public static bool Initialized = false;
            /// <summary>
            /// Message Handler Default Parameters
            /// </summary>
            private static readonly Type[] HandlerMethodParameterTypes = new Type[] { typeof(Message), typeof(AbstractClient) };
            /// <summary>
            /// Represents all the handlers methods linked to their messageIds.
            /// </summary>
            private static readonly Dictionary<uint, Delegate> Handlers = new Dictionary<uint, Delegate>();
            /// <summary>
            /// Represents all the messagesIds linked to their class type.
            /// </summary
            private static readonly Dictionary<ushort, Type> Messages = new Dictionary<ushort, Type>();
            /// <summary>
            /// Represents all the messagesIds linked to their constructor.
            /// </summary>
            private static readonly Dictionary<ushort, Func<Message>> Constructors = new Dictionary<ushort, Func<Message>>();
            
            public static bool ShowProtocolMessage;
            /// <param name="messagesAssembly"></param>
            /// <param name="handlersAssembly"></param>
            public static void Initialize(Assembly messagesAssembly, Assembly handlersAssembly, bool showProtocolMessages)
            {
                ShowProtocolMessage = showProtocolMessages;
                foreach (var type in messagesAssembly.GetTypes().Where(x => x.IsSubclassOf(typeof(Message))))
                {
                    FieldInfo field = type.GetField("Id");
                    if (field != null)
                    {
                        ushort num = (ushort)field.GetValue(type);
                        if (Messages.ContainsKey(num))
                        {
                            throw new AmbiguousMatchException(string.Format("MessageReceiver() => {0} item is already in the dictionary, old type is : {1}, new type is  {2}",
                                num, Messages[num], type));
                        }
                        Messages.Add(num, type);
                        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                        if (constructor == null)
                        {
                            throw new Exception(string.Format("'{0}' doesn't implemented a parameterless constructor", type));
                        }
                        Constructors.Add(num, constructor.CreateDelegate<Func<Message>>());
                    }
                }

                foreach (var item in handlersAssembly.GetTypes())
                {
                    foreach (var subItem in item.GetMethods())
                    {
                        var attribute = subItem.GetCustomAttribute(typeof(MessageHandlerAttribute));
                        if (attribute != null)
                        {
                            Type methodParameters = subItem.GetParameters()[0].ParameterType;
                            if (methodParameters.BaseType != null)
                            {
                                try
                                {
                                    Delegate target = subItem.CreateDelegate(HandlerMethodParameterTypes);
                                    FieldInfo field = methodParameters.GetField("Id");
                                    Handlers.Add((ushort)field.GetValue(null), target);
                                }
                                catch
                                {
                                    throw new Exception("Cannot register " + subItem.Name + " has message handler...");
                                }

                            }
                        }

                    }
                }
                Initialized = true;
                logger.Gray(Messages.Count + " Message(s) Loaded | " + Handlers.Count + " Handler(s) Loaded");
            }
            /// <summary>
            /// Unpack message
            /// </summary>
            /// <param name="id">Id of the message</param>
            /// <param name="reader">Reader with the message datas</param>
            /// <returns></returns>
            private static Message ConstructMessage(ushort id, ICustomDataInput reader)
            {
                if (!Messages.ContainsKey(id))
                {
                    return null;
                }
                Message message = Constructors[id]();
                if (message == null)
                {
                    return null;
                }
                message.Unpack(reader);
                return message;
            }
            /// <summary>
            /// Build a messagePart and call the ConstructMessage(); method.
            /// </summary>
            /// <param name="buffer">data received</param>
            /// <returns>Message of your protocol, builted</returns>
            public static Message BuildMessage(byte[] buffer)
            {
                var reader = new CustomDataReader(buffer);
                var messagePart = new MessagePart(false);

                if (messagePart.Build(reader))
                {
                    Message message;
                    try
                    {
                        message = ConstructMessage((ushort)messagePart.MessageId.Value, reader);
                        return message;
                    }
                    catch (Exception ex)
                    {
                        logger.Alert("Error while building Message :" + ex.Message);
                        return null;
                    }
                    finally
                    {
                        reader.Dispose();
                    }
                }
                else
                    return null;

            }
            /// <summary>
            /// Try to handle a message by finding an handler linked to this messae.
            /// </summary>
            /// <param name="message"></param>
            /// <param name="client"></param>
            /// <returns>True if the message is handled, False if its not</returns>
            public static bool HandleMessage(Message message, AbstractClient client, bool mute = false)
            {
                if (!Initialized)
                {
                    throw new LibraryNotLoadedException("SSync Library is not initialized, call the method SSyncCore.Initialize() before launch sockets");
                }

                if (message == null && !mute)
                {
                    logger.Color2("Cannot build datas from client " + client.Ip);
                    client.Disconnect();
                    return false;
                }

                var handler = Handlers.FirstOrDefault(x => x.Key == message.MessageId);

                if (handler.Value != null)
                {
                    {
                        if (ShowProtocolMessage && !mute)
                            logger.Gray("Receive " + message.ToString());
                        try
                        {

                            handler.Value.DynamicInvoke(null, message, client);
                            return true;

                        }
                        catch (Exception ex)
                        {
                            logger.Alert(string.Format("Unable to handle message {0} {1} : '{2}'", message.ToString(), handler.Value.Method.Name, ex.InnerException.ToString()));
                            return false;
                        }
                    }
                }
                else
                {
                    if (ShowProtocolMessage && !mute)
                        logger.White(string.Format("No Handler: ({0}) {1}", message.MessageId, message.ToString()));
                    return true;
                }
            }
        }
        /// <summary>
        /// Exception thrown when the SSync Library is not loaded
        /// </summary>
        public class LibraryNotLoadedException : Exception
        {
            public LibraryNotLoadedException() { }
            public LibraryNotLoadedException(string message) : base(message) { }
            public LibraryNotLoadedException(string message, Exception inner) : base(message, inner) { }
            protected LibraryNotLoadedException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }
        }
}
