using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Effects
{
    public class EffectInteger : Effect
    {
        public ushort Value { get; set; }

        public EffectInteger() { }

        public EffectInteger(ushort effectId, ushort value)
            : base(effectId)
        {
            this.Value = value;
        }
        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectInteger(EffectId, Value);
        }
        public override bool Equals(object obj)
        {
            return obj is EffectInteger ? this.Equals(obj as EffectInteger) : false;
        }
        public bool Equals(EffectInteger effect)
        {
            return this.EffectId == effect.EffectId && effect.Value == this.Value;
        }
    }
}
