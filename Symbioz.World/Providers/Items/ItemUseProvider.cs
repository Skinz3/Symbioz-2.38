using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Entities;
using Symbioz.Protocol.Types;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Records;
using Symbioz.World.Providers.Criterias;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using Symbioz.World.Records.Spells;
using Symbioz.World.Providers.Maps.Monsters;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Maps;
using Symbioz.World.Network;
using Symbioz.World.Records.Cosmetics;

namespace Symbioz.World.Providers.Items
{
    public class ItemUseProvider
    {
        public static readonly Dictionary<ItemUseAttribute, MethodInfo> Handlers = typeof(ItemUseProvider).MethodsWhereAttributes<ItemUseAttribute>();

        public static bool Handle(Character character, CharacterItemRecord item)
        {
            if (!CriteriaProvider.EvaluateCriterias(character.Client, item.Template.Criteria))
            {
                character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 34);
                return false;
            }
            var function = Handlers.FirstOrDefault(x => x.Key.GId == item.GId);

            if (function.Value != null)
            {
                return (bool)function.Value.Invoke(null, new object[] { character, item });
            }
            else
            {
                function = Handlers.FirstOrDefault(x => x.Key.ItemType == item.Template.TypeEnum);
                if (function.Value != null)
                {
                    return (bool)function.Value.Invoke(null, new object[] { character, item });

                }
                foreach (var effect in item.GetEffects<Effect>())
                {
                    function = Handlers.FirstOrDefault(x => x.Key.Effect == effect.EffectEnum);
                    if (function.Value != null)
                    {
                        try
                        {
                            return (bool)function.Value.Invoke(null, new object[] { character, effect });
                        }
                        catch (Exception ex)
                        {
                            character.ReplyError(ex.ToString());
                            return false;
                        }


                    }
                    else
                    {
                        //   character.Reply(effect.EffectEnum + " is not handled");
                        return false;

                    }
                }
                return false;

            }
        }


        [ItemUse(EffectsEnum.Effect_SpawnPoint)]
        public static bool TeleportSavePoint(Character character, EffectInteger effect)
        {
            character.PlayEmote((byte)EmotesEnum.DrinkPotion);
            character.SpawnPoint();
            return true;
        }
        [ItemUse(EffectsEnum.Effect_Emote)]
        public static bool LearnEmote(Character character, EffectInteger effect)
        {
            character.LearnEmote((byte)effect.Value);
            return true;
        }
        [ItemUse(EffectsEnum.Effect_Book)]
        public static bool ReadBook(Character character, EffectInteger effect)
        {
            character.ReadDocument(effect.Value);
            return false;
        }

        [ItemUse(EffectsEnum.Effect_AddPermanentAgility)]
        public static bool PermanentAgility(Character character, EffectInteger effect)
        {
            character.Record.Stats.Agility.Additional += (short)effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 12, effect.Value);
            return true;
        }
        [ItemUse(EffectsEnum.Effect_AddPermanentStrength)]
        public static bool PermanentStrength(Character character, EffectInteger effect)
        {
            character.Record.Stats.Strength.Additional += (short)effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 10, effect.Value);
            return true;
        }
        [ItemUse(EffectsEnum.Effect_AddPermanentIntelligence)]
        public static bool PermanentIntelligence(Character character, EffectInteger effect)
        {
            character.Record.Stats.Intelligence.Additional += (short)effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 14, effect.Value);
            return true;
        }
        [ItemUse(EffectsEnum.Effect_AddPermanentWisdom)]
        public static bool PermanentWisdom(Character character, EffectInteger effect)
        {
            character.Record.Stats.Wisdom.Additional += (short)effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 9, effect.Value);
            return true;
        }
        [ItemUse(EffectsEnum.Effect_AddPermanentChance)]
        public static bool PermanentChance(Character character, EffectInteger effect)
        {
            character.Record.Stats.Chance.Additional += (short)effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 11, effect.Value);
            return true;
        }
        [ItemUse(EffectsEnum.Effect_AddPermanentVitality)]
        public static bool PermanentVitality(Character character, EffectInteger effect)
        {
            character.Record.Stats.Vitality.Additional += (short)effect.Value;
            character.Record.Stats.LifePoints += effect.Value;
            character.Record.Stats.MaxLifePoints += effect.Value;
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 13, effect.Value);
            return true;
        }
        [ItemUse(EffectsEnum.Effect_AddExp)]
        public static bool AddExp(Character character, EffectInteger effect)
        {
            character.AddExperience(effect.Value);
            character.OnExperienceGained(effect.Value);
            return true;
        }
        [ItemUse(EffectsEnum.Effect_LearnSpell)]
        public static bool LearnSpell(Character character, EffectInteger effect)
        {
            character.LearnSpell(SpellLevelRecord.GetSpellId(effect.Value));
            return true;
        }
        [ItemUse(12915)]
        public static bool CadeauChaponey(Character character, CharacterItemRecord item)
        {
            if (character.Map.Instance.MonsterGroupCount >= MonsterSpawnManager.MaxMonsterGroupPerMap)
            {
                character.Reply("Impossible d'ajouter un groupe de monstres a la carte, celle-ci est déja complete.");
                return false;
            }


            character.SpellAnim(7356);

            if (character.Record.Alignment.Side != AlignmentSideEnum.ALIGNMENT_NEUTRAL)
            {
                character.AddHonor(1500);
                character.Reply("Vous avez obtenu <b>1500</b> point d'honneur.");
            }

            character.Map.Instance.RandomMonsters(3, character.Level);
            character.Reply("Vous venez de spawn un groupe de monstre aléatoire!!");
            return true;
        }
        [ItemUse(12918)]
        public static bool CadeauMesPetitsSabots(Character character, CharacterItemRecord item)
        {
            return GiftXPModifier(character, character.Map.SubArea, 1);
        }
        [ItemUse(12919)]
        public static bool CadeauGladiasque(Character character, CharacterItemRecord item)
        {
            return GiftXPModifier(character, character.Map.SubArea, 2);
        }
        private static bool GiftXPModifier(Character character, SubareaRecord subarea, int hours)
        {
            if (character.Map.SubArea.ExperienceRate >= 20)
            {
                character.Reply("L'experience de la zone est déja a son maximum!");
                return false;
            }
            int rate = character.Map.SubArea.ExperienceRate;
            string msg = string.Format("Le multiplicateur d'experience de la zone {0} passe de x{1} a x{2} pendant {3}h !  merci a {4}!", character.Map.SubArea.Name, rate, rate + 1, hours, character.Name);
            WorldServer.Instance.OnClients(x => x.Character.Notification(msg));
            character.Map.SubArea.ExperienceRate += 1;
            character.UpdateServerExperience(character.Map.SubArea.ExperienceRate);

            MapRecord map = character.Map;

            ActionTimer timer = new ActionTimer(3600000 * hours, new Action(() =>
            {
                map.SubArea.ExperienceRate -= 1;
            }), false);
            timer.Start();
            return true;
        }
        [ItemUse(12920)]
        public static bool CadeauGladiaton(Character character, CharacterItemRecord item)
        {
            var title = TitleRecord.Titles.FindAll(x => character.HasTitle(x.Id) == false);

            if (title.Count == 0)
            {
                return false;
            }
            character.LearnTitle(title.Random().Id);
            return true;
        }
        [ItemUse(12921)]
        public static bool CadeauGladialeçon(Character character, CharacterItemRecord item)
        {
            var ornaments = OrnamentRecord.Ornaments.FindAll(x => character.HasOrnament(x.Id) == false);

            if (ornaments.Count == 0)
            {
                return false;
            }
            character.LearnOrnament(ornaments.Random().Id, true);
            return true;
        }
        [ItemUse(14485)]
        public static bool Mimicry(Character character, CharacterItemRecord item)
        {
            character.OpenUIByObject(3, item.UId);
            return false;
        }
        [ItemUse(EffectsEnum.Effect_ItemTeleport)]
        public static bool ItemTeleport(Character character, EffectInteger effect)
        {
            character.PlayEmote((byte)EmotesEnum.DrinkPotion);

            switch (effect.Value)
            {
                case 156: // Maison de Kerubim
                    character.Teleport(103547392, 347);
                    return true;
                case 169: // Ilot de la couronne
                    character.Teleport(99614979, 313);
                    return true;
                default:
                    return false;
            }
        }
        [ItemUse(12127)] // Cadeau Phoenix
        public static bool PhoenixGift(Character character, CharacterItemRecord item)
        {
            character.Inventory.AddItem(ItemRecord.RandomItem(ItemTypeEnum.FAMILIER).Id, 1);
            return true;
        }
        [ItemUse(10860)]
        public static bool ChangeNamePotion(Character character, CharacterItemRecord item)
        {
            character.Record.AddToRemodelingMask(CharacterRemodelingEnum.CHARACTER_REMODELING_NAME);
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_POPUP, 41);
            return true;
        }
        [ItemUse(10861)]
        public static bool ChangeColorsPotion(Character character, CharacterItemRecord item)
        {
            character.Record.AddToRemodelingMask(CharacterRemodelingEnum.CHARACTER_REMODELING_COLORS);
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_POPUP, 42);
            return true;
        }
        [ItemUse(13518)]
        public static bool ChangeHeadPotion(Character character, CharacterItemRecord item)
        {
            character.Record.AddToRemodelingMask(CharacterRemodelingEnum.CHARACTER_REMODELING_COSMETIC);
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_POPUP, 58);
            return true;
        }
        [ItemUse(16147)]
        public static bool ChangeBreedPotion(Character character, CharacterItemRecord item)
        {
            character.Record.AddToRemodelingMask(CharacterRemodelingEnum.CHARACTER_REMODELING_BREED);
            character.Record.AddToRemodelingMask(CharacterRemodelingEnum.CHARACTER_REMODELING_COSMETIC);
            character.Record.AddToRemodelingMask(CharacterRemodelingEnum.CHARACTER_REMODELING_COLORS);
            character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_POPUP, 63);
            return true;
        }
        [ItemUse(EffectsEnum.Effect_GiveKamas)]
        public static bool GiveKamas(Character character, EffectInteger effect)
        {
            character.AddKamas(effect.Value);
            return true;
        }
        [ItemUse(EffectsEnum.Effect_CityTeleport)]
        public static bool TeleportToCity(Character character, EffectInteger effect)
        {
            character.PlayEmote((byte)EmotesEnum.DrinkPotion);

            int? mapId = null;

            switch (effect.Value)
            {
                case 104:
                    mapId = 144419; // Brackmar
                    break;
                case 1436:
                    mapId = 147768; // Bonta
                    break;

            }

            if (mapId.HasValue)
            {
                character.TeleportZaap(mapId.Value);
                return true;
            }
            else
                return false;

        }
        [ItemUse(EffectsEnum.Effect_AddSpellPoints)]
        public static bool AddSpellPoint(Character character, EffectInteger effect)
        {
            character.AddSpellPoints(effect.Value);
            return true;
        }
        [ItemUse(ItemTypeEnum.FÉE_D_ARTIFICE)]
        public static bool FairyWork(Character character, CharacterItemRecord item)
        {
            SpellRecord spell = SpellRecord.Spells.FirstOrDefault(x => x.Name == item.Template.Name);

            if (spell != null)
                character.SpellAnim(spell.Id);
            else
                character.SpellAnim(1800);

            return true;
        }
        [ItemUse(EffectsEnum.Effect_SpawnMonster)]
        public static bool SpawnMonster(Character character, EffectDice effect)
        {
            MonsterRecord template = MonsterRecord.GetMonster(effect.Const);

            if (template != null && template.GradeExist((sbyte)effect.Min))
            {
                if (character.Map.Instance.MonsterGroupCount < MonsterSpawnManager.MaxMonsterGroupPerMap && MapNoSpawnRecord.AbleToSpawn(character.Map.Id))
                {
                    MonsterSpawnManager.Instance.AddFixedMonsterGroup(character.Map.Instance,
                        new MonsterRecord[] { template },
                        new sbyte[] { (sbyte)effect.Min }
                        , false);
                    return true;
                }
                else
                {
                    character.ReplyError("Impossible de spawn le monstre ici...");
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
    }

}
