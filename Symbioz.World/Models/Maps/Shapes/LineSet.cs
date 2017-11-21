using Symbioz.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Maps.Shapes
{
    public class LineSet
    {
        private static readonly Dictionary<DirectionsEnum, MapPoint> m_vectors = new Dictionary
            <DirectionsEnum, MapPoint>
        {
            {DirectionsEnum.DIRECTION_EAST, new MapPoint(1, 1)},
            {DirectionsEnum.DIRECTION_SOUTH_EAST, new MapPoint(1, 0)},
            {DirectionsEnum.DIRECTION_SOUTH, new MapPoint(1, -1)},
            {DirectionsEnum.DIRECTION_SOUTH_WEST, new MapPoint(0, -1)},
            {DirectionsEnum.DIRECTION_WEST, new MapPoint(-1,-1)},
            {DirectionsEnum.DIRECTION_NORTH_WEST, new MapPoint(-1, 0)},
            {DirectionsEnum.DIRECTION_NORTH, new MapPoint(-1, 1)},
            {DirectionsEnum.DIRECTION_NORTH_EAST, new MapPoint(0, 1)}
        };

        public LineSet(MapPoint A, MapPoint B)
        {
            this.A = A;
            this.B = B;
        }

        public LineSet(MapPoint start, int length, DirectionsEnum direction)
        {
            Start = start;
            Length = length;
            Direction = direction;
        }

        public MapPoint Start
        {
            get;
            set;
        }

        public int Length
        {
            get;
            set;
        }

        public DirectionsEnum Direction
        {
            get;
            set;
        }

        public MapPoint A
        {
            get;
            set;
        }

        public MapPoint B
        {
            get;
            set;
        }

        public IEnumerable<MapPoint> EnumerateSet()
        {
            if (Start == null)
                foreach (var point in Raytracing())
                    yield return point;
            else
            {
                var vector = m_vectors[Direction];
                for (var i = 0; i < Length; i++)
                {
                    yield return new MapPoint(Start.X + vector.X * i, Start.Y + vector.Y * i);
                }
            }
        }

        public bool BelongToSet(MapPoint point)
        {
            if (Start == null)
            {
                return SquareDistanceToLine(point) < 0.1;
            }
            if (Equals(point, Start))
                return true;

            var vector = m_vectors[Direction];
            var dx = point.X - Start.X;
            var dy = point.Y - Start.Y;

            if (vector.X == 0 && dx != 0 || vector.Y == 0 && dy != 0)
                return false;

            if (dx == 0)
                return Math.Abs(dy) <= Length;

            return Math.Abs(dx) <= Length;
        }

        // return the square euclidean distance of a point to the line AB
        public double SquareDistanceToLine(MapPoint point)
        {
            double dx = B.X - A.X;
            double dy = B.Y - A.Y;
            var projection = dy * point.X - dx * point.Y + B.X * A.Y - B.Y * A.X;
            return projection * projection / (dy * dy + dx * dx);
        }
        public IEnumerable<MapPoint> EnumerateValidPoints()
        {
            return EnumerateSet().Where(x => x != null && x.IsInMap());
        }
        private IEnumerable<MapPoint> Raytracing()
        {
            // http://playtechs.blogspot.fr/2007/03/raytracing-on-grid.html

            var dx = Math.Abs(A.X - B.X);
            var dy = Math.Abs(A.Y - B.Y);
            var x = A.X;
            var y = A.Y;
            var n = 1 + dx + dy;
            var vectorX = (B.X > A.X) ? 1 : -1;
            var vectorY = (B.Y > A.Y) ? 1 : -1;
            var error = dx - dy;
            dx *= 2;
            dy *= 2;

            for (; n > 0; --n)
            {
                yield return new MapPoint(x, y);

                if (error > 0)
                {
                    x += vectorX;
                    error -= dy;
                }
                else if (error == 0)
                {
                    x += vectorX;
                    y += vectorY;
                    n--;
                    error += dx - dy;
                }
                else
                {
                    y += vectorY;
                    error += dx;
                }
            }
        }
    }
}
