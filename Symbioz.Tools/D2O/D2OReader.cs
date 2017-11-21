
using SSync.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Tools.D2O
{
    public class D2OReader
    {
        private string m_Path { get; set; }

        public string FileName { get { return Path.GetFileName(m_Path); } }

        public D2OReader(string path)
        {
            this.m_Path = path;
            this.D2oFileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            this.dictionary_1 = new Dictionary<int, int>();
            this.dictionary_0 = new Dictionary<int, Class16>();
            this.Reader = new BigEndianReader(this.D2oFileStream);
            Encoding.Default.GetString(this.Reader.ReadBytes(3));
            this.D2oFileStream.Position = this.Reader.ReadInt();
            int num = this.Reader.ReadInt();
            int i = 1;
            while ((i <= num))
            {
                this.dictionary_1.Add(this.Reader.ReadInt(), this.Reader.ReadInt());
                this.int_0 += 1;
                i = (i + 8);
            }
            int num3 = this.Reader.ReadInt();
            int j = 1;
            while ((j <= num3))
            {
                this.method_2(this.Reader.ReadInt());
                j += 1;
            }
        }

        public DataClass[] GetObjects()
        {
            List<DataClass> list = new List<DataClass>();
            int num = 0;
            foreach (int num_loopVariable in this.dictionary_1.Keys)
            {
                num = num_loopVariable;
                this.D2oFileStream.Position = Convert.ToInt64(this.dictionary_1[num]);
                int key = this.Reader.ReadInt();
                if (this.dictionary_0.ContainsKey(key))
                {
                    DataClass item = this.dictionary_0[key].method_0(string.Empty, this.Reader);
                    list.Add(item);
                }
            }
            return list.ToArray();
        }

        private void method_2(int int_1)
        {
            Class16 class2 = new Class16(this, this.Reader.ReadUTF(), this.Reader.ReadUTF());
            int num2 = this.Reader.ReadInt();
            int i = 1;
            while ((i <= num2))
            {
                class2.method_1(this.Reader.ReadUTF(), this.Reader);
                i += 1;
            }
            this.dictionary_0.Add(int_1, class2);
        }


        internal Dictionary<int, Class16> dictionary_0;
        private Dictionary<int, int> dictionary_1;
        private Dictionary<int, DataClass> Id_Data = new Dictionary<int, DataClass>();
        private BigEndianReader Reader;
        private FileStream D2oFileStream;
        private int int_0 = 0;
    }
}
