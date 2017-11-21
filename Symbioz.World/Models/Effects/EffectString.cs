using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Effects
{
    public class EffectString : Effect
    {
        public string Value
        {
            get;
            set;
        }

        public EffectString() { }

        public EffectString(ushort effectId, string value)
            : base(effectId)
        {
            this.Value = value;
        }

        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectString(EffectId, Value);
        }

        public override bool Equals(object obj)
        {
            return obj is EffectString ? this.Equals(obj as EffectString) : false;
        }
        public bool Equals(EffectString effect)
        {
            return this.EffectId == effect.EffectId && this.Value == effect.Value;
        }
    }
}
