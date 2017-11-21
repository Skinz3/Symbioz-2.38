using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Effects.d2o", "Effect"), Table("Effects")]
    public class Effects  //: ID2OTable
    {
        [D2OField("id"), Primary]
        public int id;

        [D2OField("descriptionId"), i18n]
        public string Description;

        [D2OField("characteristic")]
        public string Characteristic;

        [D2OField("category")]
        public short Category;


    }
}
