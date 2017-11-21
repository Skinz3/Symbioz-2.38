using Symbioz.ORM;
using Symbioz.World.Models.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Core;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Types;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Providers.Items;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records.Characters;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.Protocol.Enums;

namespace Symbioz.World.Records.Items
{
    [Table("Items", true, 6)]
    public class ItemRecord : ITable
    {
        public static List<ItemRecord> Items = new List<ItemRecord>();

        [Primary]
        public ushort Id;

        public string Name;

        public ushort TypeId;

        [Ignore]
        public ItemTypeEnum TypeEnum { get { return (ItemTypeEnum)TypeId; } }

        [Update]
        public ushort AppearanceId;

        public ushort Level;

        public int Price;

        public int Weight;

        [Xml, Update]
        public List<EffectInstance> Effects;

        public string Criteria;

        public bool HasSet { get { return ItemSetRecord.GetItemSet(Id) != null; } }

        public ItemSetRecord ItemSet { get { return ItemSetRecord.GetItemSet(Id); } }

        public bool Weapon { get { return WeaponRecord.Weapons.Find(x => x.Id == Id) != null; } }

        public ItemRecord(ushort id, string name, ushort typeId, ushort apparenceId, ushort level,
            int price, int weight, List<EffectInstance> effects, string criteria)
        {
            this.Id = id;
            this.Name = name;
            this.TypeId = typeId;
            this.AppearanceId = apparenceId;
            this.Level = level;
            this.Price = price;
            this.Weight = weight;
            this.Effects = effects;
            this.Criteria = criteria;

        }
        public CharacterItemRecord GetCharacterItem(long characterId, uint quantity, bool perfect = false)
        {
            var effects = Effects.ConvertAll<Effect>(x => x.GenerateEffect(perfect));
            effects.RemoveAll(x => x == null);
            var item = new CharacterItemRecord(characterId, 0, Id, (byte)CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED,
                quantity, effects, AppearanceId);
            return item;
        }
        public int GetPrice(bool levelPrice)
        {
            if (levelPrice)
            {
                int result = (int)((double)Level / (double)2);
                return result == 0 ? 1 : result;
            }
            else
            {
                return Price;
            }
        }
        public ObjectItemToSellInNpcShop GetObjectItemToSellInNpcShop(bool levelPrice)
        {
            return new ObjectItemToSellInNpcShop(Id,
                Effects.ConvertAll<ObjectEffect>(w => w.GetTemplateObjectEffect()).ToArray(),
                (uint)GetPrice(levelPrice), string.Empty);
        }
        public ObjectItemNotInContainer GetObjectItemNotInContainer(uint uid, uint quantity)
        {
            return new ObjectItemNotInContainer(Id, Effects.ConvertAll<ObjectEffect>(x => x.GetTemplateObjectEffect()).ToArray(),
                uid, quantity);
        }
        public ObjectItemInformationWithQuantity GetObjectItemInformationWithQuantity(uint quantity)
        {
            return new ObjectItemInformationWithQuantity(Id, Effects.ConvertAll<ObjectEffect>(x => x.GetTemplateObjectEffect()).ToArray(),
                quantity);
        }
        public static ItemRecord GetItem(ushort gid)
        {
            return Items.Find(x => x.Id == gid);
        }
        public static ItemRecord[] GetItems(ItemTypeEnum type)
        {
            return Items.FindAll(x => x.TypeEnum == type).ToArray();
        }
        public static ItemRecord RandomItem(ItemTypeEnum type)
        {
            return GetItems(type).Random();
        }
        public static ItemRecord RandomItem(Predicate<ItemRecord> predicate)
        {
            return Items.FindAll(predicate).Random();
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
