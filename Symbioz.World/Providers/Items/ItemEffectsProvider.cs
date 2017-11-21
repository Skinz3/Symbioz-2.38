using Symbioz.Core.DesignPattern;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Items;
using Symbioz.World.Network;
using Symbioz.World.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Symbioz.Core;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Records.Monsters;

namespace Symbioz.World.Providers.Items
{
    public class ItemEffectsProvider
    {
        public static EffectsEnum[] UnhandledEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_PetFeedQuantity
        };

        public static readonly Dictionary<ItemEffectAttribute, MethodInfo> Handlers = typeof(ItemEffectsProvider).MethodsWhereAttributes<ItemEffectAttribute>();

        public static void AddEffects(Character character, List<Effect> effects)
        {
            foreach (EffectInteger effect in effects.FindAll(x => x is EffectInteger))
            {
                Handle(character, effect.EffectEnum, (short)effect.Value);
            }
        }
        public static void RemoveEffects(Character character, List<Effect> effects)
        {
            foreach (EffectInteger effect in effects.FindAll(x => x is EffectInteger))
            {
                Handle(character, effect.EffectEnum, (short)(-effect.Value));
            }
        }



        private static void Handle(Character character, EffectsEnum effectEnum, short value)
        {
            if (!UnhandledEffects.Contains(effectEnum))
            {
                var handler = Handlers.FirstOrDefault(x => x.Key.Effect == effectEnum);
                if (handler.Value != null)
                {
                    handler.Value.Invoke(null, new object[] { character, value });
                }
                else if (character.Client.Account.Role == ServerRoleEnum.Fondator)
                {
                    character.Reply(effectEnum + " is not handled.");
                }
            }
        }

        [ItemEffect(EffectsEnum.Effect_AddAP_111)]
        public static void AddAp111(Character character, short delta)
        {
            character.Record.Stats.ActionPoints.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddMP_128)]
        public static void AddMp128(Character character, short delta)
        {
            character.Record.Stats.MovementPoints.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddMP)]
        public static void AddMp(Character character, short delta)
        {
            character.Record.Stats.MovementPoints.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddStrength)]
        public static void AddStrength(Character character, short delta)
        {
            character.Record.Stats.Strength.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddChance)]
        public static void AddChance(Character character, short delta)
        {
            character.Record.Stats.Chance.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddIntelligence)]
        public static void AddIntelligence(Character character, short delta)
        {
            character.Record.Stats.Intelligence.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddAgility)]
        public static void AddAgility(Character character, short delta)
        {
            character.Record.Stats.Agility.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddWisdom)]
        public static void AddWisdom(Character character, short delta)
        {
            character.Record.Stats.Wisdom.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddVitality)]
        public static void AddVitality(Character character, short delta)
        {
            character.Record.Stats.Vitality.Objects += delta;
            character.Record.Stats.LifePoints += delta;
            character.Record.Stats.MaxLifePoints += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddHealth)]
        public static void AddHealth(Character character, short delta)
        {
            AddVitality(character, delta);
        }
        [ItemEffect(EffectsEnum.Effect_SubVitality)]
        public static void SubVitality(Character character, short delta)
        {
            character.Record.Stats.Vitality.Objects -= delta;
            character.Record.Stats.LifePoints -= delta;
            character.Record.Stats.MaxLifePoints -= delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddInitiative)]
        public static void AddInitiative(Character character, short delta)
        {
            character.Record.Stats.Initiative.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddRange)]
        public static void AddRange(Character character, short delta)
        {
            character.Record.Stats.Range.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddCriticalHit)]
        public static void AddCriticalHit(Character character, short delta)
        {
            character.Record.Stats.CriticalHit.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddDamageBonus)]
        public static void AddDamagesBonus(Character character, short delta)
        {
            character.Record.Stats.AllDamagesBonus.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddTrapBonus)]
        public static void AddTrapBonus(Character character, short delta)
        {
            character.Record.Stats.TrapBonus.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddTrapBonusPercent)]
        public static void AddTrapBonusPercent(Character character, short delta)
        {
            character.Record.Stats.TrapBonusPercent.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_IncreaseDamage_138)]
        public static void IncreaseDamage138(Character character, short delta)
        {
            character.Record.Stats.DamagesBonusPercent.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddProspecting)]
        public static void AddProspecting(Character character, short delta)
        {
            character.Record.Stats.Prospecting.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddSummonLimit)]
        public static void AddSummonLimit(Character character, short delta)
        {
            character.Record.Stats.SummonableCreaturesBoost.Objects += delta;
        }



        [ItemEffect(EffectsEnum.Effect_AddNeutralResistPercent)]
        public static void AddNeutralResistPercent(Character character, short delta)
        {
            character.Record.Stats.NeutralResistPercent.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubNeutralResistPercent)]
        public static void SubNeutralResistPercent(Character character, short delta)
        {
            character.Record.Stats.NeutralResistPercent.Objects -= delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddPvpNeutralResistPercent)]
        public static void AddPvpNeutralResistPercent(Character character, short delta)
        {
            character.Record.Stats.PvPNeutralResistPercent.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubPvpNeutralResistPercent)]
        public static void SubPvpNeutralResistPercent(Character character, short delta)
        {
            character.Record.Stats.PvPNeutralResistPercent.Objects -= delta;
        }



        [ItemEffect(EffectsEnum.Effect_AddAirResistPercent)]
        public static void AddAirResistPercent(Character character, short delta)
        {
            character.Record.Stats.AirResistPercent.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubAirResistPercent)]
        public static void SubAirResistPercent(Character character, short delta)
        {
            character.Record.Stats.AirResistPercent.Objects -= delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddPvpAirResistPercent)]
        public static void AddPvpAirResistPercent(Character character, short delta)
        {
            character.Record.Stats.PvPAirResistPercent.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubPvpAirResistPercent)]
        public static void SubPvpAirResistPercent(Character character, short delta)
        {
            character.Record.Stats.PvPAirResistPercent.Objects -= delta;
        }




        [ItemEffect(EffectsEnum.Effect_AddWaterResistPercent)]
        public static void AddWaterResistPercent(Character character, short delta)
        {
            character.Record.Stats.WaterResistPercent.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_SubWaterResistPercent)]
        public static void SubWaterResistPercent(Character character, short delta)
        {
            character.Record.Stats.WaterResistPercent.Objects -= delta;
        }

        [ItemEffect(EffectsEnum.Effect_SubPvpWaterResistPercent)]
        public static void SubPvpWaterResistPercent(Character character, short delta)
        {
            character.Record.Stats.PvPWaterResistPercent.Objects -= delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddPvpWaterResistPercent)]
        public static void AddPvpWaterResistPercent(Character character, short delta)
        {
            character.Record.Stats.PvPWaterResistPercent.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddPushDamageBonus)]
        public static void AddPushDamageBonus(Character character, short delta)
        {
            character.Record.Stats.PushDamageBonus.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddPushDamageReduction)]
        public static void AddPushDamageReduction(Character character, short delta)
        {
            character.Record.Stats.PushDamageReduction.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddCriticalDamageReduction)]
        public static void AddCriticalDamageReduction(Character character, short delta)
        {
            character.Record.Stats.CriticalDamageReduction.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddCriticalDamageBonus)]
        public static void AddCriticalDamageBonus(Character character, short delta)
        {
            character.Record.Stats.CriticalDamageBonus.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddFireResistPercent)]
        public static void AddFireResistPercent(Character character, short delta)
        {
            character.Record.Stats.FireResistPercent.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_SubFireResistPercent)]
        public static void SubFireResistPercent(Character character, short delta)
        {
            character.Record.Stats.FireResistPercent.Objects -= delta;
        }

        [ItemEffect(EffectsEnum.Effect_SubPvpFireResistPercent)]
        public static void SubPvpFireResistPercent(Character character, short delta)
        {
            character.Record.Stats.PvPFireResistPercent.Objects -= delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddPvpFireResistPercent)]
        public static void AddPvpFireResistPercent(Character character, short delta)
        {
            character.Record.Stats.PvPFireResistPercent.Objects += delta;
        }



        [ItemEffect(EffectsEnum.Effect_AddEarthResistPercent)]
        public static void AddEarthResistPercent(Character character, short delta)
        {
            character.Record.Stats.EarthResistPercent.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_SubEarthResistPercent)]
        public static void SubEarthResistPercent(Character character, short delta)
        {
            character.Record.Stats.EarthResistPercent.Objects -= delta;
        }

        [ItemEffect(EffectsEnum.Effect_SubPvpEarthResistPercent)]
        public static void SubPvpEarthResistPercent(Character character, short delta)
        {
            character.Record.Stats.PvPEarthResistPercent.Objects -= delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddPvpEarthResistPercent)]
        public static void AddPvpEarthResistPercent(Character character, short delta)
        {
            character.Record.Stats.PvPEarthResistPercent.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_AddWaterDamageBonus)]
        public static void WaterDamageBonus(Character character, short delta)
        {
            character.Record.Stats.WaterDamageBonus.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddAirDamageBonus)]
        public static void AirDamageBonus(Character character, short delta)
        {
            character.Record.Stats.AirDamageBonus.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddEarthDamageBonus)]
        public static void EarthDamageBonus(Character character, short delta)
        {
            character.Record.Stats.EarthDamageBonus.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddNeutralDamageBonus)]
        public static void NeutralDamageBonus(Character character, short delta)
        {
            character.Record.Stats.NeutralDamageBonus.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddFireDamageBonus)]
        public static void FireDamageBonus(Character character, short delta)
        {
            character.Record.Stats.FireDamageBonus.Objects += delta;
        }


        [ItemEffect(EffectsEnum.Effect_AddEarthElementReduction)]
        public static void EarthElementReduction(Character character, short delta)
        {
            character.Record.Stats.EarthReduction.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddFireElementReduction)]
        public static void FireElementReduction(Character character, short delta)
        {
            character.Record.Stats.FireReduction.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddHealBonus)]
        public static void HealBonus(Character character,short delta)
        {
            character.Record.Stats.HealBonus.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddWaterElementReduction)]
        public static void WaterElementReduction(Character character, short delta)
        {
            character.Record.Stats.WaterReduction.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddAirElementReduction)]
        public static void AirElementReduction(Character character, short delta)
        {
            character.Record.Stats.AirReduction.Objects += delta;
        }
        [ItemEffect(EffectsEnum.Effect_AddNeutralElementReduction)]
        public static void NeutralElementReduction(Character character, short delta)
        {
            character.Record.Stats.NeutralReduction.Objects += delta;
        }

        [ItemEffect(EffectsEnum.Effect_Emote)]
        public static void Emote(Character character, short delta)
        {
            if (delta > 0)
                character.LearnEmote((byte)delta);
            else
                character.RemoveEmote((byte)Math.Abs(delta));
        }
        [ItemEffect(EffectsEnum.Effect_Title)]
        public static void Title(Character character, short delta)
        {
            // Todo
        }
        [ItemEffect(EffectsEnum.Effect_Followed)]
        public static void Followed(Character character, short delta)
        {
            var monsterTemplate = MonsterRecord.GetMonster((ushort)Math.Abs(delta));

            if (delta > 0)
            {
                character.AddFollower(monsterTemplate.Look);
            }
            else
            {
                character.RemoveFollower(monsterTemplate.Look);
            }
        }



    }
}
