using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Entities.Jobs;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Results;
using Symbioz.World.Models.Monsters;
using System.Drawing;
using Symbioz.World.Providers.Fights;
using Symbioz.World.Providers.Items;
using Symbioz.World.Records;
using Symbioz.World.Records.Interactives;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers
{
    public class FormulasProvider : Singleton<FormulasProvider>
    {
        public const int MAX_JOB_LEVEL_GAP = 100;
        /// <summary>
        /// 1000 de base + Force * 5 + lvl métiers * 5 + nb métiers lvl 100 * 1000 + Bonus pods 
        /// </summary>
        /// <returns></returns>
        public uint TotalWeight(Character character)
        {
            return (uint)(1000 + character.Record.Stats.Strength.Total() + character.Record.Stats.WeightBonus);
        }
        /// <summary>
        /// Total Initiative = ( Bonus Caractéristiques + Bonus initiative) x ( Points de vie actuels / Points de vie max )
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public uint TotalInitiative(Stats stats)
        {
            double num1 = (double)stats.Total() + (double)stats.Initiative.Total();
            double num2 = (double)stats.LifePoints / (double)stats.MaxLifePoints;
            return (uint)((double)num1 * (double)num2);
        }
        public uint BaseInitiative(Stats stats)
        {
            double num1 = (double)stats.Total();
            double num2 = (double)stats.LifePoints / (double)stats.MaxLifePoints;
            return (uint)((double)num1 * (double)num2);
        }
        public ushort GetZaapCost(MapRecord zaapMap, MapRecord currentMap)
        {
            Point position = currentMap.Position.Point;
            Point position2 = zaapMap.Position.Point;
            return (ushort)System.Math.Floor(System.Math.Sqrt((double)((position2.X - position.X) * (position2.X - position.X) + (position2.Y - position.Y) * (position2.Y - position.Y))) * 10.0);
        }
        public uint AdjustDroppedKamas(IFightResult looter, int teamPP, long baseKamas, int dropBonusPercent)
        {
            int prospecting = looter.Prospecting;
            double num = (looter.Fight.AgeBonus <= 0) ? 1.0 : (1.0 + (double)looter.Fight.AgeBonus / 100.0);
            int kamas = (int)((double)baseKamas * ((double)prospecting / (double)teamPP) * num * (double)WorldConfiguration.Instance.KamasRate);
            kamas += kamas.GetPercentageOf(dropBonusPercent);
            return (uint)kamas;
        }
        public double AdjustDropChance(IFightResult looter, MonsterDrop item, sbyte dropperGradeId, int monsterAgeBonus, int dropBonusPercent)
        {
            dropBonusPercent = dropBonusPercent == 0 ? 1 : dropBonusPercent;
            var dropRate = item.GetDropRate((int)dropperGradeId);
            var prospecting = ((double)looter.Prospecting / 100.0);
            int result = (int)(dropRate * (double)prospecting * ((double)monsterAgeBonus / (double)100.0 + (double)1.0) * (double)WorldConfiguration.Instance.DropsRate);
            result += result.GetPercentageOf(dropBonusPercent);
            return result / 5d;
        }
        public ulong GetPvMExperience(CharacterFighter fighter)
        {
            var pMonstersList = new List<ExperienceFormulas.MonsterData>();
            var pMembersList = new List<ExperienceFormulas.GroupMemberData>();


            foreach (var monster in fighter.OposedTeam().GetFighters<BrainFighter>(false).FindAll(x => !x.IsSummon))
            {
                pMonstersList.Add(new ExperienceFormulas.MonsterData((int)monster.Level, (int)monster.Xp));
            }

            foreach (var ally in fighter.Team.GetFighters<CharacterFighter>(false))
            {
                pMembersList.Add(new ExperienceFormulas.GroupMemberData(ally.Level, false));
            }

            ExperienceFormulas formulas = new ExperienceFormulas();

            formulas.initXpFormula(new ExperienceFormulas.PlayerData(fighter.Level,
           fighter.Character.Record.Stats.Wisdom.Total(), fighter.Character.ExpMultiplicator + fighter.Character.Map.SubArea.ExperienceRate),
           pMonstersList, pMembersList,
             0, 0);

            if (fighter.Team.GetFighters<PlayableFighter>(false).Count > 1)
                return (ulong)formulas._xpGroup;
            else
                return (ulong)formulas._xpSolo;
        }
        /// <summary>
        /// DofusInvoker.swf => Item.as : getCraftXpByJobLevel(param1:int)
        /// </summary>
        /// <param name="resultLevel"></param>
        public int GetCraftXpByJobLevel(ushort resultLevel, ushort crafterLevel, int craftXpRatio)
        {
            double result = 0;
            double value = 0;

            if (resultLevel - MAX_JOB_LEVEL_GAP > crafterLevel)
            {
                return 0;
            }

            value = 20.0 * (double)resultLevel / (Math.Pow((double)crafterLevel - (double)resultLevel, 1.1) / 10.0 + 1.0);

            if (craftXpRatio > -1)
            {
                result = (double)value * ((double)craftXpRatio / (double)100);
            }
            else if (craftXpRatio > -1)
            {
                result = value * (craftXpRatio / 100);
            }
            else
            {
                result = value;
            }
            return (int)Math.Floor(result) * WorldConfiguration.Instance.CraftRate;
        }
        public uint GetCollectedItemQuantity(short jobLevel, SkillRecord skillRecord)
        {
            return (uint)new AsyncRandom().Next(jobLevel == 200 ? 7 : 1, skillRecord.MinLevel == 200 ? 1 : (int)(7 + ((jobLevel - skillRecord.MinLevel) / 10)));
        }
        public Jet EvaluateJet(Fighter source, EffectElementType elementType, EffectInstance effect, ushort spellId)
        {
            return this.EvaluateJet(source, elementType, (short)effect.DiceMin, (short)effect.DiceMax, source.GetSpellBoost(spellId), spellId == WeaponManager.PunchSpellId);
        }
        public Jet EvaluateJet(Fighter source, EffectElementType elementType, short jetMin, short jetMax, short boost, bool useWeapon)
        {

            if (jetMax == 0 || jetMax <= jetMin)
            {
                jetMin += boost;

                short delta = GetJetDelta(source, jetMin, elementType, useWeapon);

                return new Jet(delta, delta, delta);
            }
            else
            {
                jetMin += boost;

                jetMax += boost;

                short deltaMin = GetJetDelta(source, jetMin, elementType, useWeapon);

                short deltaMax = GetJetDelta(source, jetMax, elementType, useWeapon);

                short delta = (short)new AsyncRandom().Next(deltaMin, deltaMax + 1);

                return new Jet(deltaMin, deltaMax, delta);
            }
        }
        private static short GetJetDelta(Fighter fighter, short jet, EffectElementType elementType, bool useWeapon)
        {
            short result;

            switch (elementType)
            {
                case EffectElementType.Neutral:
                    result = (short)((double)(jet * (100 + fighter.Stats.Strength.TotalInContext() + fighter.Stats.DamagesBonusPercent.TotalInContext() + (useWeapon ? fighter.Stats.WeaponDamagesBonusPercent.TotalInContext() : 0)) / 100.0 + (double)(fighter.Stats.AllDamagesBonus.TotalInContext() + fighter.Stats.NeutralDamageBonus.TotalInContext())));
                    break;
                case EffectElementType.Earth:
                    result = (short)((double)(jet * (100 + fighter.Stats.Strength.TotalInContext() + fighter.Stats.DamagesBonusPercent.TotalInContext() + (useWeapon ? fighter.Stats.WeaponDamagesBonusPercent.TotalInContext() : 0)) / 100.0 + (double)(fighter.Stats.AllDamagesBonus.TotalInContext() + fighter.Stats.EarthDamageBonus.TotalInContext())));
                    break;
                case EffectElementType.Water:
                    result = (short)((double)(jet * (100 + fighter.Stats.Chance.TotalInContext() + fighter.Stats.DamagesBonusPercent.TotalInContext() + (useWeapon ? fighter.Stats.WeaponDamagesBonusPercent.TotalInContext() : 0)) / 100.0 + (double)(fighter.Stats.AllDamagesBonus.TotalInContext() + fighter.Stats.WaterDamageBonus.TotalInContext())));
                    break;
                case EffectElementType.Air:
                    result = (short)((double)(jet * (100 + fighter.Stats.Agility.TotalInContext() + fighter.Stats.DamagesBonusPercent.TotalInContext() + (useWeapon ? fighter.Stats.WeaponDamagesBonusPercent.TotalInContext() : 0)) / 100.0 + (double)(fighter.Stats.AllDamagesBonus.TotalInContext() + fighter.Stats.AirDamageBonus.TotalInContext())));
                    break;
                case EffectElementType.Fire:
                    result = (short)((double)(jet * (100 + fighter.Stats.Intelligence.TotalInContext() + fighter.Stats.DamagesBonusPercent.TotalInContext() + (useWeapon ? fighter.Stats.WeaponDamagesBonusPercent.TotalInContext() : 0)) / 100.0 + (double)(fighter.Stats.AllDamagesBonus.TotalInContext() + fighter.Stats.FireDamageBonus.TotalInContext())));
                    break;
                default:
                    result = jet;
                    break;
            }
            result = result < jet ? jet : result;
            return result;
        }
        public short GetPushDamages(Fighter source, Fighter target, short cellsCount, bool headOn)
        {
            double num1 = headOn ? 4 : 8;
            double num2 = (((double)source.Level / (double)2) + (source.Stats.PushDamageBonus.TotalInContext() - target.Stats.PushDamageReduction.TotalInContext()) + 32)
                 * ((double)cellsCount / (double)num1);
            return (short)num2;
        }
        /// <summary>
        /// Base * (100 + Intelligence ) / 100 + Soins
        /// </summary>
        /// <returns></returns>
        public short GetHealDelta(Fighter source, short jet)
        {
            return (short)((double)jet * (double)((100 + source.Stats.Intelligence.TotalInContext())) / (double)100 + (double)source.Stats.HealBonus.TotalInContext());
        }
        /// <summary>
        ///
        ///   Le nombre de PA totaux de la cible = 10.
        ///   Le nombre de PA/PM restants de la cible au moment du retrait du PA=7
        ///   La esquive de base du lanceur = 150 .
        ///   L'esquive aux pertes de PA de la cible = 90.
        /// PA = 7/10 x 150/90 x 1/2 = ~58%
        /// 
        /// PA ou PM restants de la cible/PA ou PM totaux de la cible x esquive de base du lanceur/esquive de la cible. x 1/2
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public bool RollMpLose(Fighter from, Fighter to)
        {
            double currentActionPointsTo = (double)to.Stats.ActionPoints.TotalInContext();

            double maxActionPointsTo = (double)to.Stats.ActionPoints.Total();

            double mpDodgeFrom = ((double)from.Stats.Wisdom.TotalInContext() / (double)10) + (double)from.Stats.DodgePMProbability.TotalInContext();

            double mpDodgeTo = ((double)to.Stats.Wisdom.TotalInContext() / (double)10) + (double)to.Stats.DodgePMProbability.TotalInContext();

            mpDodgeTo = mpDodgeTo == 0 ? 1 : mpDodgeTo;
            mpDodgeFrom = mpDodgeFrom == 0 ? 1 : mpDodgeFrom;

            double probability = (currentActionPointsTo / maxActionPointsTo) * (mpDodgeFrom / mpDodgeTo) * ((double)1 / (double)2);

            probability *= 100;

            double num = new AsyncRandom().Next(0, 100);
            return probability > num;
        }
        public bool RollApLose(Fighter from, Fighter to)
        {
            double currentActionPointsTo = (double)to.Stats.ActionPoints.TotalInContext();

            double maxActionPointsTo = (double)to.Stats.ActionPoints.Total();

            double apDodgeFrom = ((double)from.Stats.Wisdom.TotalInContext() / (double)10) + (double)from.Stats.DodgePAProbability.TotalInContext();

            double apDodgeTo = ((double)to.Stats.Wisdom.TotalInContext() / (double)10) + (double)to.Stats.DodgePAProbability.TotalInContext();

            apDodgeTo = apDodgeTo == 0 ? 1 : apDodgeTo;
            apDodgeFrom = apDodgeFrom == 0 ? 1 : apDodgeFrom;

            double probability = (currentActionPointsTo / maxActionPointsTo) * (apDodgeFrom / apDodgeTo) * ((double)1 / (double)2);

            probability *= 100;

            double num = new AsyncRandom().Next(0, 100);
            return probability > num;
        }
        /// <summary>
        /// (Caractéristique de base de l'invocation)*(1 + (Niveau de l'invocateur)/100)
        /// </summary>
        /// <param name="baseDelta"></param>
        /// <param name="ownerLevel"></param>
        /// <returns></returns>
        public int GetSummonedCharacteristicDelta(int baseDelta, ushort ownerLevel)
        {
            return (int)(((double)baseDelta) * (1 + ((double)ownerLevel / (double)100)));
        }
        /// <summary>
        ///  (Vitalité du roublard / 10) x 2 + 10 
        /// </summary>
        /// <param name="ownerVitality"></param>
        /// <returns></returns>
        public int GetBombLife(Fighter owner)
        {
            return (int)((owner.Stats.MaxLifePoints / (double)10) * 2 + 10);
        }

    }
}
