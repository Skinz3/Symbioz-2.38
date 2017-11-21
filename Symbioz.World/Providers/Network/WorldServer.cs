using SSync;
using SSync.Messages;
using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Network
{
    public class WorldServer : Singleton<WorldServer>
    {
        static object m_locker = new object();

        static Logger logger = new Logger();

        private ServerStatusEnum ServerStatus = ServerStatusEnum.ONLINE;

        private List<WorldClient> m_clients = new List<WorldClient>();

        public int MaxClientsCount
        {
            get;
            private set;
        }

        public int ClientsCount
        {
            get
            {
                return m_clients.Count;
            }
        }

        public bool IsStatus(ServerStatusEnum status)
        {
            return ServerStatus == status;
        }

        public SSyncServer Server
        {
            get;
            set;
        }

        public WorldServer()
        {
            this.Server = new SSyncServer(WorldConfiguration.Instance.Host, WorldConfiguration.Instance.Port);
            this.Server.OnServerStarted += Server_OnServerStarted;
            this.Server.OnServerFailedToStart += Server_OnServerFailedToStart;
            this.Server.OnClientConnected += Server_OnSocketAccepted;
        }

        void Server_OnSocketAccepted(Socket socket)
        {
            if (socket.RemoteEndPoint != null)
            {
                logger.White("New client connected");
                new WorldClient(socket);
            }
            else
            {
                logger.Error("A world socket try to connect without endpoint??? is it spoofing?");
            }
        }

        void Server_OnServerFailedToStart(Exception ex)
        {

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
            lock (m_locker)
                GetOnlineClients().SendAll(message);
        }
        public void AddClient(WorldClient client)
        {
            m_clients.Add(client);

            if (ClientsCount > MaxClientsCount)
            {
                MaxClientsCount = ClientsCount;
            }
        }
        public void RemoveClient(WorldClient client)
        {
            lock (m_locker)
            {
                m_clients.Remove(client);
                logger.White("Client disconnected");
            }
        }
        public List<WorldClient> GetClients()
        {
            lock (m_locker)
            {
                return m_clients;
            }
        }
        public bool Disconnect(int accountId)
        {
            lock (m_locker)
            {
                var client = m_clients.Find(x => x.Account != null && x.Account.Id == accountId);
                if (client != null)
                {
                    client.Disconnect();
                    return true;
                }
                else
                    return false;
            }
        }
        public List<WorldClient> GetOnlineClients()
        {
            lock (m_locker)
            {
                return m_clients.FindAll(x => x.Character != null);
            }
        }
        public WorldClient GetOnlineClient(int accountId)
        {
            lock (m_locker)
            {
                return GetOnlineClients().Find(x => x.Account != null && x.Account.Id == accountId);
            }
        }
        public WorldClient GetOnlineClient(string name)
        {
            lock (m_locker)
            {
                return GetOnlineClients().Find(x => x.Character.Name == name);
            }
        }
        public WorldClient GetOnlineClient(long id)
        {
            lock (m_locker)
            {
                return GetOnlineClients().Find(x => x.Character.Id == id);
            }
        }
        public bool IsOnline(long id)
        {
            return GetOnlineClient(id) != null;
        }
        public void SendOnSubarea(Message message, ushort subAreaId)
        {
            foreach (var client in GetOnlineClients().FindAll(x => x.Character.SubareaId == subAreaId))
            {
                client.Send(message);
            }
        }
        public void OnClients(Action<WorldClient> action)
        {
            foreach (var client in GetOnlineClients())
            {
                action(client);
            }
        }
        public void SetServerStatus(ServerStatusEnum status)
        {
            ServerStatus = status;
            TransitionServerManager.Instance.AuthServer.SendUnique(new SetServerStatusMessage(ServerStatus));

            switch (status)
            {
                case ServerStatusEnum.STATUS_UNKNOWN:
                    break;
                case ServerStatusEnum.OFFLINE:
                    DisconnectAll();
                    break;
                case ServerStatusEnum.STARTING:
                    break;
                case ServerStatusEnum.ONLINE:
                    break;
                case ServerStatusEnum.NOJOIN:
                    break;
                case ServerStatusEnum.SAVING:
                    break;
                case ServerStatusEnum.STOPING:
                    DisconnectAll();
                    break;
                case ServerStatusEnum.FULL:
                    break;
                default:
                    break;
            }
        }
        public void DisconnectAll()
        {
            for (int i = 0; i < m_clients.Count; i++)
            {
                m_clients[i].Disconnect();
            }
        }
    }
}
