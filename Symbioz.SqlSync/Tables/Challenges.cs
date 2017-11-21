using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Challenge.d2o", "Challenge"), Table("Challenges")]
    public class Challenges// : ID2OTable
    {
        [D2OField("id")]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("descriptionId"),i18n]
        public string Description;

        [D2OField("incompatibleChallenges")]
        public List<int> IncompatibleChallenges;
    }
}
