using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Heads.d2o","Head"), Table("Heads")]
    public class Heads  //: ID2OTable 
    {
        [D2OField("id"),Primary]
        public int Id;

        [D2OField("skins")]
        public ushort SkinId;

    }
}
