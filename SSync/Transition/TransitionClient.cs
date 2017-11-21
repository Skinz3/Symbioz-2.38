using SSync;
using SSync.Arc;
using SSync.IO;
using SSync.Messages;
using SSync.Sockets;
using Symbioz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSync.Transition
{
    public abstract class TransitionClient : Client
    {
        static Logger logger = new Logger();

        public ushort ServerId { get; set; }
        public TransitionClient(Socket socket)
            : base(socket)
        {


        }
        public override void OnDataArrival(byte[] buffer)
        {
            try
            {
                CustomDataReader reader = new CustomDataReader(buffer);

                bool isRequest = reader.ReadBoolean();
                int num = reader.ReadInt();
                var guid = new Guid(reader.ReadBytes(num));

                TransitionMessage message = SSyncCore.BuildMessage(reader.ReadBytes(reader.BytesAvailable)) as TransitionMessage;
                message.Guid = guid;

                if (SSyncCore.ShowProtocolMessage)
                    logger.Color2("Receive " + message.ToString());

                if (isRequest)
                    MessagePool.HandleRequest(this, message);
                else
                    MessagePool.HandleAnswer(this, message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

        }

        public TransitionClient()
        {

        }

        public void Send(Guid guid, TransitionMessage message, bool isRequest)
        {
            CustomDataWriter writer = new CustomDataWriter();

            writer.WriteBoolean(isRequest);
            byte[] guidDatas = guid.ToByteArray();
            writer.WriteInt(guidDatas.Length);
            writer.WriteBytes(guidDatas);

            message.Pack(writer);
            var packet = writer.Data;
            Send(packet);
            if (SSyncCore.ShowProtocolMessage)
                logger.Color2(string.Format("Send {0}", message.ToString()));

        }
        /// <summary>
        /// Envoit la réponse a une request 
        /// </summary>
        /// <param name="message"></param>
        public void SendReply(TransitionMessage message, Guid guid)
        {
            Send(guid, message, false);
        }
        /// <summary>
        /// Envoit un message simple sans handler 
        /// </summary>
        /// <param name="message"></param>
        public void SendUnique(TransitionMessage message)
        {
            Send(message.Guid, message, true);
        }
        /// <summary>
        /// Envoit une request avec un handler destinataire
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="requestCallback"></param>
        /// <param name="isRequest"></param>
        public void Send<T>(TransitionMessage message, RequestCallbackDelegate<T> requestCallback, RequestCallbackErrorDelegate errorCallback) where T : TransitionMessage
        {
            MessagePool.SendRequest<T>(this, message, requestCallback, errorCallback);
        }




    }
}
