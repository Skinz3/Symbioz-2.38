using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("MapPositions.d2o", "MapPosition"), Table("MapPositions")]
    public class MapPositions  //: ID2OTable
    {
        [D2OField("id"),Primary]
        public int Id;

        [D2OField("posX")]
        public int X;

        [D2OField("posY")]
        public int Y;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("outdoor")]
        public bool Outdoor;

        [D2OField("capabilities")]
        public int Capabilities;
    }
}
