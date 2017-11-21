using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Items
{
    public class ItemGenerationProvider
    {
        static Logger logger = new Logger();

        private static Dictionary<ItemGenerationAttribute[], MethodInfo> Handlers = new Dictionary<ItemGenerationAttribute[], MethodInfo>();

        [StartupInvoke(StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var method in typeof(ItemGenerationProvider).GetMethods())
            {
                ItemGenerationAttribute[] attributes = method.GetCustomAttributes<ItemGenerationAttribute>().ToArray();

                if (attributes.Length > 0)
                    Handlers.Add(attributes, method);
            }
        }
        public static bool IsHandled(ItemTypeEnum type)
        {
            foreach (var handler in Handlers)
            {
                var value = handler.Key.FirstOrDefault(x => x.Type == type);

                if (value != null)
                {
                    return true;
                }

            }
            return false;
        }
        private static MethodInfo GetHandler(ItemTypeEnum type)
        {
            foreach (var handler in Handlers)
            {
                var value = handler.Key.FirstOrDefault(x => x.Type == type);

                if (value != null)
                {
                    return handler.Value;
                }
            }
            return null;
        }
        public static void InitItem(CharacterItemRecord item, Character character)
        {
            var handler = GetHandler(item.Template.TypeEnum);

            if (handler != null)
            {
                handler.Invoke(null, new object[] { item, character });
            }
            else
            {
                logger.Error(((ItemTypeEnum)item.Template.TypeId) + " cannot be handled...");
            }
        }

        [ItemGeneration(ItemTypeEnum.CERTIFICAT_DE_DRAGODINDE)]
        [ItemGeneration(ItemTypeEnum.CERTIFICAT_DE_MULDO)]
        public static void InitCertificate(CharacterItemRecord item, Character character)
        {
            if (!item.IsValidMountCertificate)
            {
                CharacterMountRecord record = CharacterMountRecord.New(item);
                record.AddElement();
                character.Inventory.Mounts.Add(record);
                item.Effects.AddRange(GetCertificateEffects(record.Name, character.Name, (ushort)CharacterMountRecord.DisplayedMountLevel,
                    (int)record.UId, (ushort)record.ModelId));
            }
        }
        public static Effect[] GetCertificateEffects(string mountName, string characterName, ushort mountLevel, int mountUID, ushort modelId)
        {
            List<Effect> effects = new List<Effect>();
            effects.Add(new EffectString((ushort)EffectsEnum.Effect_MountName, mountName));
            effects.Add(new EffectString((ushort)EffectsEnum.Effect_MountOwner, characterName));
            effects.Add(new EffectDuration((ushort)EffectsEnum.Effect_MountValidity, 3, 21, 40));
            effects.Add(new EffectInteger((ushort)EffectsEnum.Effect_Level, (ushort)mountLevel));
            effects.Add(new EffectMount((ushort)EffectsEnum.Effect_MountDefinition, mountUID, 40, modelId));
            return effects.ToArray();
        }
        [ItemGeneration(ItemTypeEnum.OBJET_VIVANT)]
        public static void InitLivingObject(CharacterItemRecord item, Character character)
        {
            if (item.Effects.Count == 3)
            {
                LivingObjectRecord livingObjectDatas = LivingObjectRecord.GetLivingObjectDatas(item.GId);

                if (livingObjectDatas != null)
                {
                    item.Effects.Clear();

                    item.Effects.Add(new EffectInteger((ushort)EffectsEnum.Effect_LivingObjectLevel, 361));
                    item.Effects.Add(new EffectInteger((ushort)EffectsEnum.Effect_LivingObjectCategory, livingObjectDatas.ItemType));
                    item.Effects.Add(new EffectInteger((ushort)EffectsEnum.Effect_LivingObjectMood, 0));
                    item.Effects.Add(new EffectInteger((ushort)EffectsEnum.Effect_LivingObjectSkin, 1));

                    if (livingObjectDatas.Skinnable)
                        item.AppearanceId = livingObjectDatas.Skins.FirstOrDefault();
                }
                else
                {
                    logger.Error("Cannot initialize living object, no template:  " + item.Template.Name);
                }
            }
        }
        [ItemGeneration(ItemTypeEnum.FAMILIER)]
        public static void InitPet(CharacterItemRecord item, Character character)
        {
            Effect effect = item.Effects.FindAll(x => x.EffectEnum != EffectsEnum.Effect_PetFeedQuantity).Random();

            if (effect != null)
            {
                item.Effects.Clear();
                item.AddEffect(effect);
            }
        }
    }
}
