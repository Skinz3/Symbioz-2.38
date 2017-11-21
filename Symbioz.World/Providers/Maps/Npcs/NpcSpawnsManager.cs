using Symbioz.Core.DesignPattern;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps.Npcs
{
    public class NpcSpawnsManager : Singleton<NpcSpawnsManager>
    {
        public void SpawnAtStartup(MapRecord record)
        {
            foreach (var npcRecord in record.NpcsRecord)
            {
                record.Instance.AddNpc(npcRecord, true);
            }
        }
        public void Spawn(ushort npcId, MapRecord record, ushort cellId, sbyte direction)
        {
            var spawnRecord = NpcSpawnRecord.AddNpc(npcId, record.Id, cellId, direction);
            record.Instance.AddNpc(spawnRecord, false);
        }
    }
}
