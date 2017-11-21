using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Spells
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomSpellHandlerAttribute : Attribute
    {
        public ushort SpellId
        {
            get;
            private set;
        }
        public CustomSpellHandlerAttribute(ushort spellId)
        {
            this.SpellId = spellId;
        }
    }
}
