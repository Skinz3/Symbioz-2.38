using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("MapScrollActions.d2o", "MapScrollAction"), Table("ScrollActions")]
    public class MapScrollAction //: ID2OTable
    {
        [D2OField("id")]
        public int Id;

        [D2OField("rightMapId")]
        public int RightMapId;

        [D2OField("bottomMapId")]
        public int BottomMapId;

        [D2OField("leftMapId")]
        public int LeftMapId;

        [D2OField("topMapId")]
        public int TopMapId;

    }
}
