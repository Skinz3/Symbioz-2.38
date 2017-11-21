using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Monsters
{
    [Table("EndFightActions")]
    public class EndFightActionRecord : ITable
    {
        public static List<EndFightActionRecord> EndFightActions = new List<EndFightActionRecord>();

        [Primary]
        public int Id;

        public int MapId;

        public int TeleportMapId;

        public ushort TeleportCellId;

        public EndFightActionRecord(int id, int mapId, int teleportMapId, ushort teleportCellId)
        {
            this.Id = id;
            this.MapId = mapId;
            this.TeleportMapId = teleportMapId;
            this.TeleportCellId = teleportCellId;
        }

        public static EndFightActionRecord GetEndFightAction(int mapId)
        {
            return EndFightActions.Find(x => x.MapId == mapId);
        }
    }
}
