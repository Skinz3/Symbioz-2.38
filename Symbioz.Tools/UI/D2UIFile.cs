using SSync.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Tools.UI
{
    public class D2UIFile
    {
        public const string FILE_HEADER = "D2UI";

        public string Path
        {
            get;
            private set;
        }
        public Dictionary<string, int> UIListPosition
        {
            get;
            private set;
        }
        public string Xml
        {
            get;
            set;
        }
        public D2UIFile(string path)
        {
            this.Path = path;

            if (File.Exists(path))
                this.Open();
        }
        public D2UIFile()
        {
            this.UIListPosition = new Dictionary<string, int>();
            this.Xml = "";
        }
        private void Open()
        {
            BigEndianReader reader = new BigEndianReader(File.ReadAllBytes(Path));

            string header = reader.ReadUTF();

            if (header != FILE_HEADER)
            {
                throw new Exception("malformated file (wrong header)");
            }

            UIListPosition = new Dictionary<string, int>();

            uint loc7 = 0;

            this.Xml = reader.ReadUTF();

            short definitionCount = reader.ReadShort();

            while (loc7 < definitionCount)
            {
                UIListPosition.Add(reader.ReadUTF(), reader.ReadInt());
                loc7++;
            }
        }
        public void Save()
        {
            BigEndianWriter writer = new BigEndianWriter();

            writer.WriteUTF(FILE_HEADER);

            writer.WriteUTF(Xml);


            writer.WriteShort((short)UIListPosition.Count);

            foreach (var def in UIListPosition)
            {
                writer.WriteUTF(def.Key);
                writer.WriteInt(def.Value);
            }

            File.WriteAllBytes(Path, writer.Data);

        }
    }
}
