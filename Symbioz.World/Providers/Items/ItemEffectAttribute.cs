using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Items
{
    public class ItemEffectAttribute : Attribute
    {
        public EffectsEnum Effect
        {
            get;
            set;
        }
        public ItemEffectAttribute(EffectsEnum effect)
        {
            this.Effect = effect;
        }
    }
}
