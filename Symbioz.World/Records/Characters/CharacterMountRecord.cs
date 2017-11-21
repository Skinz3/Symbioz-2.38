using Symbioz.ORM;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Core;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Providers.Items;
using Symbioz.World.Models.Entities;

namespace Symbioz.World.Records.Characters
{
    [Table("CharactersMounts"), Resettable]
    public class CharacterMountRecord : ITable
    {
        public const string DefaultMountName = "SansNom";

        public const sbyte DisplayedMountLevel = 100;

        public static List<CharacterMountRecord> CharactersMounts = new List<CharacterMountRecord>();

        [Primary]
        public long UId;

        [Update]
        public long CharacterId;

        public bool Sex;

        public bool IsRideable;

        public bool IsWild;

        public bool IsFecondationReady;

        public int ModelId;

        [Update]
        public string Name;

        [Xml, Update]
        public List<EffectInteger> Effects;

        [Ignore]
        public MountRecord Template
        {
            get
            {
                return MountRecord.GetMount(ModelId);
            }
        }
        [Update]
        public bool Setted;

        [Update]
        public bool Toggled;

        public CharacterMountRecord(long uid, long characterId, bool sex, bool isRideable, bool isWild, bool isFecondationReady, int modelId,
            string name, List<EffectInteger> effects, bool setted, bool toggled)
        {
            this.UId = uid;
            this.CharacterId = characterId;
            this.Sex = sex;
            this.IsRideable = isRideable;
            this.IsWild = isWild;
            this.IsFecondationReady = isFecondationReady;
            this.ModelId = modelId;
            this.Name = name;
            this.Effects = effects;
            this.Setted = setted;
            this.Toggled = toggled;
        }
        public MountClientData GetMountClientData()
        {
            return new MountClientData()
            {
                aggressivityMax = 10,
                ancestor = new int[0],
                maturityForAdult = 10,
                behaviors = new int[0],
                boostLimiter = 10,
                boostMax = 10,
                effectList = Effects.ConvertAll<ObjectEffectInteger>(x => x.GetObjectEffect() as ObjectEffectInteger).ToArray(),
                energy = 10,
                energyMax = 100,
                experience = 1,
                experienceForLevel = 2,
                experienceForNextLevel = 3,
                fecondationTime = 1,
                id = UId,
                isFecondationReady = IsFecondationReady,
                isRideable = IsRideable,
                isWild = IsWild,
                level = DisplayedMountLevel,
                love = 1,
                loveMax = 2,
                maturity = 1,
                maxPods = 222,
                model = (uint)ModelId,
                name = Name,
                ownerId = (int)CharacterId,
                reproductionCount = 4,
                reproductionCountMax = 4,
                serenity = 1,
                serenityMax = 2,
                sex = Sex,
                stamina = 1,
                staminaMax = 2,
            };
        }
        public CharacterItemRecord CreateCertificate(Character character)
        {
            ItemRecord template = ItemRecord.GetItem(Template.ItemGId);
            var item = template.GetCharacterItem(CharacterId, 1);
            item.Effects.AddRange(ItemGenerationProvider.GetCertificateEffects(Name, character.Name, (ushort)DisplayedMountLevel,
                (int)UId, (ushort)ModelId));
            return item;
        }
        public static CharacterMountRecord New(CharacterItemRecord item)
        {
            MountRecord template = MountRecord.GetMount(item.GId);
            long uid = CharactersMounts.DynamicPop(x => x.UId);

            return new CharacterMountRecord(uid, item.CharacterId, false, true, false, false,
                template.Id, DefaultMountName, template.Effects.ConvertAll<EffectInteger>(x => x.GenerateEffect() as EffectInteger), false, false);
        }

        public static List<CharacterMountRecord> GetCharacterMounts(long id)
        {
            return CharactersMounts.FindAll(x => x.CharacterId == id);
        }

        [RemoveWhereId]
        public static List<CharacterMountRecord> RemoveAll(long id)
        {
            return CharactersMounts.FindAll(x => x.CharacterId == id);
        }
    }
}
