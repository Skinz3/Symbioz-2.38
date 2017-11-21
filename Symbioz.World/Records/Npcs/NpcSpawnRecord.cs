using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;

namespace Symbioz.World.Records.Npcs
{
    [Table("NpcsSpawns", true, 3)]
    public class NpcSpawnRecord : ITable
    {
        public static List<NpcSpawnRecord> NpcsSpawns = new List<NpcSpawnRecord>();
        [Primary]
        public int Id;

        public ushort TemplateId;

        public int MapId;

        [Update]
        public ushort CellId;

        [Update]
        public sbyte Direction;

        [Ignore]
        public DirectionsEnum DirectionEnum
        {
            get
            {
                return (DirectionsEnum)Direction;
            }
            set
            {
                Direction = (sbyte)value;
            }
        }
        [Ignore]
        public NpcRecord Template
        {
            get;
            set;
        }
        public NpcSpawnRecord(int id, ushort templateId, int mapId, ushort cellId, sbyte direction)
        {
            this.Id = id;
            this.TemplateId = templateId;
            this.Template = NpcRecord.GetNpc(templateId);
            this.MapId = mapId;
            this.CellId = cellId;
            this.Direction = direction;
        }
        public static List<NpcSpawnRecord> GetMapNpcs(int mapId)
        {
            return NpcsSpawns.FindAll(x => x.MapId == mapId);
        }
        public static NpcSpawnRecord AddNpc(ushort templateId, int mapId, ushort cellId, sbyte direction)
        {
            var spawn = new NpcSpawnRecord(NpcsSpawns.DynamicPop(x => x.Id), templateId, mapId, cellId, direction);
            spawn.AddInstantElement();
            return spawn;
        }
        public static NpcSpawnRecord GetSpawn(int id)
        {
            return NpcsSpawns.FirstOrDefault(x => x.Id == id);
        }
    }
}
