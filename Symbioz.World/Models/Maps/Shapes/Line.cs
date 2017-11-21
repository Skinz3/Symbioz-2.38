using Symbioz.Protocol.Enums;
using Symbioz.World.Records;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Maps.Shapes
{
    public class Line : IShape
    {
        public uint Surface
        {
            get
            {
                return (uint)(this.Radius + 1);
            }
        }
        public byte MinRadius
        {
            get;
            set;
        }
        public DirectionsEnum Direction
        {
            get;
            set;
        }
        public byte Radius
        {
            get;
            set;
        }
        public Line(byte radius)
        {
            this.Radius = radius;
            this.Direction = DirectionsEnum.DIRECTION_SOUTH_WEST;
        }
        public Line(byte radius,DirectionsEnum direction)
        {
            this.Radius = radius;
            this.Direction = direction;
        }
        public short[] GetCells(short centerCell, MapRecord map)
        {
            MapPoint mapPoint = new MapPoint(centerCell);
            List<short> list = new List<short>();
            for (int i = (int)this.MinRadius; i <= (int)this.Radius; i++)
            {
                switch (this.Direction)
                {
                    case DirectionsEnum.DIRECTION_EAST:
                        MapPoint.AddCellIfValid(mapPoint.X + i, mapPoint.Y + i, map, list);
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH_EAST:
                        MapPoint.AddCellIfValid(mapPoint.X + i, mapPoint.Y, map, list);
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH:
                        MapPoint.AddCellIfValid(mapPoint.X + i, mapPoint.Y - i, map, list);
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH_WEST:
                        MapPoint.AddCellIfValid(mapPoint.X, mapPoint.Y - i, map, list);
                        break;
                    case DirectionsEnum.DIRECTION_WEST:
                        MapPoint.AddCellIfValid(mapPoint.X - i, mapPoint.Y - i, map, list);
                        break;
                    case DirectionsEnum.DIRECTION_NORTH_WEST:
                        MapPoint.AddCellIfValid(mapPoint.X - i, mapPoint.Y, map, list);
                        break;
                    case DirectionsEnum.DIRECTION_NORTH:
                        MapPoint.AddCellIfValid(mapPoint.X - i, mapPoint.Y + i, map, list);
                        break;
                    case DirectionsEnum.DIRECTION_NORTH_EAST:
                        MapPoint.AddCellIfValid(mapPoint.X, mapPoint.Y + i, map, list);
                        break;
                }
            }
            return list.ToArray();
        }
    }
}
