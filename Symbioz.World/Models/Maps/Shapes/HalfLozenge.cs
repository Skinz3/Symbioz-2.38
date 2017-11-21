
using Symbioz.Protocol.Enums;
using Symbioz.World.Records;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;

namespace Symbioz.World.Models.Maps.Shapes
{
    public class HalfLozenge : IShape
    {
        public uint Surface
        {
            get
            {
                return (uint)(this.Radius * 2 + 1);
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
        public HalfLozenge(byte minRadius, byte radius)
        {
            this.MinRadius = minRadius;
            this.Radius = radius;
            this.Direction = DirectionsEnum.DIRECTION_NORTH;
        }
        public short[] GetCells(short centerCell, MapRecord map)
        {
            MapPoint mapPoint = new MapPoint(centerCell);
            List<short> list = new List<short>();
            if (this.MinRadius == 0)
            {
                list.Add(centerCell);
            }
            for (int i = 1; i <= (int)this.Radius; i++)
            {
                switch (this.Direction)
                {
                    case DirectionsEnum.DIRECTION_SOUTH_EAST:
                        MapPoint.AddCellIfValid(mapPoint.X - i, mapPoint.Y + i, map, list);
                        MapPoint.AddCellIfValid(mapPoint.X - i, mapPoint.Y - i, map, list);
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH_WEST:
                        MapPoint.AddCellIfValid(mapPoint.X - i, mapPoint.Y + i, map, list);
                        MapPoint.AddCellIfValid(mapPoint.X + i, mapPoint.Y + i, map, list);
                        break;
                    case DirectionsEnum.DIRECTION_NORTH_WEST:
                        MapPoint.AddCellIfValid(mapPoint.X + i, mapPoint.Y + i, map, list);
                        MapPoint.AddCellIfValid(mapPoint.X + i, mapPoint.Y - i, map, list);
                        break;
                    case DirectionsEnum.DIRECTION_NORTH_EAST:
                        MapPoint.AddCellIfValid(mapPoint.X - i, mapPoint.Y - i, map, list);
                        MapPoint.AddCellIfValid(mapPoint.X + i, mapPoint.Y - i, map, list);
                        break;
                }
            }
            return list.ToArray();
        }
       
    }
}
