using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records
{
    public class CSVDoubleArray
    {
        public uint[][] Values = new uint[0][];
        public CSVDoubleArray(uint[][] values)
        {
            this.Values = values;
        }
        public static CSVDoubleArray Deserialize(string str)
        {
            if (str == string.Empty)
                return new CSVDoubleArray(new uint[0][]);
            List<uint[]> list = new List<uint[]>();
            foreach (var value in str.Split('|'))
            {
                List<uint> subList = new List<uint>();
                foreach (var value2 in value.Split(','))
                {
                    subList.Add(uint.Parse(value2));
                }
                list.Add(subList.ToArray());
            }

            return new CSVDoubleArray(list.ToArray());
        }

    }
}
