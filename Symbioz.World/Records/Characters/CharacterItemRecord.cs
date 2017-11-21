using Symbioz.ORM;
using System;
using Symbioz.Protocol.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Effects;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Items;
using Symbioz.Core;
using Symbioz.World.Providers.Items;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Network;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records.Items;

namespace Symbioz.World.Records.Characters
{
    [Table("CharactersItems"), Resettable]
    public class CharacterItemRecord : AbstractItem, ITable
    {
        public static List<CharacterItemRecord> CharactersItems = new List<CharacterItemRecord>();

        [Update]
        public long CharacterId;

        public CharacterItemRecord(long characterId, uint uid, ushort gid, byte position,
            uint quantity, List<Effect> effects, ushort appearanceId)
        {
            this.UId = uid;
            this.GId = gid;
            this.Position = position;
            this.CharacterId = characterId;
            this.Quantity = quantity;
            this.Effects = effects;
            this.AppearanceId = appearanceId;
        }

        public override AbstractItem CloneWithoutUID()
        {
            return new CharacterItemRecord(CharacterId, ItemUIdPopper.PopUID(), GId, Position, Quantity, new List<Effect>(Effects), AppearanceId);
        }
        public override AbstractItem CloneWithUID()
        {
            return new CharacterItemRecord(CharacterId, UId, GId, Position, Quantity, Effects, AppearanceId);
        }
        public CharacterItemRecord Cut(uint quantity, CharacterInventoryPositionEnum newItempos)
        {
            CharacterItemRecord newItem = (CharacterItemRecord)this.CloneWithoutUID();

            this.PositionEnum = newItempos;
            this.Quantity = quantity;
            newItem.Quantity -= quantity;

            this.UpdateElement();

            return newItem;

        }
        public bool IsEquiped()
        {
            return PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED;
        }
        public override void AddEffectInteger(EffectsEnum effect, ushort value)
        {
            base.AddEffectInteger(effect, value);
            this.UpdateElement();
        }
        public override void Mage(EffectsEnum effectEnum, byte percentage)
        {
            base.Mage(effectEnum, percentage);
            this.UpdateElement();
        }
        public override void AddEffect(Effect effect)
        {
            Effects.Add(effect);
            this.UpdateElement();
        }
        public override void RemoveAllEffects(EffectsEnum effect)
        {
            Effects.RemoveAll(x => x.EffectEnum == effect);
            this.UpdateElement();
        }
        public override void AddEffectDice(EffectsEnum effect, ushort min, ushort max, ushort value)
        {
            Effects.Add(new EffectDice((ushort)effect, min, max, value));
            this.UpdateElement();
        }
        public ObjectItemNotInContainer GetObjectItemNotInContainer()
        {
            return new ObjectItemNotInContainer(GId, Effects.ConvertAll<ObjectEffect>(x => x.GetObjectEffect()).ToArray(), UId, Quantity);
        }
        public static List<CharacterItemRecord> GetCharacterItems(long characterId)
        {
            return CharactersItems.FindAll(x => x.CharacterId == characterId);
        }

        [RemoveWhereId]
        public static List<CharacterItemRecord> RemoveAll(long id)
        {
            return CharactersItems.FindAll(x => x.CharacterId == id);
        }

        public override string ToString()
        {
            return "(" + UId + ") " + Template.Name;
        }

        public CharacterItemRecord ToMimicry(int newskinid, ushort appearanceId)
        {
            CharacterItemRecord newItem = (CharacterItemRecord)this.CloneWithoutUID();
            newItem.AddEffectInteger(EffectsEnum.Effect_ChangeAppearence1151, (ushort)newskinid);
            newItem.Quantity = 1;
            newItem.AppearanceId = appearanceId;
            return newItem;
        }

        public bool CanBeExchanged()
        {
            return !IsValidMountCertificate;
        }

        /// <summary>
        /// Warning !! this method is experimental, there is known issues dues to architectural problems: 
        /// the Item wont be initialize by ItemGenerationProvider.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        public static void AddQuietCharacterItem(CharacterRecord character, CharacterItemRecord item)
        {
            lock (CharactersItems)
            {
                bool isOnline = WorldServer.Instance.IsOnline(character.Id);

                if (isOnline)
                {
                    Logger.Write<CharacterItemRecord>("Cannot attribute " + item.Template.Name + " to " + character.Name + " , the character is online!", ConsoleColor.Red);
                }
                else
                {
                    item.CharacterId = character.Id;
                    var items = CharacterItemRecord.GetCharacterItems(character.Id);
                    var same = items.FirstOrDefault(x => x.GId == item.GId && x.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED && Inventory.SameEffects(x.Effects, item.Effects));

                    if (same != null)
                    {
                        same.Quantity += item.Quantity;
                        same.UpdateElement();
                    }
                    else
                    {
                        item.UId = ItemUIdPopper.PopUID();
                        item.AddElement();
                    }
                }
            }
        }
    }
}
