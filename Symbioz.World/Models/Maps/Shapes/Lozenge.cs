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
    public class Lozenge : IShape
    {
        public uint Surface
        {
            get
            {
                return (uint)((this.Radius + 1) * (this.Radius + 1) + this.Radius * this.Radius);
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
        public Lozenge(byte minRadius, byte radius)
        {
            this.MinRadius = minRadius;
            this.Radius = radius;
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
                int i = mapPoint.X - (int)this.Radius;
                int num = 0;
                int num2 = 1;
                while (i <= mapPoint.X + (int)this.Radius)
                {
                    for (int j = -num; j <= num; j++)
                    {
                        if (this.MinRadius == 0 || System.Math.Abs(mapPoint.X - i) + System.Math.Abs(j) >= (int)this.MinRadius)
                        {
                            MapPoint.AddCellIfValid(i, j + mapPoint.Y, map, list);
                        }
                    }
                    if (num == (int)this.Radius)
                    {
                        num2 = -num2;
                    }
                    num += num2;
                    i++;
                }
                result = list.ToArray();
            }
            return result;
        }
    
    }
}
