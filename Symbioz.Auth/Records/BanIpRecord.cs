using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Auth.Records
{
    [Table("BanIps")]
    public class BanIpRecord : ITable
    {
        public static List<BanIpRecord> BanIps = new List<BanIpRecord>();

        public string Ip;

        public BanIpRecord(string ip)
        {
            this.Ip = ip;
        }

        public static void Add(string ip)
        {
            if (!IsBanned(ip))
            {
                new BanIpRecord(ip).AddInstantElement<BanIpRecord>();
            }
          
        }
        public static bool IsBanned(string ip)
        {
            return BanIps.Find(x => x.Ip == ip) != null;
        }
    }
}
