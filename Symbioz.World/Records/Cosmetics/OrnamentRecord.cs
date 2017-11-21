using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Cosmetics
{
    [Table("ornaments")]
    public class OrnamentRecord : ITable
    {
        public static List<OrnamentRecord> Ornaments = new List<OrnamentRecord>();

        [Primary]
        public ushort Id;

        public string Name;

        public OrnamentRecord(ushort id,string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
