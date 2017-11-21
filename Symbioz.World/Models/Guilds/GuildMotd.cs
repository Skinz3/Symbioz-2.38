using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Guilds
{
    public class GuildMotd
    {
        public string Content
        {
            get;
            set;
        }
        public int Timestamp
        {
            get;
            set;
        }
        public ulong MemberId
        {
            get;
            set;
        }
        public string MemberName
        {
            get;
            set;
        }
    }
}
