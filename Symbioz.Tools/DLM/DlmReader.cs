using SSync.IO;
using Symbioz.MapLoader.IO;
using System;
using System.IO;

namespace Symbioz.Tools.DLM
{
    public class DlmReader : IDisposable
    {
        private BigEndianReader m_reader;
        private Stream m_stream;

        public string DecryptionKey
        {
            get;
            set;
        }

        public Func<int, string> DecryptionKeyProvider
        {
            get;
            set;
        }

        public DlmReader(string filePath)
        {
            this.m_stream = File.OpenRead(filePath);
            this.m_reader = new BigEndianReader(this.m_stream);
        }

        public DlmReader(Stream stream)
        {
            this.m_stream = stream;
            this.m_reader = new BigEndianReader(this.m_stream);
        }

        public DlmReader(string filePath, string decryptionKey)
        {
            this.m_stream = File.OpenRead(filePath);
            this.m_reader = new BigEndianReader(this.m_stream);
            this.DecryptionKey = decryptionKey;
        }

        public DlmReader(Stream stream, string decryptionKey)
        {
            this.m_stream = stream;
            this.m_reader = new BigEndianReader(this.m_stream);
            this.DecryptionKey = decryptionKey;
        }

        public DlmMap ReadMap()
        {
            int header = (int)this.m_reader.ReadByte();
            if (header != 77)
            {
                try
                {
                    this.m_reader.Seek(0, SeekOrigin.Begin);
                    MemoryStream output = new MemoryStream();
                    ZipHelper.Deflate(new MemoryStream(this.m_reader.ReadBytes((int)this.m_reader.BytesAvailable)), output);
                    byte[] uncompress = output.ToArray();
                    this.ChangeStream(new MemoryStream(uncompress));
                    header = (int)this.m_reader.ReadByte();
                    if (header != 77)
                    {
                        throw new FileLoadException("Wrong header file");
                    }
                }
                catch (Exception)
                {
                    throw new FileLoadException("Wrong header file");
                }
            }
            return DlmMap.ReadFromStream(this.m_reader, this);
        }

        internal void ChangeStream(Stream stream)
        {
            this.m_stream.Dispose();
            this.m_reader.Dispose();
            this.m_stream = stream;
            this.m_reader = new BigEndianReader(this.m_stream);
        }

        public void Dispose()
        {
            this.m_stream.Dispose();
            this.m_reader.Dispose();
        }
    }
}