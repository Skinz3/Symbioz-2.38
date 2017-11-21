using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Monsters.d2o", "Monster"), Table("Monsters")]
    public class Monsters  //: ID2OTable
    {
        [D2OField("id"),Primary]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("look")]
        public string Look;

        [D2OField("isBoss")]
        public bool IsBoss;

        [D2OField("isMiniBoss")]
        public bool IsMiniBoss;

        [D2OField("race")]
        public int Race;

        [D2OField("useSummonSlot")]
        public bool UseSummonSlot;

        [D2OField("useBombSlot")]
        public bool UseBombSlot;

        [D2OField("drops")]
        public string Drops;

        [D2OField("spells")]
        public List<long> Spells = new List<long>();

        [D2OField("grades")]
        public string Grades;

        public int MinDroppedKamas;

        public int MaxDroppedKamas;

        public int Power;

        public string BehaviorName;
    }
}
