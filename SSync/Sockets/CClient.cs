using SSync.Arc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSync.Sockets
{
    public class Client : AbstractClient
    {
        public Client()
            : base()
        {
            _bufferSegment = Buffers.CheckOut();
            Receive();
        }
        public Client(Socket sock)
            : base(sock)
        {
            _bufferSegment = Buffers.CheckOut();
            Receive();
        }
        private uint _bytesReceived;

        private static readonly BufferManager Buffers = BufferManager.Default;

        private uint _bytesSent;

        public int sizeBuffer = 8192;

        private static long _totalBytesReceived;

        private static long _totalBytesSent;

        public static long TotalBytesSent
        {
            get { return _totalBytesSent; }
        }

        public static long TotalBytesReceived
        {
            get { return _totalBytesReceived; }
        }

        protected BufferSegment _bufferSegment;

        protected int _offset, _remainingLength;

        public override void OnClosed()
        {
            throw new NotImplementedException();
        }

        public override void OnConnected()
        {
            Receive();
            Thread.Sleep(100);
            OnConnected();
        }
        public void Connect(IPAddress addr, int port)
        {
            if (Socket != null)
            {
                if (Socket.Connected)
                {
                    Socket.Disconnect(true);
                }
                try
                {
                    Socket.Connect(addr, port);

                }
                catch (Exception ex)
                {
                    OnFailToConnect(ex);
                    return;
                }

                OnConnected();
            }
        }
        public IPEndPoint EndPoint
        {
            get
            {
                return Socket.RemoteEndPoint as IPEndPoint;
            }
        }

        public override string Ip
        {
            get
            {
                return EndPoint.Address + ":" + EndPoint.Port;

            }
        }

        public override void OnDataArrival(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public override void OnFailToConnect(Exception ex)
        {
            throw new NotImplementedException();
        }
        private void ReceiveAsyncComplete(object sender, SocketAsyncEventArgs args)
        {
            ProcessRecieve(args);
        }
        private void SendAsyncComplete(object sender, SocketAsyncEventArgs args)
        {
            args.Completed -= SendAsyncComplete;
            SocketHelpers.ReleaseSocketArg(args);
        }
        public override void Send(byte[] buffer)
        {
            if (Socket != null && Socket.Connected)
            {
                var args = SocketHelpers.AcquireSocketArg();
                if (args != null)
                {
                    args.Completed += SendAsyncComplete;
                    args.UserToken = this;
                    args.SetBuffer(buffer, 0, buffer
                        .Length);
                    Socket.SendAsync(args);

                    //  Logger.WriteMsg(string.Format("[Send] {0}", message.ToString()), ConsoleColor.DarkGray);
                    unchecked
                    {
                        _bytesSent += (uint)buffer.Length;
                    }

                    Interlocked.Add(ref _totalBytesSent, buffer.Length);
                }
                else
                {
                }
            }
        }
        public void Receive()
        {

            if (Socket != null || Socket.Connected)
            {

                var args = SocketHelpers.AcquireSocketArg();
                var offset = 0; //_offset + _remainingLength;

                args.SetBuffer(_bufferSegment.Buffer.Array, _bufferSegment.Offset + offset, sizeBuffer - offset);

                args.UserToken = this;
                args.Completed += ReceiveAsyncComplete;

                var willRaiseEvent = Socket.ReceiveAsync(args);

                if (!willRaiseEvent)
                {
                    ProcessRecieve(args);
                }
            }
        }
        private void ProcessRecieve(SocketAsyncEventArgs args)
        {
            try
            {
                var bytesReceived = args.BytesTransferred;

                if (args.BytesTransferred == 0)
                {
                    OnClosed();
                }
                else
                {
                    unchecked
                    {
                        _bytesReceived += (uint)bytesReceived;
                    }

                    Interlocked.Add(ref _totalBytesReceived, bytesReceived);


                    OnDataArrival(_bufferSegment.SegmentData); // weird interactions

                    _bufferSegment.DecrementUsage();
                    _bufferSegment = Buffers.CheckOut();
                    //_offset = 0;

                    //else
                    //{
                    //    EnsureBuffer();
                    //}

                    Receive();
                }
            }
            catch (LibraryNotLoadedException ex)
            {
                throw ex;
            }
            catch (ObjectDisposedException)
            {

                OnClosed();
            }
            catch (Exception)
            {

                OnClosed();
            }
            finally
            {
                args.Completed -= ReceiveAsyncComplete;
                SocketHelpers.ReleaseSocketArg(args);
            }
        }
        protected void EnsureBuffer() //(int size)
        {
            //if (size > BufferSize - _offset)
            {
                // not enough space left in buffer: Copy to new buffer
                var newSegment = Buffers.CheckOut();
                Array.Copy(_bufferSegment.Buffer.Array,
                    _bufferSegment.Offset + _offset,
                    newSegment.Buffer.Array,
                    newSegment.Offset,
                    _remainingLength);
                _bufferSegment.DecrementUsage();
                _bufferSegment = newSegment;
                _offset = 0;
            }
        }
        public override void Connect(string host, int port)
        {
            Connect(IPAddress.Parse(host), port);
        }

        public override void Disconnect()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (Socket != null && Socket.Connected)
            {
                try
                {
                    Socket.Shutdown(SocketShutdown.Both);
                    Socket.Close();
                    Socket = null;
                }
                catch
                {

                }
            }
        }
    }
}
