using Symbioz.Protocol.Enums;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Maps.Shapes
{
    public class All : IShape
    {
        public uint Surface
        {
            get
            {
                return 0u;
            }
        }
        public byte MinRadius
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }
        public DirectionsEnum Direction
        {
            get
            {
                return DirectionsEnum.DIRECTION_NORTH;
            }
            set
            {
            }
        }
        public byte Radius
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        public short[] GetCells(short centerCell, MapRecord map)
        {
            return Array.ConvertAll(map.WalkableCells, x => (short)x);
        }
    }
}
