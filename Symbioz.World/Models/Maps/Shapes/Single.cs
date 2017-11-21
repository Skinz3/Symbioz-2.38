
using Symbioz.Protocol.Enums;
using Symbioz.World.Records;
using Symbioz.World.Records.Maps;
using System;

namespace Symbioz.World.Models.Maps.Shapes
{
	public class Single : IShape
	{
		public uint Surface
		{
			get
			{
				return 1u;
			}
		}
		public byte MinRadius
		{
			get
			{
				return 1;
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
				return 1;
			}
			set
			{
			}
		}
		public short[] GetCells(short centerCell,MapRecord map)
		{
            return new short[] { centerCell };
		}
	}
}
