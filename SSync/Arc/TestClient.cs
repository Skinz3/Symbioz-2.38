using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SSync.Arc
{
    public class TestClient : AbstractClient
    {
        public TestClient(Socket sock)
            : base(sock)
        {
            m_buffer = new byte[BufferLenght];
            Socket.BeginReceive(m_buffer, 0, BufferLenght, SocketFlags.None, OnReceived, null);
        }
        public TestClient()
        {

        }
        public override void OnClosed()
        {

        }

        public override void OnConnected()
        {
            throw new NotImplementedException();
        }

        public override void OnDataArrival(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public override void OnFailToConnect(Exception ex)
        {
            throw new NotImplementedException();
        }

        public override void Send(byte[] buffer)
        {
            Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSended, null);
        }
        public async void OnSended(IAsyncResult result)
        {

        }
        public override void Connect(string host, int port)
        {
            Socket.BeginConnect(EndPoint, new AsyncCallback(OnConnectedAsync), Socket);

        }
        byte[] m_buffer;
        public int BufferLenght = 8192;
        public void OnConnectedAsync(IAsyncResult result)
        {

        }
        public void OnReceived(IAsyncResult result)
        {
            Socket.EndReceive(result);
            //if (m_buffer[0] == 0)
            //{
            //    Disconnect();
            //    return;
            //}
            OnDataArrival(m_buffer);
            Socket.BeginReceive(m_buffer, 0, BufferLenght, SocketFlags.None, OnReceived, null);
        }
        public override void Disconnect()
        {
            Socket.Shutdown(SocketShutdown.Both);
            Socket.Close();
            OnClosed();
        }
    }
}
