using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Items
{
    public class ItemUseAttribute : Attribute
    {
        public EffectsEnum? Effect { get; set; }

        public ushort? GId { get; set; }

        public ItemTypeEnum? ItemType
        {
            get;
            set;
        }

        public ItemUseAttribute(EffectsEnum effect)
        {
            this.Effect = effect;
        }
        public ItemUseAttribute(ushort gid)
        {
            this.GId = gid;
        }
        public ItemUseAttribute(ItemTypeEnum type)
        {
            this.ItemType = type;
        }
    }
}
