using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Modules
{
    public static class Extensions
    {
        public static string GetHash(this byte[] data)
        {
            byte[] myHash = MD5.Create().ComputeHash(data);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < myHash.Length; i++)
            {
                sb.Append(myHash[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }
    }
}
