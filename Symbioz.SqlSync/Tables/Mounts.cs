using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Mounts.d2o", "Mount"), Table("Mounts")]
    public class Mounts  //: ID2OTable
    {
        [D2OField("id")]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("look")]
        public string Look;
    }
}
