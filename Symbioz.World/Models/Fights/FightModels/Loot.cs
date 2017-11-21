using Symbioz.Protocol.Types;
using Symbioz.World.Models.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.FightModels
{
    public class Loot
    {
        private readonly System.Collections.Generic.Dictionary<int, DroppedItem> m_items = new System.Collections.Generic.Dictionary<int, DroppedItem>();
        public IReadOnlyDictionary<int, DroppedItem> Items
        {
            get
            {
                return new ReadOnlyDictionary<int, DroppedItem>(this.m_items);
            }
        }
        public uint Kamas
        {
            get;
            set;
        }
        public void AddItem(ushort itemId)
        {
            this.AddItem(itemId, 1u);
        }
        public void AddItem(ushort itemId, uint amount)
        {
            if (this.m_items.ContainsKey(itemId))
            {
                this.m_items[itemId].Amount += 1u;
            }
            else
            {
                this.m_items.Add(itemId, new DroppedItem(itemId, amount));
            }
        }
        public void AddItem(DroppedItem item)
        {
            if (this.m_items.ContainsKey(item.ItemGId))
            {
                this.m_items[item.ItemGId].Amount += item.Amount;
            }
            else
            {
                this.m_items.Add(item.ItemGId, new DroppedItem(item.ItemGId, item.Amount));
            }
        }
        public FightLoot GetFightLoot()
        {
            return new FightLoot(this.m_items.Values.SelectMany((DroppedItem entry
                ) => new ushort[]
			{
				(ushort)entry.ItemGId,
				(ushort)entry.Amount
			}).ToArray(), this.Kamas);
        }
    }
}
