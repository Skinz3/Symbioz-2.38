using Symbioz.Core;
using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Maps
{
    [Table("ArenaMaps")]
    public class ArenaMapRecord : ITable
    {
        public static List<ArenaMapRecord> ArenaMaps = new List<ArenaMapRecord>();

        public int MapId;

        public ArenaMapRecord(int mapId)
        {
            this.MapId = mapId;
        }

        public static MapRecord GetArenaMap()
        {
            return MapRecord.GetMap(ArenaMaps.Random().MapId);
        }
    }
}
