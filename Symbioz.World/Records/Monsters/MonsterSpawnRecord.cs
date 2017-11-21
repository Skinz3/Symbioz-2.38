using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Monsters
{
    [Table("MonsterSpawns")]
    public class MonsterSpawnRecord : ITable
    {
        public static List<MonsterSpawnRecord> MonsterSpawns = new List<MonsterSpawnRecord>();

        [Primary]
        public int Id;

        public ushort MonsterId;

        public ushort SubareaId;

        public sbyte Probability;

        public MonsterSpawnRecord(int id,ushort monsterId,ushort subareaId,sbyte probability)
        {
            this.Id = id;
            this.MonsterId = monsterId;
            this.SubareaId = subareaId;
            this.Probability = probability;
        }

        public static List<MonsterSpawnRecord> GetSpawns(ushort subareaId)
        {
            return MonsterSpawns.FindAll(x => x.SubareaId == subareaId);
        }
    }
}
