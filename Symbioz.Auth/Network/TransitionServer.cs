using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Network;
using Symbioz.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SSync.Transition;
using SSync.Messages;
using SSync;
using Symbioz.Auth.Network;

namespace Symbioz.Auth.Transition
{
    public class TransitionServer : Singleton<TransitionServer>
    {
        private Logger logger = new Logger();

        private SSyncServer m_server { get; set; }

        private List<TransitionClient> m_servers = new List<TransitionClient>();

        void m_server_OnServerStarted()
        {
            logger.Gray("Server Started (" + m_server.EndPoint.ToString() + ")");
        }

        public void Start(string ip, int port)
        {
            m_server = new SSyncServer(ip, port);
            this.m_server.OnClientConnected += m_server_OnClientConnected;
            this.m_server.OnServerStarted += m_server_OnServerStarted;
            this.m_server.OnServerFailedToStart += m_server_OnServerFailedToStart;
            m_server.Start();
        }

        void m_server_OnClientConnected(Socket socket)
        {
            m_servers.Add(new WorldTransitionClient(socket));
            logger.White("Transition Client Connected");
        }

        void m_server_OnServerFailedToStart(Exception ex)
        {
            logger.Alert("Unable to start TransitionServer " + ex);
        }

        public void RemoveWorld(TransitionClient client)
        {
            m_servers.Remove(client);
            ServersManager.Instance.UnregisterWorld(client.ServerId);
        }
        public void SendAllUnique(TransitionMessage message)
        {
            foreach (var server in m_servers)
            {
                server.SendUnique(message);
            }
        }
        public TransitionClient GetServerClient(ushort id)
        {
            return m_servers.Find(x => x.ServerId == id);
        }
    }
}
