using System;                                   
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SSync.Arc
{
    public abstract class AbstractClient
    {
        public AbstractClient()
        {
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public AbstractClient(Socket sock)
        {
            this.Socket = sock;
        }
        public IPEndPoint EndPoint
        {
            get
            {
                return Socket.RemoteEndPoint as IPEndPoint;
            }
        }
        public string Ip
        {
            get
            {
                return EndPoint.Address.ToString();
            }
        }

        public abstract void OnClosed();

        public abstract void OnConnected();

        protected Socket Socket
        {
            get;
            set;
        }
        public bool IsConnected
        {
            get
            {
                return Socket.Connected;
            }
        }

        public abstract void OnDataArrival(byte[] buffer);

        public abstract void OnFailToConnect(Exception ex);

        public abstract void Send(byte[] buffer);

        public abstract void Connect(string host, int port);

        public abstract void Disconnect();
    }
}
