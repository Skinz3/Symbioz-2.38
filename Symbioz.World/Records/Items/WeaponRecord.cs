using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Items
{
    [Table("Weapons", true, 7)]
    public class WeaponRecord : ITable
    {
        public static List<WeaponRecord> Weapons = new List<WeaponRecord>();

        [Primary]
        public ushort Id;

        public string Name;

        public short CraftXpRatio;

        public short MaxRange;

        public sbyte CriticalHitBonus;

        public short MinRange;

        public short MaxCastPerTurn;

        public bool Etheral;

        [Ignore]
        public ItemRecord Template;

        [Update]
        public ushort AppearanceId;

        public ushort Level;

        public bool Exchangeable;

        public int RealWeight;

        public bool CastTestLos;

        public string Criteria;

        public sbyte CriticalHitProbability;

        public bool TwoHanded;

        public int ItemSetId;

        public bool CastInDiagonal;

        public int Price;

        public short ApCost;

        public bool CastInLine;

        [Xml, Update]
        public List<EffectInstance> Effects;

        public ushort TypeId;

        [Ignore]
        public ItemTypeEnum TypeEnum
        {
            get { return (ItemTypeEnum)TypeId; }
        }

        public WeaponRecord(ushort id, string name, short craftXpRatio, short maxRange, sbyte criticalHitBonus,
            short minRange, short maxCastPerTurn, bool etheral, ushort appearanceId, ushort level,
           bool exchangeable, int realWeight, bool castTestLos, string criteria, sbyte criticalHitProbability,
            bool twoHanded, int itemSetId, bool castInDiagonal, int price, short apCost, bool castInLine,
            List<EffectInstance> effects, ushort typeId)
        {
            this.Id = id;
            this.Name = name;
            this.CraftXpRatio = craftXpRatio;
            this.MaxRange = maxRange;
            this.CriticalHitBonus = criticalHitBonus;
            this.MinRange = minRange;
            this.MaxCastPerTurn = maxCastPerTurn;
            this.Etheral = etheral;
            this.AppearanceId = appearanceId;
            this.Level = level;
            this.Exchangeable = exchangeable;
            this.RealWeight = realWeight;
            this.CastTestLos = castTestLos;
            this.Criteria = criteria;
            this.CriticalHitProbability = criticalHitProbability;
            this.TwoHanded = twoHanded;
            this.ItemSetId = itemSetId;
            this.CastInDiagonal = castInDiagonal;
            this.Price = price;
            this.ApCost = apCost;
            this.CastInLine = castInLine;
            this.Effects = effects;
            this.TypeId = typeId;
            this.Template = ToItemRecord();

            ItemRecord.Items.Add(Template);
        }
        private ItemRecord ToItemRecord()
        {
            return new ItemRecord(Id, Name, TypeId, AppearanceId, Level, Price, RealWeight, Effects, Criteria);
        }
        public static WeaponRecord GetWeapon(ushort id)
        {
            return Weapons.Find(x => x.Id == id);
        }
    }
}
