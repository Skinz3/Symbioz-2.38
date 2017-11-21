using SSync.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zlib;

namespace Symbioz.Tools.ELE
{
    /// <summary>
    /// Stump
    /// </summary>
    public class EleReader
    {
        private BigEndianReader m_reader;
        private Stream m_stream;

        public EleReader(string filePath)
        {
            this.m_stream = File.OpenRead(filePath);
            this.m_reader = new BigEndianReader(this.m_stream);
        }

        public EleReader(Stream stream)
        {
            this.m_stream = stream;
            this.m_reader = new BigEndianReader(this.m_stream);
        }

        public Elements ReadElements()
        {
            this.m_reader.Seek(0, SeekOrigin.Begin);
            int header = (int)this.m_reader.ReadByte();
            this.m_reader.Seek(0, SeekOrigin.Begin);
            MemoryStream output = new MemoryStream();
            Deflate(new MemoryStream(this.m_reader.ReadBytes((int)this.m_reader.BytesAvailable)), output);
            byte[] uncompress = output.ToArray();
            this.m_reader = new BigEndianReader(uncompress);
            return Elements.ReadFromStream(this.m_reader);
        }

        private void ChangeStream(Stream stream)
        {
            this.m_stream.Dispose();
            this.m_reader.Dispose();
            this.m_stream = stream;
            this.m_reader = new BigEndianReader(this.m_stream);
        }

        private static void Deflate(Stream input, Stream output)
        {
            ZOutputStream zoutput = new ZOutputStream(output);
            BinaryReader inputReader = new BinaryReader(input);
            zoutput.Write(inputReader.ReadBytes((int)input.Length), 0, (int)input.Length);
            zoutput.Flush();
        }

        public void Dispose()
        {
            this.m_stream.Dispose();
            this.m_reader.Dispose();
        }
    }
}
