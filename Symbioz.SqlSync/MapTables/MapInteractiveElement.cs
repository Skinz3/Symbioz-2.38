using Symbioz.ORM;
using Symbioz.Tools.DLM;
using Symbioz.Tools.ELE;
using Symbioz.Tools.ELE.Repertory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.MapTables
{
    [Table("MapInteractiveElements")]
    public class MapInteractiveElement : ITable
    {
        public int ElementId;

        public int MapId;

        public ushort CellId;

        public int ElementType = -1; // we dunno dat

        public int GfxId;

        public int GfxBonesId;



    }
}
