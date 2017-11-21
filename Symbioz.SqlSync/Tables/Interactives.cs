using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Interactives.d2o", "Interactive"), Table("Interactives")]
    public class Interactives //: ID2OTable
    {
        [D2OField("id")]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("actionId")]
        public int ActionId;
    }
}
