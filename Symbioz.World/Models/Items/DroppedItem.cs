using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Items
{
    public class DroppedItem
    {
        public ushort ItemGId
        {
            get;
            set;
        }
        public uint Amount
        {
            get;
            set;
        }
        public DroppedItem(ushort itemGId, uint amount)
        {
            this.ItemGId = itemGId;
            this.Amount = amount;
        }
    }
}
