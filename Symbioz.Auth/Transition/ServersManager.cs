using SSync.Transition;
using Symbioz.Auth.Records;
using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Network;
using Symbioz.Network.Servers;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Messages;
using Symbioz.Protocol.Selfmade.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Auth.Transition
{
    public class ServersManager : Singleton<ServersManager>
    {
        static Logger logger = new Logger();

        public void RegisterWorld(ushort serverId, string name, sbyte type, string host, int port)
        {
            ServerRecord world = ServerRecord.GetWorldServer(serverId);

            if (world != null)
            {
                if (world.Host != host || world.Port != port || world.Name != name || world.Type != type)
                {
                    world.Host = host;
                    world.Port = port;
                    world.Name = name;
                    world.Type = type;

                    logger.White("Updating server informations...(" + name + ")");
                    world.UpdateInstantElement();
                }

                SetServerStatus(serverId, ServerStatusEnum.STARTING);
                logger.White(string.Format("Server Registred : {0} ({1}:{2})", world.Name, world.Host, world.Port));
            }
            else
            {
                var newWorld = new ServerRecord(serverId, name, type, host, port);

                if (ServerRecord.CanBeAdded(newWorld))
                {
                    ServerRecord.AddWorldServer(newWorld);
                    SetServerStatus(serverId, ServerStatusEnum.STARTING);
                    logger.White(string.Format("New server added : {0} ({1}:{2})", newWorld.Name, newWorld.Host, newWorld.Port));
                }
                else
                {
                    logger.Alert("A new server try to join Auth but he is not allowed (Id or Ip and Port are reserved)");
                }
            }
        }

        public void UnregisterWorld(ushort serverId)
        {
            ServerRecord world = ServerRecord.GetWorldServer(serverId);

            if (world != null)
            {
                SetServerStatus(serverId, ServerStatusEnum.OFFLINE);
                logger.White("Server Unregistred (" + world.Name + ")");
            }
        }

        public void SetServerStatus(ushort serverId, ServerStatusEnum state)
        {
            ServerRecord.SetServerStatus(serverId, state);

            foreach (var client in AuthServer.Instance.GetClients())
            {
                client.SendServerList();
            }
        }
        public bool DisconnectClient(AuthClient newClient)
        {
            bool succes = false;
            AuthClient client = AuthServer.Instance.GetClients().Find(x => x.Account != null && x.Account.Id == newClient.Account.Id);

            if (client != null)
            {
                client.Disconnect();
                succes = true;
            }
            else
            {
                if (newClient.Account.LastSelectedServerId != 0)
                {
                    TransitionClient server = TransitionServer.Instance.GetServerClient(newClient.Account.LastSelectedServerId);
                    var serverData = ServerRecord.GetWorldServer(newClient.Account.LastSelectedServerId);
                    if (server != null && server.IsConnected && serverData != null && serverData.Status != ServerStatusEnum.STARTING) // Online
                    {
                        MessagePool.SendRequest<DisconnectClientResultMessage>(server, new DisconnectClientRequestMessage
                        {
                            AccountId = newClient.Account.Id,
                        },
                        delegate(DisconnectClientResultMessage message)
                        {
                            succes = message.IsSucces;
                        },
                        delegate()
                        {
                            OnTransitionFailed(newClient);
                        });
                    }
                    else
                    {
                        succes = false;
                    }
                }

            }
            return succes;
        }
        public void OnTransitionFailed(AuthClient client)
        {
            client.Send(new IdentificationFailedMessage((sbyte)IdentificationFailureReasonEnum.OTP_TIMEOUT));
        }
        public void ResetAuthDatabase(ushort serverId)
        {
            ServerCharacterRecord.ResetServer(serverId);
        }


    }
}
