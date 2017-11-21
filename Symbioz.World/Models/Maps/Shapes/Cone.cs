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
    public class Cone : IShape
    {
        public uint Surface
        {
            get
            {
                return (uint)((this.Radius + 1) * (this.Radius + 1));
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
        public Cone(byte minRadius, byte radius)
        {
            this.MinRadius = minRadius;
            this.Radius = radius;
            this.Direction = DirectionsEnum.DIRECTION_SOUTH_EAST;
        }
        public short[] GetCells(short centerCell, MapRecord map)
        {
            MapPoint mapPoint = new MapPoint(centerCell);
            List<short> list = new List<short>();
            short[] result;
            if (this.Radius == 0)
            {
                if (this.MinRadius == 0)
                {
                    list.Add(centerCell);
                }
                result = list.ToArray();
            }
            else
            {
                int num = 0;
                int num2 = 1;
                switch (this.Direction)
                {
                    case DirectionsEnum.DIRECTION_SOUTH_EAST:
                        for (int i = mapPoint.X; i <= mapPoint.X + (int)this.Radius; i++)
                        {
                            for (int j = -num; j <= num; j++)
                            {
                                if (this.MinRadius == 0 || System.Math.Abs(mapPoint.X - i) + System.Math.Abs(j) >= (int)this.MinRadius)
                                {
                                    MapPoint.AddCellIfValid(i, j + mapPoint.Y, map, list);
                                }
                            }
                            num += num2;
                        }
                        break;
                    case DirectionsEnum.DIRECTION_SOUTH_WEST:
                        for (int j = mapPoint.Y; j >= mapPoint.Y - (int)this.Radius; j--)
                        {
                            for (int i = -num; i <= num; i++)
                            {
                                if (this.MinRadius == 0 || System.Math.Abs(i) + System.Math.Abs(mapPoint.Y - j) >= (int)this.MinRadius)
                                {
                                    MapPoint.AddCellIfValid(i + mapPoint.X, j, map, list);
                                }
                            }
                            num += num2;
                        }
                        break;
                    case DirectionsEnum.DIRECTION_NORTH_WEST:
                        for (int i = mapPoint.X; i >= mapPoint.X - (int)this.Radius; i--)
                        {
                            for (int j = -num; j <= num; j++)
                            {
                                if (this.MinRadius == 0 || System.Math.Abs(mapPoint.X - i) + System.Math.Abs(j) >= (int)this.MinRadius)
                                {
                                    MapPoint.AddCellIfValid(i, j + mapPoint.Y, map, list);
                                }
                            }
                            num += num2;
                        }
                        break;
                    case DirectionsEnum.DIRECTION_NORTH_EAST:
                        for (int j = mapPoint.Y; j <= mapPoint.Y + (int)this.Radius; j++)
                        {
                            for (int i = -num; i <= num; i++)
                            {
                                if (this.MinRadius == 0 || System.Math.Abs(i) + System.Math.Abs(mapPoint.Y - j) >= (int)this.MinRadius)
                                {
                                    MapPoint.AddCellIfValid(i + mapPoint.X, j, map, list);
                                }
                            }
                            num += num2;
                        }
                        break;

                }
                result = list.ToArray();
            }
            return result;
        }
    
    }
}
