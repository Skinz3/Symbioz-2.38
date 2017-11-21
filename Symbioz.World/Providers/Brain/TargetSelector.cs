using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain
{
    public class TargetSelector : Attribute
    {
        public SpellCategoryEnum SpellCategory
        {
            get;set;
        }
        public TargetSelector(SpellCategoryEnum category)
        {
            this.SpellCategory = category;
        }
    }
}
