using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("SkillNames.d2o", "SkillName"), Table("SkillNames")]
    public class SkillsName //: ID2OTable
    {
        [D2OField("id")]
        public int Id;

        [D2OField("nameId"),i18n]
        public string Name;
    }
}
