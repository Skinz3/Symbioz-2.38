using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Effects
{
    public class EffectDuration : Effect
    {
        public ushort Days { get; set; }

        public sbyte Hours { get; set; }

        public sbyte Minutes { get; set; }

        public EffectDuration() { }

        public EffectDuration(ushort effectId,ushort days,sbyte hours,sbyte minutes)
            : base(effectId)
        {
            this.Days = days;
            this.Hours = hours;
            this.Minutes = minutes;
        }
        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectDuration(EffectId, Days, Hours, Minutes);
        }

        public override bool Equals(object obj)
        {
            return obj is EffectDuration ? this.Equals(obj as EffectDuration) : false;
        }
        public bool Equals(EffectDuration effect)
        {
            return this.EffectId == effect.EffectId && this.Days == effect.Days && this.Hours == effect.Hours && this.Minutes == effect.Minutes;
        }
    }
}
