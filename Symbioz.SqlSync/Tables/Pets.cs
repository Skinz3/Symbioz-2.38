using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Pets.d2o", "Pet"), Table("Pets")]
    public class Pets  //: ID2OTable
    {
        [D2OField("id")]
        public int Id;

        [D2OField("possibleEffects")]
        public string Effects;
    }
}
