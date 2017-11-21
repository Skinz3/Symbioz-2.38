using SSync.Transition;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Network
{
    public class AuthTransitionClient : TransitionClient
    {
        public AuthTransitionClient()
        {

        }
        public AuthTransitionClient(Socket socket)
            : base(socket)
        {

        }
        public override void OnClosed()
        {
            if (TransitionServerManager.Instance.IsConnected)
            {
                TransitionServerManager.Instance.OnConnectionToAuthLost();
                base.OnClosed();
            }
        }
        public override void OnFailToConnect(Exception ex)
        {
            TransitionServerManager.Instance.OnFailedToConnectAuth();
            base.OnFailToConnect(ex);
        }
        public override void OnConnected()
        {
            TransitionServerManager.Instance.OnConnectedToAuth();
            base.OnConnected();
        }

    }
}
