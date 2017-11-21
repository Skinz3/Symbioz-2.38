using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Maps
{
    public class Point
    {
        public int X;

        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public Point()
        {

        }
        public override bool Equals(object obj)
        {
            var pt = obj as Point;

            if (pt != null)
            {
                return X == pt.X && Y == pt.Y;
            }
            else
            {
                return false;
            }
        }
    }
}
