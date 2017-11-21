using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using System.Net.Sockets;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.Auth;
using SSync;
using SSync.Messages;

namespace Symbioz.Network.Servers
{
    public class AuthServer : Singleton<AuthServer>
    {
        static Logger logger = new Logger();

        private List<AuthClient> m_clients = new List<AuthClient>();

        public SSyncServer Server
        {
            get; private
            set;
        }
        public int ClientsCount
        {
            get
            {
                return m_clients.Count;
            }
        }
        public AuthServer()
        {

            this.Server = new SSyncServer(AuthConfiguration.Instance.Host, AuthConfiguration.Instance.Port);
            this.Server.OnServerStarted += Server_OnServerStarted;
            this.Server.OnServerFailedToStart += Server_OnServerFailedToStart;
            this.Server.OnClientConnected += Server_OnClientConnected;
        }

        void Server_OnClientConnected(Socket socket)
        {
            logger.White("New client connected");
            new AuthClient(socket);
        }

        void Server_OnServerFailedToStart(Exception ex)
        {
            logger.Alert("Unable to start AuthServer " + ex);
        }
        void Server_OnServerStarted()
        {
            logger.Gray("Server Started (" + Server.EndPoint.ToString() + ")");
        }

        public void Start()
        {
            Server.Start();
        }
        public void Send(Message message)
        {
            foreach (var client in m_clients)
            {
                client.Send(message);
            }
        }
        public void AddClient(AuthClient client)
        {
            m_clients.Add(client);
        }
        public void RemoveClient(AuthClient client)
        {
            m_clients.Remove(client);
            logger.White("Client disconnected");
        }
        public List<AuthClient> GetClients()
        {
            return m_clients;
        }
    }
}
