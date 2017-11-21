using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Maps
{
    [Table("MapsNoSpawns")]
    public class MapNoSpawnRecord : ITable
    {
        public static List<MapNoSpawnRecord> MapsNoSpawns = new List<MapNoSpawnRecord>();

        [Primary]
        public int MapId;

        public MapNoSpawnRecord(int mapId)
        {
            this.MapId = mapId;
        }

        public static bool AbleToSpawn(int mapId)
        {
            return MapsNoSpawns.FirstOrDefault(x => x.MapId == mapId) == null;
        }

        public static void UpdateSpawns()
        {
            foreach (var map in MapRecord.Maps)
            {
                if (map.HasZaap() || map.Position.Name != string.Empty || !map.ValidForFight)
                {
                    if (AbleToSpawn(map.Id))
                        new MapNoSpawnRecord(map.Id).AddInstantElement();
                }
            }
        }
    }
}
