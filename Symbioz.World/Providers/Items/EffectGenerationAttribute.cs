using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Items
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EffectGenerationAttribute : Attribute
    {
        public EffectsEnum Effect;

        public EffectGenerationAttribute(EffectsEnum effect)
        {
            this.Effect = effect;
        }
    }
}
