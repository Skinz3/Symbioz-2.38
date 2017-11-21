using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.ORM;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Core;
using Symbioz.World.Providers.Maps;

namespace Symbioz.World.Records.Maps
{
    [Table("ScrollActions")]
    public class ScrollActionRecord : ITable
    {
        public static List<ScrollActionRecord> ScrollActions = new List<ScrollActionRecord>();

        public int MapId;
        public int RightMapId;
        public int BottomMapId;
        public int LeftMapId;
        public int TopMapId;

        public ScrollActionRecord(int mapid, int rightmapid, int bottommapid, int leftmapid, int topmapid)
        {
            this.MapId = mapid;
            this.RightMapId = rightmapid;
            this.BottomMapId = bottommapid;
            this.LeftMapId = leftmapid;
            this.TopMapId = topmapid;
        }
        public static sbyte GetScrollDirection(MapScrollEnum type)
        {
            switch (type)
            {
                case MapScrollEnum.Top:
                    return 6;

                case MapScrollEnum.Left:
                    return 4;

                case MapScrollEnum.Bottom:
                    return 2;

                case MapScrollEnum.Right:
                    return 0;

                default:
                    return 0;
            }
        }
        public static ushort SearchScrollCellId(ushort cellid, MapScrollEnum type, MapRecord map)
        {
            var defaultCell = GetScrollDefaultCellId(cellid, type);
            var cells = CellShapesProvider.GetMapBorder(GetOposedTransition(type));
            var walkables = cells.FindAll(x => map.Walkable((ushort)x));
            return walkables.Count == 0 ? map.RandomWalkableCell() : (ushort)walkables[new AsyncRandom().Next(0, walkables.Count - 1)];
        }
        public static MapScrollEnum GetOposedTransition(MapScrollEnum type)
        {
            switch (type)
            {
                case MapScrollEnum.Top:
                    return MapScrollEnum.Bottom;
                case MapScrollEnum.Left:
                    return MapScrollEnum.Right;
                case MapScrollEnum.Bottom:
                    return MapScrollEnum.Top;
                case MapScrollEnum.Right:
                    return MapScrollEnum.Left;
            }
            throw new Exception("What is that MapScrollType dude?");
        }
        public static ushort GetScrollDefaultCellId(ushort cellid, MapScrollEnum type)
        {
            switch (type)
            {
                case MapScrollEnum.Top:
                    return (ushort)(cellid + 532);
                case MapScrollEnum.Left:
                    return (ushort)(cellid + 27);
                case MapScrollEnum.Bottom:
                    return (ushort)(cellid - 532); ;
                case MapScrollEnum.Right:
                    return (ushort)(cellid - 27);
                default:
                    return 0;
            }
        }
        public static MapScrollEnum GetScrollTypeFromCell(short cellid)
        {
            if (CellShapesProvider.GetMapBorder(MapScrollEnum.Top).Contains(cellid))
                return MapScrollEnum.Top;
            if (CellShapesProvider.GetMapBorder(MapScrollEnum.Bottom).Contains(cellid))
                return MapScrollEnum.Bottom;
            if (CellShapesProvider.GetMapBorder(MapScrollEnum.Left).Contains(cellid))
                return MapScrollEnum.Left;
            if (CellShapesProvider.GetMapBorder(MapScrollEnum.Right).Contains(cellid))
                return MapScrollEnum.Right;
            return MapScrollEnum.UNDEFINED;
        }
        public static int GetOverrideScrollMapId(int mapid, MapScrollEnum type)
        {
            var scroll = ScrollActions.Find(x => x.MapId == mapid);
            if (scroll != null)
            {
                switch (type)
                {
                    case MapScrollEnum.Top:
                        return scroll.TopMapId;
                    case MapScrollEnum.Left:
                        return scroll.LeftMapId;
                    case MapScrollEnum.Bottom:
                        return scroll.BottomMapId;
                    case MapScrollEnum.Right:
                        return scroll.RightMapId;
                }
            }
            return -1;
        }

    }
}
