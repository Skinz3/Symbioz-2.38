using SSync.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Tools.SWL
{
    public class SwlFile
    {
        string FileName;

        public SwlFile(string path)
        {
            BigEndianReader reader = new BigEndianReader(File.ReadAllBytes(path));
            FileName = Path.GetFileNameWithoutExtension(path);
            Deserialize(reader);

        }
        byte[] swf;
        private void Deserialize(BigEndianReader reader)
        {
            byte header = reader.ReadByte();
            if (header != 76)
            {
                throw new Exception("Malformated library file (wrong header).");
            }
            byte version = reader.ReadByte();
            uint frameRate = reader.ReadUInt();
            int classesCount = reader.ReadInt();

            var classes = new List<string>();
            for (int i = 0; i < classesCount; i++)
            {
                classes.Add(reader.ReadUTF());
            }

            swf = reader.ReadBytes(reader.BytesAvailable);
        }

        public void ExtractSwf(string output)
        {
            File.WriteAllBytes(output + FileName + ".swf", swf);
        }
    }

}
