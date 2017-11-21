using SSync.Messages;
using SSync.Transition;
using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade;
using Symbioz.Protocol.Selfmade.Messages;
using Symbioz.World.Providers.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symbioz.World.Network
{
    public class TransitionServerManager : Singleton<TransitionServerManager>
    {
        static Logger logger = new Logger();

        public TransitionClient AuthServer;

        public bool IsConnected = false;

        public void TryToJoinAuthServer()
        {
            AuthServer = new AuthTransitionClient();
            TryConnect();
        }

        public void AllowConnections()
        {
            AuthServer.SendUnique(new SetServerStatusMessage(ServerStatusEnum.ONLINE));
        }


        private void TryConnect()
        {
            AuthServer.Connect(WorldConfiguration.Instance.TransitionHost, WorldConfiguration.Instance.TransitionPort);
        }
        public void OnFailedToConnectAuth()
        {
            logger.Gray("Unable to connect to AuthServer.. Trying to reconnect in 3s");
            Thread.Sleep(3000);
            TryToJoinAuthServer();
        }

        public void OnConnectionToAuthLost()
        {
            logger.Error("Connection to AuthServer was lost.. Server is shutting down.");
            SaveTask.Save();
            WorldServer.Instance.DisconnectAll();
            Thread.Sleep(3000);
            Environment.Exit(0);
        }

        public void OnConnectedToAuth()
        {
            logger.White("Connected to AuthServer");
            this.IsConnected = true;
            string host = WorldConfiguration.Instance.UseCustomHost ? WorldConfiguration.Instance.CustomHost : WorldConfiguration.Instance.Host;

            AuthServer.SendUnique(new WorldRegistrationRequestMessage((ushort)WorldConfiguration.Instance.ServerId,
            WorldConfiguration.Instance.ServerName, WorldConfiguration.Instance.ServerType,
            host, WorldConfiguration.Instance.Port));
        }




    }
}
