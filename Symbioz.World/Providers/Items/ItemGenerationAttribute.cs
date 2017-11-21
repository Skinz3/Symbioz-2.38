using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Items
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ItemGenerationAttribute : Attribute
    {
        public ItemTypeEnum Type;

        public ItemGenerationAttribute(ItemTypeEnum type)
        {
            this.Type = type;
        }
    }
}
