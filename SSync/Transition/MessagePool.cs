using SSync;
using SSync.Messages;
using SSync.Transition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSync.Transition
{
    public class MessagePool
    {
        private static List<IMessageRequest> m_requests = new List<IMessageRequest>();

        public static void HandleRequest(TransitionClient client, TransitionMessage message)
        {
            SSyncCore.HandleMessage(message, client, true);
        }
        public static void HandleAnswer(TransitionClient client, TransitionMessage message)
        {
            lock (m_requests)
            {
                var request = m_requests.FirstOrDefault(x => x.Guid == message.Guid);
                request.ProcessMessage(message);
                m_requests.Remove(request);
            }
        }
        public static void SendRequest<T>(TransitionClient client, TransitionMessage message, RequestCallbackDelegate<T> requestCallback, RequestCallbackErrorDelegate errorCallback = null) where T : TransitionMessage
        {
            lock (m_requests)
            {
                var messageRequest = new MessageRequest<T>(requestCallback, Guid.NewGuid(), errorCallback);
                m_requests.Add(messageRequest);
                client.Send(messageRequest.Guid, message, true);
            }
        }
    }
}
