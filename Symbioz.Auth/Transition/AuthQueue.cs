
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core.DesignPattern;
using Symbioz.Network;
using Symbioz.Core;
using Symbioz.Protocol.Messages;
using Symbioz.Auth.Handlers;
using Symbioz.Protocol.Enums;

namespace Symbioz.Auth.Transition
{
    /// <summary>
    /// From Stump
    /// </summary>
    public class AuthQueue : Singleton<AuthQueue>
    {
        public static int QueueDelay = 3000;

        private ConcurrentQueue<AuthClient> m_clients = new ConcurrentQueue<AuthClient>();
        private Logger logger = new Logger();
        private bool m_running;

        public bool IsInQueue(AuthClient client)
        {
            return m_clients.Contains(client);
        }

        public void AddClient(AuthClient client)
        {
            m_clients.Enqueue(client);
            if (!m_running)
            {
                m_running = true;
                Authentificate();
                Task.Factory.StartNewDelayed(QueueDelay, () =>
                {
                    try
                    {
                        Authentificate();
                    }
                    catch (Exception ex)
                    {
                        logger.Alert(string.Format("errror while Authentificate some clients\n{1}", ex.Message));
                    }
                });
            }
        }

        private void Authentificate()
        {
            AuthClient client;

            if (!m_running || !m_clients.TryDequeue(out client))
            {
                return;
            }

            while (client == null || (client != null && !client.IsConnected))
            {
                if (m_clients.Count == 0)
                {
                    break;
                }

                m_clients.TryDequeue(out client);
                if (client != null && !client.IsConnected)
                {
                    client = null;
                }
            }

            if (client == null || client.IdentificationMessage == null)
            {
                if (m_clients.Count == 0)
                {
                    m_running = false;
                }
                if (m_running)
                {
                    Task.Factory.StartNewDelayed(QueueDelay, Authentificate);
                }
            }

            Task.Factory.StartNew(RefreshQueue);

            try
            {
                Indentificate(client);
            }
            catch (Exception ex)
            {
                logger.Alert(string.Format("errror while Indentificate {0} :\n{1}", client, ex.ToString()));
            }

            if (m_clients.Count == 0)
            {
                m_running = false;
            }
            if (m_running)
            {
                Task.Factory.StartNewDelayed(QueueDelay, Authentificate);
            }
        }

        public void Indentificate(AuthClient client)
        {
            try
            {
                ConnectionHandler.ProcessIdentification(client);
            }
            catch (Exception ex)
            {
                client.Send(new IdentificationFailedMessage((sbyte)(IdentificationFailureReasonEnum.SERVICE_UNAVAILABLE)));
                logger.Alert(ex.ToString());
            }
        }

        private void RefreshQueue()
        {
            foreach (var client in m_clients)
            {
                client.Send(new LoginQueueStatusMessage(GetPosition(client), GetCount()));
            }
        }

        private ushort GetPosition(AuthClient client)
        {
            return (ushort)m_clients.ToList().IndexOf(client);
        }

        private ushort GetCount()
        {
            return (ushort)m_clients.Count;
        }
    }
}
