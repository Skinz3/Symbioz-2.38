
using Symbioz.Protocol.Enums;
using Symbioz.World.Records;
using Symbioz.World.Records.Maps;

namespace Symbioz.World.Models.Maps.Shapes
{
    public interface IShape
    {
        uint Surface
        {
            get;
        }
        byte MinRadius
        {
            get;
            set;
        }
        DirectionsEnum Direction
        {
            get;
            set;
        }
        byte Radius
        {
            get;
            set;
        }
        short[] GetCells(short centerCell, MapRecord map);
    }
}
