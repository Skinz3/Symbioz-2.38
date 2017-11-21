using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Marks.Shapes
{
    public class MarkShape
    {
        public GameActionMarkCellsTypeEnum Shape
        {
            get
            {
                return GameActionMarkCellsTypeEnum.CELLS_SQUARE;
            }
        }

        public Fight Fight
        {
            get;
            private set;
        }
        public MapPoint Point
        {
            get;
            private set;
        }

        public byte Size
        {
            get;
            private set;
        }
        public Color Color
        {
            get;
            set;
        }
        public MarkShape(Fight fight, MapPoint point, Color color)
        {
            this.Fight = fight;
            this.Point = point;
            this.Color = color;
        }

        public GameActionMarkedCell GetGameActionMarkedCell()
        {
            return new GameActionMarkedCell((ushort)this.Point.CellId, (sbyte)this.Size, this.Color.ToArgb() & 16777215, (sbyte)this.Shape);
        }

    }
}