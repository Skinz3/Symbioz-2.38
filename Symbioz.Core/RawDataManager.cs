using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core
{
    public class RawDataManager
    {
        static Logger logger = new Logger();

        public static string RawsDirectory = Environment.CurrentDirectory + "/SWF/";

        public static byte[] GetRawData(string fileName)
        {
            return File.ReadAllBytes(RawsDirectory + fileName + ".swf");
        }
    }
}
