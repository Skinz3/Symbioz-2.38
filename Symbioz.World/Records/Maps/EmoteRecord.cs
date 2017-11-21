using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Maps
{
    [Table("Emotes")]
    public class EmoteRecord : ITable
    {
        public static List<EmoteRecord> Emotes = new List<EmoteRecord>();

        [Primary]
        public byte Id;

        public string Name;

        public bool IsAura;

        public ushort AuraBones;

        public EmoteRecord(byte id,string name,bool isAura,ushort auraBones)
        {
            this.Id = id;
            this.Name = name;
            this.IsAura = isAura;
            this.AuraBones = auraBones;
        }

        public static EmoteRecord GetEmote(byte id)
        {
            return Emotes.Find(x => x.Id == id);
        }
    }
}
