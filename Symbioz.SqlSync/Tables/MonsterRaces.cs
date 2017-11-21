using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("MonsterRaces.d2o","MonsterRace"),Table("MonsterRaces")]
    public class MonsterRaces// : ID2OTable
    {
        [D2OField("id"),Primary]
        public int Id;

        [D2OField("nameId"),i18n]
        public string Name;

        [D2OField("superRaceId")]
        public short SuperRaceId;
    }
}
