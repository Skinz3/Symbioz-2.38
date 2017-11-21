using SSync.Messages;
using Symbioz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SSync.Transition
{
    public class MessageRequest<T> : IMessageRequest where T : TransitionMessage
    {
        public const int TIMEOUT = 10000;

        Logger logger = new Logger();

        public MessageRequest(RequestCallbackDelegate<T> callback, Guid Guid, RequestCallbackErrorDelegate errorCallback)
        {
            this.Callback = callback;
            this.ErrorCallback = errorCallback;
            this.Guid = Guid;
            this.TimeoutTimer = new Timer(TIMEOUT);
            this.TimeoutTimer.Elapsed += TimeoutTimer_Elapsed;
            this.TimeoutTimer.Start();
        }

        void TimeoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            logger.Alert("Transition Message " + typeof(T).Name + " timeout..");
            TimeoutTimer.Stop();

            if (ErrorCallback != null)
                ErrorCallback();
        }
        public RequestCallbackDelegate<T> Callback
        {
            get;
            set;
        }
        public RequestCallbackErrorDelegate ErrorCallback
        {
            get;
            set;
        }
        public Guid Guid
        {
            get;
            set;
        }
        public Timer TimeoutTimer
        {
            get;
            set;
        }
        public void ProcessMessage(TransitionMessage message)
        {
            this.TimeoutTimer.Stop();
            if (message is T)
            {
                this.Callback(message as T);
            }

        }
    }
}
