using Symbioz.SqlSync.Attributes;
using Symbioz.Tools.D2O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.D2OTypes
{
    [D2oType("EffectInstanceDice")]
    public class EffectInstance
    {
        [D2OField("targetMask")]
        public string TargetMask { get; set; }

        [D2OField("effectUid")]
        public string EffectUID { get; set; }

        [D2OField("rawZone")]
        public string RawZone { get; set; }

        [D2OField("effectId")]
        public ushort EffectId { get; set; }
    
        [D2OField("diceNum")]
        public ushort DiceMin { get; set; }

        [D2OField("duration")]
        public int Duration { get; set; }

        [D2OField("diceSide")]
        public ushort DiceMax { get; set; }

        [D2OField("value")]
        public int Value { get; set; }

        [D2OField("delay")]
        public short Delay { get; set; }

        [D2OField("random")]
        public sbyte Random { get; set; }

        [D2OField("triggers")]
        public string Triggers { get; set; }

        [D2OField("effectElement")]
        public short EffectElement { get; set; }
    }
}
