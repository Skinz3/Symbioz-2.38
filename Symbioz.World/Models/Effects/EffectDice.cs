using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Effects
{
    public class EffectDice : Effect
    {
        public ushort Min
        {
            get;
            set;
        }

        public ushort Max
        {
            get;
            set;
        }

        public ushort Const
        {
            get;
            set;
        }

        public EffectDice()
        {
        }

        public EffectDice(ushort effectId, ushort min, ushort max, ushort @const)
            : base(effectId)
        {
            this.Min = min;
            this.Max = max;
            this.Const = @const;
        }

        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectDice(EffectId, Min, Max, Const);
        }
        public override bool Equals(object obj)
        {
            return obj is EffectDice ? this.Equals(obj as EffectDice) : false;
        }
        public bool Equals(EffectDice effect)
        {
            return this.EffectId == effect.EffectId && effect.Min == Min && effect.Max == Max && effect.Const == Const;
        }

    }
}
