using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Providers.Items;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Core;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Items
{
    public abstract class AbstractItem
    {
        [Ignore]
        private ItemRecord m_template;

        [Ignore]
        public ItemRecord Template
        {
            get
            {
                if (m_template == null)
                {
                    m_template = ItemRecord.GetItem(GId);
                    return m_template;
                }
                else
                {
                    return m_template;
                }
            }
        }

        [Primary]
        public uint UId;

        public ushort GId;

        [Update]
        public byte Position;

        [Ignore]
        public CharacterInventoryPositionEnum PositionEnum { get { return (CharacterInventoryPositionEnum)Position; } set { Position = (byte)value; } }

        [Update]
        public uint Quantity;

        [Xml, Update]
        public List<Effect> Effects;

        [Update]
        public ushort AppearanceId;

        public List<T> GetEffects<T>() where T : Effect
        {
            return Effects.OfType<T>().ToList();
        }
        public List<T> GetEffects<T>(EffectsEnum effect) where T : Effect
        {
            return GetEffects<T>().FindAll(x => x.EffectEnum == effect);
        }
        public T FirstEffect<T>(EffectsEnum effect) where T : Effect
        {
            return GetEffects<T>().FirstOrDefault(x => x.EffectEnum == effect);
        }
        public T FirstEffect<T>() where T : Effect
        {
            return GetEffects<T>().FirstOrDefault();
        }
        public bool HasEffect<T>() where T : Effect
        {
            return FirstEffect<T>() != null;
        }
        public bool HasEffect<T>(EffectsEnum effect) where T : Effect
        {
            return FirstEffect<T>(effect) != null;
        }
        /// <summary>
        /// Forgemagie d'arme
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="percentage"></param>
        public virtual void Mage(EffectsEnum effectEnum, byte percentage)
        {
            if (!Template.Weapon)
            {
                throw new Exception("You can only mage weapons...");
            }
            var effects = GetEffects<Effect>(EffectsEnum.Effect_DamageNeutral);

            foreach (var effect in effects)
            {
                if (effect is EffectDice)
                {
                    var dice = effect as EffectDice;
                    dice.Min = (ushort)((int)(dice.Min)).GetPercentageOf(percentage);
                    dice.Max = (ushort)((int)(dice.Max)).GetPercentageOf(percentage);
                    dice.Const = 0;

                }
                if (effect is EffectInteger)
                {
                    var integer = effect as EffectInteger;
                    integer.Value = (ushort)((int)(integer.Value)).GetPercentageOf(percentage);
                }

                effect.EffectId = (ushort)effectEnum;
            }



        }
        public bool IsAssociated
        {
            get
            {
                return HasEffect<EffectDice>(EffectsEnum.Effect_LivingObjectId) ||
                HasEffect<EffectInteger>(EffectsEnum.Effect_ChangeApparence1176) ||
                HasEffect<Effect>(EffectsEnum.Effect_ChangeAppearence1151);
            }
        }

        public bool IsValidMountCertificate
        {
            get
            {
                return HasEffect<EffectString>(EffectsEnum.Effect_MountName) && HasEffect<EffectString>(EffectsEnum.Effect_MountOwner) &&
                    HasEffect<EffectDuration>(EffectsEnum.Effect_MountValidity) && HasEffect<EffectInteger>(EffectsEnum.Effect_Level) &&
                    HasEffect<EffectMount>(EffectsEnum.Effect_MountDefinition);
            }
        }
        public virtual void AddEffectInteger(EffectsEnum effect, ushort value)
        {
            EffectInteger current = FirstEffect<EffectInteger>(effect);

            if (current == null)
            {
                Effects.Add(new EffectInteger((ushort)effect, value));
            }
            else
            {
                current.Value += value;
            }
        }
        public virtual void AddEffectDice(EffectsEnum effect, ushort min, ushort max, ushort value)
        {
            Effects.Add(new EffectDice((ushort)effect, min, max, value));
        }
        public virtual void AddEffect(Effect effect)
        {
            this.Effects.Add(effect);
        }
        public virtual void RemoveAllEffects(EffectsEnum effect)
        {
            this.Effects.RemoveAll(x => x.EffectEnum == effect);
        }

        public ObjectItem GetObjectItem()
        {
            return new ObjectItem(Position, GId, GetObjectEffects(), UId, Quantity);
        }
        public ObjectItemQuantity GetObjectItemQuantity()
        {
            return new ObjectItemQuantity(UId, Quantity);
        }
        public ObjectEffect[] GetObjectEffects()
        {
            ObjectEffect[] effects = new ObjectEffect[Effects.Count];
            for (int i = 0; i < Effects.Count; i++)
            {
                effects[i] = Effects[i].GetObjectEffect();
            }
            return effects;
        }

        public abstract AbstractItem CloneWithUID();

        public abstract AbstractItem CloneWithoutUID();

        public CharacterItemRecord ToCharacterItemRecord(long characterId)
        {
            return new CharacterItemRecord(characterId, UId, GId, Position, Quantity, new List<Effect>(Effects), AppearanceId);
        }
        public BankItemRecord ToBankItemRecord(int accountId)
        {
            return new BankItemRecord(accountId, UId, GId, Position, Quantity, new List<Effect>(Effects), AppearanceId);
        }
        public BidShopItemRecord ToBidShopItemRecord(int bidshopId, int accountId, uint price)
        {
            return new BidShopItemRecord(bidshopId, accountId, price, ItemUIdPopper.PopUID(), GId, Position, Quantity, new List<Effect>(Effects), AppearanceId);
        }
    }
}
