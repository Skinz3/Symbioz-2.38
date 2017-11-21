using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Characters
{
    [Table("Heads")]
    public class HeadRecord : ITable
    {
        public static List<HeadRecord> Heads = new List<HeadRecord>();

        public int Id;

        public ushort SkinId;

        public HeadRecord(int id, ushort skinid)
        {
            this.Id = id;
            this.SkinId = skinid;
        }

        public static ushort GetSkin(int cosmeticid)
        {
            return Heads.Find(x => x.Id == cosmeticid).SkinId;
        }
    }
}
