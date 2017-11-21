using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]

    public class SpellLookAttribute : Attribute
    {
        public ushort SpellId
        {
            get;
            set;
        }
        public SpellLookAttribute(ushort spellId)
        {
            this.SpellId = spellId;
        }
    }
}
