using Symbioz.ORM;
using Symbioz.Tools.DLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbioz.SqlSync.MapTables
{
    [Table("Maps")]
    public class Map : ITable
    {
        [Primary]
        public int Id;

        public ushort SubAreaId;

        public int TopMap;

        public int DownMap;

        public int LeftMap;

        public int RightMap;

        public List<short> BlueCells;

        public List<short> RedCells;

        public Dictionary<short, short> CellsLosMov;

        public int Version;

        public static Map FromDlm(DlmMap dlmMap)
        {
            Map map = new Map();
            map.Id = dlmMap.Id;
            map.SubAreaId = (ushort)dlmMap.SubAreaId;
            map.TopMap = dlmMap.TopNeighbourId;
            map.DownMap = dlmMap.BottomNeighbourId;
            map.LeftMap = dlmMap.LeftNeighbourId;
            map.RightMap = dlmMap.RightNeighbourId;

            map.BlueCells = dlmMap.Cells.Where(x => x.Blue).ToList().ConvertAll<short>(x => x.Id);
            map.RedCells = dlmMap.Cells.Where(x => x.Red).ToList().ConvertAll<short>(x => x.Id);

            map.Version = dlmMap.Version;

            map.CellsLosMov = new Dictionary<short, short>();

            foreach (var cell in dlmMap.Cells)
            {
                map.CellsLosMov.Add(cell.Id,cell.LosMov);
            }

            return map;

        }


    }
}
