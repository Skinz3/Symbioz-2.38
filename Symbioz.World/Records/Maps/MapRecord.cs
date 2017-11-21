using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.World.Models.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Core;
using System.Threading.Tasks;
using Symbioz.World.Providers.Maps;
using Symbioz.World.Records.Interactives;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Npcs;
using Symbioz.World.Providers.Maps.Monsters;
using Symbioz.World.Providers.Maps.Npcs;
using Symbioz.World.Models.Maps.Instances;

namespace Symbioz.World.Records.Maps
{
    [Table("Maps", true, 11)]
    public class MapRecord : ITable
    {
        public static List<MapRecord> Maps = new List<MapRecord>();

        [Ignore]
        public bool AbleToSpawn
        {
            get
            {
                return MapNoSpawnRecord.AbleToSpawn(this.Id);
            }
        }

        [Ignore]
        public MonsterSpawnRecord[] MonsterSpawnsSubArea = new MonsterSpawnRecord[0];

        [Ignore]
        public NpcSpawnRecord[] NpcsRecord = new NpcSpawnRecord[0];

        [Ignore]
        public AbstractMapInstance Instance;

        [Ignore]
        public MapPositionRecord Position;

        [Primary]
        public int Id;

        public ushort SubAreaId;

        [Ignore]
        public SubareaRecord SubArea;

        public int TopMap;

        public int DownMap;

        public int LeftMap;

        public int RightMap;

        [Update]
        public List<short> BlueCells;

        [Update]
        public List<short> RedCells;

        public Dictionary<ushort, short> CellsLosMov;

        [Ignore]
        public ushort[] WalkableCells
        {
            get;
            private set;
        }
        [Ignore]
        public ushort[] WalkableFightCells
        {
            get;
            private set;
        }
        [Ignore]
        public short X
        {
            get
            {
                return (short)Position.X;
            }
        }
        [Ignore]
        public short Y
        {
            get
            {
                return (short)Position.Y;
            }
        }
        [Ignore]
        public List<InteractiveElementRecord> InteractiveElements = new List<InteractiveElementRecord>();

        public InteractiveElementRecord Zaap
        {
            get
            {
                return InteractiveElements.Find(x => x.ElementType == 16 && x.MapId == Id);
            }
        }

        public bool HasZaap()
        {
            return Zaap != null;
        }
        public bool ValidForFight
        {
            get
            {
                return RedCells.Count > 0 && BlueCells.Count > 0;
            }
        }
        public bool LineOfSight(short cellId)
        {
            return (this.CellsLosMov[(ushort)cellId] & 8) == 0;
        }
        public InteractiveSkillRecord GetInteractiveSkill(uint elementId, uint skillUID)
        {
            return InteractiveElements.Find(x => x.ElementId == elementId).Skills.Find(x => x.UID == skillUID);
        }
        public InteractiveElementRecord GetInteractiveByElementType(ushort elementType)
        {
            return InteractiveElements.Find(x => x.ElementType == elementType);
        }

        public MapRecord(int id, ushort subareaid, int topmap, int downmap,
            int leftmap, int rightmap, List<short> bluecells, List<short> redcells, Dictionary<ushort, short> cellsLosMov)
        {
            this.Id = id;
            this.SubAreaId = subareaid;
            this.TopMap = topmap;
            this.DownMap = downmap;
            this.LeftMap = leftmap;
            this.RightMap = rightmap;
            this.BlueCells = bluecells;
            this.RedCells = redcells;
            this.CellsLosMov = cellsLosMov;
            this.WalkableCells = LoadWalkables();
            this.WalkableFightCells = LoadWalkableDuringFight();
            this.SubArea = SubareaRecord.GetSubarea(SubAreaId);
        }
        public ushort[] LoadWalkableDuringFight()
        {
            var nonWalkable = Array.ConvertAll(CellsLosMov.Where(x => (x.Value & 2) != 0).ToArray(), x => x.Key);
            var walkable = WalkableCells.ToList();
            walkable.RemoveAll(x => nonWalkable.Contains(x));
            return walkable.ToArray();
        }
        private ushort[] LoadWalkables()
        {
            return Array.ConvertAll(CellsLosMov.Where(x => (x.Value & 1) == 0).ToArray(), x => x.Key);
        }
        public bool Walkable(ushort cellid)
        {
            return WalkableCells.Contains(cellid);
        }
        public bool WalkableDuringFight(ushort cellId)
        {
            return WalkableFightCells.Contains(cellId);
        }
        public bool Walkable(int x, int y)
        {
            return Walkable((ushort)MapPoint.CoordToCellId(x, y));
        }
        public ushort RandomWalkableCell()
        {
            return WalkableCells.Random();
        }
        public ushort RandomNoBorderCell()
        {
            return Array.FindAll(WalkableCells, x => !CellShapesProvider.MapBorders.Contains((short)x)).Random();
        }
        public ushort RandomBorderCell()
        {
            return Array.FindAll(WalkableCells, x => CellShapesProvider.MapBorders.Contains((short)x)).Random();
        }
        public ushort RandomNoBorderFightCell()
        {
            return Array.FindAll(WalkableFightCells, x => !CellShapesProvider.MapBorders.Contains((short)x)).Random();
        }
        public ushort[] CloseCells(ushort cellid)
        {
            return new MapPoint((short)cellid).GetAdjacentCells((short entry)
                => Walkable((ushort)entry)).ToArray<MapPoint>().Select(x
                    => (ushort)x.CellId).ToArray();
        }
        public ushort CloseCellWithoutEntitiesPositions(ushort cellId)
        {
            var cells = CloseCells(cellId);
            if (cells.Count() == 0)
                return 0;
            else
            {
                return cells.FirstOrDefault(x => !Instance.GetEntities().ToList().ConvertAll<ushort>(w => w.CellId).Contains(x));
            }
        }
        public ushort CloseCellForDropItem(ushort cellId)
        {
            var cells = CloseCells(cellId);
            if (cells.Count() == 0)
                return 0;
            else
            {
                return cells.FirstOrDefault(x => !Instance.GetEntities().ToList().ConvertAll<ushort>(w => w.CellId).Contains(x) && !Array.ConvertAll(Instance.GetDroppedItems(), w => w.CellId).Contains(x));
            }
        }
        public static ushort GetSubAreaId(int mapid)
        {
            var map = GetMap(mapid);
            if (map != null)
                return (ushort)map.SubAreaId;
            else
                return 0;
        }
        public static List<int> GetSubAreaMaps(int subareaid)
        {
            return Maps.FindAll(x => x.SubAreaId == subareaid).ConvertAll<int>(x => x.Id);
        }
        public static MapRecord GetMap(int id)
        {
            return Maps.Find(x => x.Id == id);
        }
        public static List<MapRecord> GetMapWithoutPlacementCells()
        {
            return Maps.FindAll(x => x.BlueCells.Count == 0 || x.RedCells.Count == 0);
        }
        public static MapRecord RandomOutdoorMap()
        {
            var maps = Maps.FindAll(x => x.Position != null && x.Position.Outdoor);
            AsyncRandom random = new AsyncRandom();
            int index = random.Next(0, maps.Count);
            return maps[index];
        }
        [StartupInvoke("Map Instances", StartupInvokePriority.Ninth)]
        public static void CreateInstances()
        {
            UpdateLogger updateLogger = new UpdateLogger();

            int num = 0;
            foreach (var record in Maps)
            {

                record.InteractiveElements = InteractiveElementRecord.GetActiveElementsOnMap(record.Id);

                record.Position = MapPositionRecord.GetMapPosition(record.Id);

                record.MonsterSpawnsSubArea = MonsterSpawnRecord.GetSpawns(record.SubAreaId).ToArray();

                record.NpcsRecord = NpcSpawnRecord.GetMapNpcs(record.Id).ToArray();

                record.Instance = new MapInstance(record);

                NpcSpawnsManager.Instance.SpawnAtStartup(record);

                if (record.AbleToSpawn)
                    MonsterSpawnManager.Instance.SpawnMonsters(record);

                updateLogger.Update(num.Percentage(Maps.Count));
                num++;
            }

        }
    }
}
