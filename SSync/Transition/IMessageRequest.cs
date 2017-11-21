using SSync.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SSync.Transition
{
    public interface IMessageRequest
    {
        Guid Guid
        {
            get;
            set;
        }
        Timer TimeoutTimer
        {
            get;
            set;
        }

        void ProcessMessage(TransitionMessage  message);
    }
}
