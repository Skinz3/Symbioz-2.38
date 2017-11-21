using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("NpcActions.d2o", "NpcAction"), Table("NpcActions")]
    public class NpcActions  //: ID2OTable
    {
        [D2OField("id")]
        public int id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("realId")]
        public int RealId;


    }
}
