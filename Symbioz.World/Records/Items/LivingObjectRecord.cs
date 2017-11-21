using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Items
{
    [Table("LivingObjects")]
    public class LivingObjectRecord : ITable
    {
        public static List<LivingObjectRecord> LivingObjects = new List<LivingObjectRecord>();

        [Primary]
        public ushort GId;

        public ushort ItemType;

        public List<ushort> Skins;

        [Ignore]
        public bool Skinnable
        {
            get
            {
                return Skins.Count > 0;
            }
        }

        public LivingObjectRecord(ushort gid, ushort itemType, List<ushort> skins)
        {
            this.GId = gid;
            this.ItemType = itemType;
            this.Skins = skins;
        }
        public ushort GetSkin(ushort skinId)
        {
            return Skins[skinId - 1];
        }
        public static LivingObjectRecord GetLivingObjectDatas(ushort gid)
        {
            return LivingObjects.Find(x => x.GId == gid);
        }
        public static bool IsLivingObject(ushort gid)
        {
            return LivingObjects.Find(x => x.GId == gid) != null;
        }
    }
}
