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
using System.Threading.Tasks;

namespace SSync
{
    public class SSyncClient : Client
    {
        static Logger logger = new Logger();

        public event Action<Message> OnMessageHandleFailed;

        public SSyncClient()
            : base()
        {

        }
        public override void OnDataArrival(byte[] buffer)
        {
            Message message = SSyncCore.BuildMessage(buffer);
            if (!SSyncCore.HandleMessage(message, this))
            {
                if (OnMessageHandleFailed != null)
                    OnMessageHandleFailed(message);
            }
        }
        public SSyncClient(Socket sock)
            : base(sock)
        {

        }
        public void Send(Message message)
        {
            if (Socket != null && Socket.Connected)
            {
                CustomDataWriter writer = new CustomDataWriter();
                message.Pack(writer);
                var packet = writer.Data;
                this.Send(packet);
                if (SSyncCore.ShowProtocolMessage)
                    logger.DarkGray(string.Format("Send {0}", message.ToString()));
            }

        }
    }
}
