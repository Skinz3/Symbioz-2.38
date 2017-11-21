using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SSync
{
    public class SSyncServer
    {
        public delegate void OnSocketAcceptedDel(Socket socket);

        public delegate void OnServerFailedToStartDel(Exception ex);

        /// <summary>
        /// Called when a client connect to the server on the EndPoint
        /// </summary>
        public event OnSocketAcceptedDel OnClientConnected;
        /// <summary>
        /// Called when server failed to start on the EndPoint
        /// </summary>
        public event OnServerFailedToStartDel OnServerFailedToStart;
        /// <summary>
        /// Called When server is started
        /// </summary>
        public event Action OnServerStarted;

        private Socket m_Listen_Socket { get; set; }

        public IPEndPoint EndPoint { get; set; }

        public SSyncServer(string ip,int port)
        {
            EndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            m_Listen_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        void OnListenSucces()
        {
            if (OnServerStarted != null)
                OnServerStarted();
        }
        /// <summary>
        /// Try to start the server and call OnListenFailed() its not possible
        /// </summary>
        public void Start()
        {
            try
            {
                m_Listen_Socket.Bind(EndPoint);
            }
            catch (Exception ex)
            {
                OnListenFailed(ex);
                return;
            }
            m_Listen_Socket.Listen(100); 
            StartAccept(null);
            OnListenSucces();
        }
        void OnListenFailed(Exception ex)
        {
            if (OnServerFailedToStart != null)
                OnServerFailedToStart(ex);
        }
        protected void StartAccept(SocketAsyncEventArgs args)
        {
            if (args == null)
            {
                args = new SocketAsyncEventArgs();
                args.Completed += AcceptEventCompleted;
            }
            else
            {
                args.AcceptSocket = null;
            }

            bool willRaiseEvent = m_Listen_Socket.AcceptAsync(args);
            if (!willRaiseEvent)
            {
                ProcessAccept(args);
            }
        }
        private void AcceptEventCompleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }
        /// <summary>
        /// Stop the server
        /// </summary>
        public void Stop()
        {
            m_Listen_Socket.Shutdown(SocketShutdown.Both);
        }
        void ProcessAccept(SocketAsyncEventArgs args)
        {
            if (OnClientConnected != null)
                OnClientConnected(args.AcceptSocket);
            StartAccept(args); 
        }
    }
}
