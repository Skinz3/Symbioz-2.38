using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.D2OTypes
{
    [D2oType("Rectangle")]
    public class Rectangle
    {
        [D2OField("x")]
        public int X { get; set; }

        [D2OField("y")]
        public int Y { get; set; }

        [D2OField("width")]
        public int Width { get; set; }

        [D2OField("height")]
        public int Height { get; set; }
    }
}
