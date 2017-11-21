using SSync.Transition;
using Symbioz.Auth.Transition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Auth.Network
{
    public class WorldTransitionClient : TransitionClient
    {
        public WorldTransitionClient(Socket socket)
            : base(socket)
        {

        }
        public WorldTransitionClient()
        {

        }
        public override void OnClosed()
        {
            TransitionServer.Instance.RemoveWorld(this);
            base.OnClosed();
        }
    }
}
