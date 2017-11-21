using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Appearances.d2o", "Appearance"), Table("Appearances")]
    public class Appearence //: ID2OTable
    {
        [D2OField("id")]
        public int Id;

        [D2OField("type")]
        public string Type;

        [D2OField("data")]
        public string Datas;
    }
}
