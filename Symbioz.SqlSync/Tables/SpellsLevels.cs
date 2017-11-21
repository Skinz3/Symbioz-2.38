using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("SpellLevels.d2o", "SpellLevel"), Table("SpellsLevels")]
    public class SpellsLevels //: ID2OTable
    {
        [D2OField("id"), Primary]
        public int Id;

        [D2OField("spellId")]
        public ushort SpellId;

        [D2OField("grade")]
        public sbyte Grade;

        [D2OField("apCost")]
        public short ApCost;

        [D2OField("minRange")]
        public short MinRange;

        [D2OField("range")]
        public short MaxRange;

        [D2OField("castInLine")]
        public bool CastInLine;

        [D2OField("castInDiagonal")]
        public bool CastInDiagonal;

        [D2OField("castTestLos")]
        public bool CastTestLos;

        [D2OField("criticalHitProbability")]
        public short CriticalHitProbability;

        [D2OField("needFreeCell")]
        public bool NeedFreeCell;

        [D2OField("needTakenCell")]
        public bool NeedTakenCell;

        [D2OField("needFreeTrapCell")]
        public bool NeedFreeTrapCell;

        [D2OField("rangeCanBeBoosted")]
        public bool RangeCanBeBoosted;

        [D2OField("maxStack")]
        public short MaxStacks;

        [D2OField("maxCastPerTurn")]
        public short MaxCastPerTurn;

        [D2OField("maxCastPerTarget")]
        public short MaxCastPerTarget;

        [D2OField("minCastInterval")]
        public short MinCastInterval;

        [D2OField("initialCooldown")]
        public short InitialCooldown;

        [D2OField("globalCooldown")]
        public short GlobalCooldown;

        [D2OField("statesRequired")]
        public List<short> StatesRequired = new List<short>();

        [D2OField("statesForbidden")]
        public List<short> StatesForbidden = new List<short>();

        [D2OField("effects")]
        public string Effects;

        [D2OField("criticalEffect")]
        public string CriticalEffects;


















    }
}
