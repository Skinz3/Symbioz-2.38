using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Ornaments.d2o", "Ornament"), Table("Ornaments")]
    public class Ornaments : ID2OTable
    {
        [Primary]
        [D2OField("id")]
        public string Id;

        [D2OField("nameId"),i18n]
        public string Name;
    }
}
