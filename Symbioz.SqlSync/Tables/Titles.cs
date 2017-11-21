using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Titles.d2o", "Title"), Table("Titles")]
    public class Titles //: ID2OTable
    {
        [D2OField("id"),Primary]
        public int Id;

        [D2OField("nameMaleId"), i18n]
        public string NameMale;

        [D2OField("nameFemaleId"), i18n]
        public string NameFemale;

        [D2OField("categoryId")]
        public int CategoryId;

        [D2OField("visible")]
        public bool Visible;
    }
}
