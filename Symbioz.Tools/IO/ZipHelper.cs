using System.IO;
using System.IO.Compression;
using zlib;

namespace Symbioz.MapLoader.IO
{
    public class ZipHelper
    {
        public static byte[] Compress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream memoryStream = new MemoryStream();
            ZipHelper.Compress(input, memoryStream);
            return memoryStream.ToArray();
        }

        public static void Compress(Stream input, Stream output)
        {
            using (GZipStream gZipStream = new GZipStream(output, CompressionMode.Compress))
            {
                input.CopyTo(gZipStream);
            }
        }

        public static byte[] Uncompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream memoryStream = new MemoryStream();
            ZipHelper.Uncompress(input, memoryStream);
            return memoryStream.ToArray();
        }

        public static void Uncompress(Stream input, Stream output)
        {
            using (GZipStream gZipStream = new GZipStream(input, CompressionMode.Decompress, true))
            {
                gZipStream.CopyTo(output);
            }
        }

        public static void Deflate(Stream input, Stream output)
        {
            ZOutputStream zOutputStream = new ZOutputStream(output);
            BinaryReader binaryReader = new BinaryReader(input);
            zOutputStream.Write(binaryReader.ReadBytes((int)input.Length), 0, (int)input.Length);
            zOutputStream.Flush();
        }
    }
}