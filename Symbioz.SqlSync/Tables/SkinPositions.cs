using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("SkinPositions.d2o", "SkinPosition"), Table("SkinPositions")]
    public class SkinPositions// : ID2OTable
    {
        [D2OField("id"),Primary]
        public int Id;

        [D2OField("transformation")]
        public string Transformation;

        [D2OField("clip")]
        public string Clip;

        [D2OField("skin")]
        public string Skin;



    }
}
