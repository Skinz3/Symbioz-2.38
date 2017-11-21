using Symbioz.ORM;
using Symbioz.World.Models.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Core;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Items
{
    [Table("ItemSets", true)]
    public class ItemSetRecord : ITable
    {
        public static List<ItemSetRecord> ItemsSets = new List<ItemSetRecord>();

        [Primary]
        public int Id;

        public string Name;

        public List<ushort> Items;

        public ItemSetEffects Effects;

        public ItemSetRecord(int id, string name, List<ushort> items, ItemSetEffects effects)
        {
            this.Id = id;
            this.Name = name;
            this.Items = items;
            this.Effects = effects;
        }

        public List<Effect> GetSetEffects(int itemCount)
        {
            if (Effects.SetEffects.Count >= itemCount)
                return Effects.SetEffects[itemCount - 1].ConvertAll<Effect>(x => x.GenerateEffect());
            else
                return new List<Effect>();

        }

        public static ItemSetRecord GetItemSet(ushort itemGID)
        {
            return ItemsSets.Find(x => x.Items.Contains(itemGID));
        }
    }
    public class ItemSetEffects
    {
        public List<List<EffectInstance>> SetEffects = new List<List<EffectInstance>>();

        public static ItemSetEffects Deserialize(string str)
        {
            ItemSetEffects itemSetEffects = new ItemSetEffects();


            foreach (var item in str.Split('|'))
            {
                List<EffectInstance> effects = new List<EffectInstance>();
                foreach (var subItem in item.Split(','))
                {
                    if (subItem != string.Empty)
                        effects.Add(subItem.XMLDeserialize<EffectInstance>());
                }

                itemSetEffects.SetEffects.Add(effects);

            }
            itemSetEffects.SetEffects.Remove(itemSetEffects.SetEffects.Last());
            return itemSetEffects;
        }
    }
}
