using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World.Models.Effects
{
    public abstract class Effect
    {
        [YAXDontSerialize]
        public EffectsEnum EffectEnum
        {
            get
            {
                return (EffectsEnum)EffectId;
            }
        }

        public ushort EffectId
        {
            get;
            set;
        }

        public Effect()
        {
        }

        public Effect(ushort effectId)
        {
            this.EffectId = effectId;
        }

        public abstract ObjectEffect GetObjectEffect();




    }
}
