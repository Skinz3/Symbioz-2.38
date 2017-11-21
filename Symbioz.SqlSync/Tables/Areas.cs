using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Areas.d2o", "Area"), Table("Areas")]
    public class Areas //: ID2OTable
    {
        [D2OField("id"),Primary]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("superAreaId")]
        public int SuperAreaId;

        [D2OField("containHouses")]
        public bool ContainHouses;

        [D2OField("containPaddocks")]
        public bool ContainPaddocks;

        [D2OField("bounds")]
        public object Bounds;

        [D2OField("worldmapId")]
        public int WorldMapId;

        [D2OField("hasWorldMap")]
        public bool HasWorldMap;
    }
}
