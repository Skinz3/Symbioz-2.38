using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Monsters;
using System;
using System.Collections.Generic;
using Symbioz.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Providers;

namespace Symbioz.World.Models.Entities.Stats
{
    public class FighterStats : Stats
    {
        public int LifePercentage
        {
            get
            {
                return CurrentLifePoints.Percentage(CurrentMaxLifePoints);
            }
        }
        public int MpPercentage
        {
            get
            {
                return ((int)MovementPoints.TotalInContext()).Percentage(MovementPoints.Total());
            }
        }
        public double FinalDamageCoefficient = 1;

        public int LifeLost
        {
            get
            {
                return CurrentMaxLifePoints - CurrentLifePoints;
            }
        }
        public int CurrentLifePoints
        {
            get;
            set;
        }
        public int Erosion
        {
            get;
            set;
        }
        public int CurrentMaxLifePoints
        {
            get;
            set;
        }
        public int ErrodedLife
        {
            get
            {
                return MaxLifePoints - CurrentMaxLifePoints;
            }
        }

        public short MpUsed
        {
            get;
            set;
        }
        public short ApUsed
        {
            get;
            set;
        }
        public GameActionFightInvisibilityStateEnum InvisibilityState
        {
            get;
            set;
        }

        public int ShieldPoints
        {
            get;
            private set;
        }
        public bool Summoned
        {
            get;
            private set;
        }
        public int SummonerId
        {
            get;
            private set;
        }
        public void OnTurnEnded()
        {
            MovementPoints.Context += MpUsed;
            ActionPoints.Context += ApUsed;
            MpUsed = 0;
            ApUsed = 0;


        }
        public void UseMp(short amount)
        {
            MovementPoints.Context -= amount;
            MpUsed += amount;
        }
        public void UseAp(short amount)
        {
            ActionPoints.Context -= amount;
            ApUsed += amount;
        }
        public void AddShield(short amount)
        {
            ShieldPoints += amount;
        }
        public void RemoveShield(short amount)
        {
            ShieldPoints = ShieldPoints - amount < 0 ? 0 : ShieldPoints - amount;
        }
        public void SetShield(int delta)
        {
            ShieldPoints = delta < 0 ? 0 : delta;
        }
        public FighterStats()
        {

        }
        /// <summary>
        /// 0 dans les stats de mobs, a voir. + Initiative Mobs :)
        /// </summary>
        /// <param name="grade"></param>
        public FighterStats(MonsterGrade grade, int power)
        {
            this.CurrentLifePoints = (int)grade.LifePoints;
            this.CurrentMaxLifePoints = (int)grade.LifePoints;
            this.InvisibilityState = GameActionFightInvisibilityStateEnum.VISIBLE;
            this.ShieldPoints = 0;
            this.Summoned = false;
            this.SummonerId = 0;

            this.ActionPoints = ApCharacteristic.New(grade.ActionPoints);
            this.MovementPoints = MpCharacteristic.New(grade.MovementPoints);
            this.Agility = Characteristic.New((short)(power));
            this.AirDamageBonus = Characteristic.Zero();
            this.AirReduction = Characteristic.Zero();
            this.AirResistPercent = ResistanceCharacteristic.New(grade.AirResistance);
            this.AllDamagesBonus = Characteristic.Zero();
            this.Chance = Characteristic.New((short)(power));
            this.CriticalDamageBonus = Characteristic.Zero();
            this.CriticalDamageReduction = Characteristic.Zero();
            this.CriticalHit = Characteristic.Zero();
            this.CriticalHitWeapon = 0;
            this.DamagesBonusPercent = Characteristic.Zero();
            this.DodgePAProbability = Characteristic.New(grade.PADodge);
            this.DodgePMProbability = Characteristic.New(grade.PmDodge);
            this.EarthDamageBonus = Characteristic.Zero();
            this.EarthReduction = Characteristic.Zero();
            this.EarthResistPercent = ResistanceCharacteristic.New(grade.EarthResistance);
            this.Energy = 0;
            this.FireDamageBonus = Characteristic.Zero();
            this.FireReduction = Characteristic.Zero();
            this.FireResistPercent = ResistanceCharacteristic.New(grade.FireResistance);
            this.GlobalDamageReduction = 0;
            this.GlyphBonusPercent = Characteristic.Zero();
            this.HealBonus = Characteristic.Zero();
            this.Initiative = Characteristic.Zero();
            this.Intelligence = Characteristic.New((short)(power));
            this.LifePoints = (int)grade.LifePoints;
            this.MaxEnergyPoints = 0;
            this.MaxLifePoints = (int)grade.LifePoints;
            this.NeutralDamageBonus = Characteristic.Zero();
            this.NeutralReduction = Characteristic.Zero();
            this.NeutralResistPercent = ResistanceCharacteristic.New(grade.NeutralResistance);
            this.PAAttack = Characteristic.Zero();
            this.PermanentDamagePercent = Characteristic.Zero();
            this.PvPAirResistPercent = ResistanceCharacteristic.Zero();
            this.PvPAirReduction = Characteristic.Zero();
            this.PvPEarthReduction = Characteristic.Zero();
            this.PvPEarthResistPercent = ResistanceCharacteristic.Zero();
            this.PvPFireReduction = Characteristic.Zero();
            this.PvPFireResistPercent = ResistanceCharacteristic.Zero();
            this.PvPNeutralReduction = Characteristic.Zero();
            this.PvPNeutralResistPercent = ResistanceCharacteristic.Zero();
            this.PvPWaterReduction = ResistanceCharacteristic.Zero();
            this.PvPWaterResistPercent = ResistanceCharacteristic.Zero();
            this.PushDamageReduction = Characteristic.Zero();
            this.PushDamageBonus = Characteristic.Zero();
            this.Prospecting = Characteristic.Zero();
            this.PMAttack = Characteristic.Zero();
            this.Range = RangeCharacteristic.Zero();
            this.Reflect = Characteristic.New((short)grade.DamageReflect);
            this.RuneBonusPercent = Characteristic.Zero();
            this.Strength = Characteristic.New((short)(power));
            this.SummonableCreaturesBoost = Characteristic.New(1);
            this.TackleBlock = Characteristic.Zero();
            this.TackleEvade = Characteristic.Zero();
            this.TrapBonus = Characteristic.Zero(); ;
            this.TrapBonusPercent = Characteristic.Zero();
            this.Vitality = Characteristic.Zero();
            this.WaterDamageBonus = Characteristic.Zero();
            this.WaterReduction = Characteristic.Zero();
            this.WaterResistPercent = ResistanceCharacteristic.New(grade.WaterResistance);
            this.WeaponDamagesBonusPercent = Characteristic.Zero();
            this.WeightBonus = 0;
            this.Wisdom = Characteristic.New((short)(grade.Wisdom));//+ ((double)power / (double)2)));
            this.FinalDamageCoefficient = 1;
        }
        public void InitializeSummon(Fighter owner, bool summonStats)
        {
            this.Summoned = true;
            this.SummonerId = owner.Id;


            if (summonStats)
            {

                this.CurrentLifePoints = this.CurrentMaxLifePoints = this.MaxLifePoints = this.LifePoints = FormulasProvider.Instance.GetSummonedCharacteristicDelta(CurrentLifePoints, owner.Level);

                this.Strength.Base = (short)FormulasProvider.Instance.GetSummonedCharacteristicDelta(Strength.Total(), owner.Level);

                this.Agility.Base = (short)FormulasProvider.Instance.GetSummonedCharacteristicDelta(Agility.Total(), owner.Level);

                this.Intelligence.Base = (short)FormulasProvider.Instance.GetSummonedCharacteristicDelta(Intelligence.Total(), owner.Level);

                this.Chance.Base = (short)FormulasProvider.Instance.GetSummonedCharacteristicDelta(Chance.Total(), owner.Level);

                this.Wisdom.Base = (short)FormulasProvider.Instance.GetSummonedCharacteristicDelta(Wisdom.Total(), owner.Level);

            }
        }
        public void InitializeBomb(Fighter owner)
        {
            this.Summoned = true;
            this.SummonerId = owner.Id;
            this.CurrentLifePoints = this.CurrentMaxLifePoints = this.MaxLifePoints = this.LifePoints = FormulasProvider.Instance.GetBombLife(owner);
        }
        /// <summary>
        /// (Caractéristique de base de l'invocation)*(1 + (Niveau de l'invocateur)/100)
        /// </summary>
        /// <param name="monsterChar"></param>
        /// <param name="ownerLevel"></param>
        /// <returns></returns>

        public FighterStats(Character character)
        {
            this.CurrentLifePoints = (int)character.Record.Stats.LifePoints;
            this.CurrentMaxLifePoints = (int)character.Record.Stats.MaxLifePoints;
            this.InvisibilityState = GameActionFightInvisibilityStateEnum.VISIBLE;
            this.ShieldPoints = 0;
            this.Summoned = false;
            this.SummonerId = 0;

            this.ActionPoints = (ApCharacteristic)character.Record.Stats.ActionPoints.Clone();
            this.MovementPoints = (MpCharacteristic)character.Record.Stats.MovementPoints.Clone();
            this.Agility = character.Record.Stats.Agility.Clone();
            this.AirDamageBonus = character.Record.Stats.AirDamageBonus.Clone();
            this.AirReduction = character.Record.Stats.AirReduction.Clone();
            this.AirResistPercent = (ResistanceCharacteristic)character.Record.Stats.AirResistPercent.Clone();
            this.AllDamagesBonus = character.Record.Stats.AllDamagesBonus.Clone();
            this.Chance = character.Record.Stats.Chance.Clone();
            this.CriticalDamageBonus = character.Record.Stats.CriticalDamageBonus.Clone();
            this.CriticalDamageReduction = character.Record.Stats.CriticalDamageReduction.Clone();
            this.CriticalHit = character.Record.Stats.CriticalHit.Clone();
            this.CriticalHitWeapon = character.Record.Stats.CriticalHitWeapon;
            this.DamagesBonusPercent = character.Record.Stats.DamagesBonusPercent.Clone();
            this.DodgePAProbability = character.Record.Stats.DodgePAProbability.Clone();
            this.DodgePMProbability = character.Record.Stats.DodgePMProbability.Clone();
            this.EarthDamageBonus = character.Record.Stats.EarthDamageBonus.Clone();
            this.EarthReduction = character.Record.Stats.EarthReduction.Clone();
            this.EarthResistPercent = (ResistanceCharacteristic)character.Record.Stats.EarthResistPercent.Clone();
            this.Energy = character.Record.Stats.Energy;
            this.FireDamageBonus = character.Record.Stats.FireDamageBonus.Clone();
            this.FireReduction = character.Record.Stats.FireReduction.Clone();
            this.FireResistPercent = (ResistanceCharacteristic)character.Record.Stats.FireResistPercent.Clone();
            this.GlobalDamageReduction = character.Record.Stats.GlobalDamageReduction;
            this.GlyphBonusPercent = character.Record.Stats.GlyphBonusPercent.Clone();
            this.HealBonus = character.Record.Stats.HealBonus.Clone();
            this.Initiative = character.Record.Stats.Initiative.Clone();
            this.Intelligence = character.Record.Stats.Intelligence.Clone();
            this.LifePoints = character.Record.Stats.LifePoints;
            this.MaxEnergyPoints = character.Record.Stats.MaxEnergyPoints;
            this.MaxLifePoints = character.Record.Stats.MaxLifePoints;
            this.NeutralDamageBonus = character.Record.Stats.NeutralDamageBonus.Clone();
            this.NeutralReduction = character.Record.Stats.NeutralReduction.Clone();
            this.NeutralResistPercent = (ResistanceCharacteristic)character.Record.Stats.NeutralResistPercent.Clone();
            this.PAAttack = character.Record.Stats.PAAttack.Clone();
            this.PMAttack = character.Record.Stats.PMAttack.Clone();
            this.Prospecting = character.Record.Stats.Prospecting.Clone();
            this.PermanentDamagePercent = character.Record.Stats.PermanentDamagePercent.Clone();
            this.PvPAirResistPercent = (ResistanceCharacteristic)character.Record.Stats.PvPAirResistPercent.Clone();
            this.PvPAirReduction = character.Record.Stats.PvPAirReduction.Clone();
            this.PvPEarthReduction = character.Record.Stats.PvPEarthReduction.Clone();
            this.PvPEarthResistPercent = (ResistanceCharacteristic)character.Record.Stats.PvPEarthResistPercent.Clone();
            this.PvPFireReduction = character.Record.Stats.PvPFireReduction.Clone();
            this.PvPFireResistPercent = (ResistanceCharacteristic)character.Record.Stats.PvPFireResistPercent.Clone();
            this.PvPNeutralReduction = character.Record.Stats.PvPNeutralReduction.Clone();
            this.PvPNeutralResistPercent = (ResistanceCharacteristic)character.Record.Stats.PvPNeutralResistPercent.Clone();
            this.PvPWaterReduction = character.Record.Stats.PvPWaterReduction.Clone();
            this.PvPWaterResistPercent = (ResistanceCharacteristic)character.Record.Stats.PvPWaterResistPercent.Clone();
            this.PushDamageReduction = character.Record.Stats.PushDamageReduction.Clone();
            this.PushDamageBonus = character.Record.Stats.PushDamageBonus.Clone();
            this.Range = (RangeCharacteristic)character.Record.Stats.Range.Clone();
            this.Reflect = character.Record.Stats.Reflect.Clone();
            this.RuneBonusPercent = character.Record.Stats.RuneBonusPercent.Clone();
            this.Strength = character.Record.Stats.Strength.Clone();
            this.SummonableCreaturesBoost = character.Record.Stats.SummonableCreaturesBoost.Clone();
            this.TackleBlock = character.Record.Stats.TackleBlock.Clone();
            this.TackleEvade = character.Record.Stats.TackleEvade.Clone();
            this.TrapBonus = character.Record.Stats.TrapBonus.Clone();
            this.TrapBonusPercent = character.Record.Stats.TrapBonusPercent.Clone();
            this.Vitality = character.Record.Stats.Vitality.Clone();
            this.WaterDamageBonus = character.Record.Stats.WaterDamageBonus.Clone();
            this.WaterReduction = character.Record.Stats.WaterReduction.Clone();
            this.WaterResistPercent = (ResistanceCharacteristic)character.Record.Stats.WaterResistPercent.Clone();
            this.WeaponDamagesBonusPercent = character.Record.Stats.WeaponDamagesBonusPercent.Clone();
            this.WeightBonus = character.Record.Stats.WeightBonus;
            this.Wisdom = character.Record.Stats.Wisdom.Clone();

        }
        public GameFightMinimalStats GetFightMinimalStats()
        {
            return new GameFightMinimalStats()
            {
                actionPoints = ActionPoints.TotalInContext(),
                movementPoints = MovementPoints.TotalInContext(),
                maxActionPoints = ActionPoints.Total(),
                maxMovementPoints = MovementPoints.Total(),

                airElementReduction = AirReduction.TotalInContext(),
                airElementResistPercent = AirResistPercent.TotalInContext(),
                baseMaxLifePoints = (uint)MaxLifePoints,
                lifePoints = (uint)CurrentLifePoints,
                maxLifePoints = (uint)CurrentMaxLifePoints,
                invisibilityState = (sbyte)InvisibilityState,
                dodgePALostProbability = (ushort)DodgePAProbability.TotalInContext(),
                dodgePMLostProbability = (ushort)DodgePMProbability.TotalInContext(),
                pvpAirElementReduction = PvPAirReduction.TotalInContext(),
                pvpAirElementResistPercent = PvPAirResistPercent.TotalInContext(),
                pvpEarthElementReduction = PvPEarthReduction.TotalInContext(),
                pvpEarthElementResistPercent = PvPEarthResistPercent.TotalInContext(),
                pvpFireElementReduction = PvPFireReduction.TotalInContext(),
                pvpFireElementResistPercent = PvPFireResistPercent.TotalInContext(),
                pvpNeutralElementReduction = PvPNeutralReduction.TotalInContext(),
                pvpNeutralElementResistPercent = PvPNeutralResistPercent.TotalInContext(),
                pvpWaterElementReduction = PvPWaterReduction.TotalInContext(),
                pvpWaterElementResistPercent = PvPWaterResistPercent.TotalInContext(),
                shieldPoints = (uint)ShieldPoints,
                earthElementReduction = EarthReduction.TotalInContext(),
                earthElementResistPercent = EarthResistPercent.TotalInContext(),
                fireElementResistPercent = FireResistPercent.TotalInContext(),
                waterElementResistPercent = WaterResistPercent.TotalInContext(),
                neutralElementResistPercent = NeutralResistPercent.TotalInContext(),
                fireElementReduction = FireReduction.TotalInContext(),
                neutralElementReduction = NeutralReduction.TotalInContext(),
                waterElementReduction = WaterReduction.TotalInContext(),
                summoned = Summoned,
                summoner = SummonerId,
                tackleBlock = TackleBlock.TotalInContext(),
                tackleEvade = TackleEvade.TotalInContext(),
                pushDamageFixedResist = PushDamageReduction.TotalInContext(),
                permanentDamagePercent = (uint)PermanentDamagePercent.TotalInContext(),
                fixedDamageReflection = Reflect.TotalInContext(),
                criticalDamageFixedResist = CriticalDamageReduction.TotalInContext()
            };
        }
        public override Stats Clone()
        {
            return new FighterStats()
            {
                LifePoints = this.LifePoints,
                MaxLifePoints = this.MaxLifePoints,
                MaxEnergyPoints = this.MaxEnergyPoints,
                Energy = this.Energy,
                Initiative = this.Initiative.Clone(),
                Prospecting = this.Prospecting.Clone(),
                ActionPoints = (ApCharacteristic)this.ActionPoints.Clone(),
                MovementPoints = (MpCharacteristic)this.MovementPoints.Clone(),
                Range = (RangeCharacteristic)this.Range.Clone(),
                SummonableCreaturesBoost = this.SummonableCreaturesBoost.Clone(),
                Reflect = this.Reflect.Clone(),
                CriticalHit = this.CriticalHit.Clone(),
                CriticalHitWeapon = this.CriticalHitWeapon,
                HealBonus = this.HealBonus.Clone(),
                AllDamagesBonus = this.AllDamagesBonus.Clone(),
                DamagesBonusPercent = this.DamagesBonusPercent.Clone(),
                WeaponDamagesBonusPercent = this.WeaponDamagesBonusPercent.Clone(),
                TrapBonus = this.TrapBonus.Clone(),
                TrapBonusPercent = this.TrapBonusPercent.Clone(),
                GlyphBonusPercent = this.GlyphBonusPercent.Clone(),
                RuneBonusPercent = this.RuneBonusPercent.Clone(),
                PermanentDamagePercent = this.PermanentDamagePercent.Clone(),
                PushDamageBonus = this.PushDamageBonus.Clone(),
                CriticalDamageBonus = this.CriticalDamageBonus.Clone(),
                NeutralDamageBonus = this.NeutralDamageBonus.Clone(),
                EarthDamageBonus = this.EarthDamageBonus.Clone(),
                WaterDamageBonus = this.WaterDamageBonus.Clone(),
                AirDamageBonus = this.AirDamageBonus.Clone(),
                FireDamageBonus = this.FireDamageBonus.Clone(),
                DodgePAProbability = this.DodgePAProbability.Clone(),
                DodgePMProbability = this.DodgePMProbability.Clone(),
                NeutralResistPercent = (ResistanceCharacteristic)this.NeutralResistPercent.Clone(),
                EarthResistPercent = (ResistanceCharacteristic)this.EarthResistPercent.Clone(),
                WaterResistPercent = (ResistanceCharacteristic)this.WaterResistPercent.Clone(),
                AirResistPercent = (ResistanceCharacteristic)this.AirResistPercent.Clone(),
                FireResistPercent = (ResistanceCharacteristic)this.FireResistPercent.Clone(),
                NeutralReduction = this.NeutralReduction.Clone(),
                EarthReduction = this.EarthReduction.Clone(),
                WaterReduction = this.WaterReduction.Clone(),
                AirReduction = this.AirReduction.Clone(),
                FireReduction = this.FireReduction.Clone(),
                PushDamageReduction = this.PushDamageReduction.Clone(),
                CriticalDamageReduction = this.CriticalDamageReduction.Clone(),
                PvPNeutralResistPercent = (ResistanceCharacteristic)this.PvPNeutralResistPercent.Clone(),
                PvPEarthResistPercent = (ResistanceCharacteristic)this.PvPEarthResistPercent.Clone(),
                PvPWaterResistPercent = (ResistanceCharacteristic)this.PvPWaterResistPercent.Clone(),
                PvPAirResistPercent = (ResistanceCharacteristic)this.PvPAirResistPercent.Clone(),
                PvPFireResistPercent = (ResistanceCharacteristic)this.PvPFireResistPercent.Clone(),
                PvPNeutralReduction = this.PvPNeutralReduction.Clone(),
                PvPEarthReduction = this.PvPEarthReduction.Clone(),
                PvPWaterReduction = this.PvPWaterReduction.Clone(),
                PvPAirReduction = this.PvPAirReduction.Clone(),
                PvPFireReduction = this.PvPFireReduction.Clone(),
                GlobalDamageReduction = this.GlobalDamageReduction,
                Strength = this.Strength.Clone(),
                Agility = this.Agility.Clone(),
                Chance = this.Chance.Clone(),
                Vitality = this.Vitality.Clone(),
                Wisdom = this.Wisdom.Clone(),
                Intelligence = this.Intelligence.Clone(),
                TackleBlock = this.TackleBlock.Clone(),
                TackleEvade = this.TackleEvade.Clone(),
                PAAttack = this.PAAttack.Clone(),
                PMAttack = this.PMAttack.Clone(),
                WeightBonus = this.WeightBonus,
                CurrentLifePoints = this.CurrentLifePoints,
                CurrentMaxLifePoints = this.CurrentMaxLifePoints,
                InvisibilityState = GameActionFightInvisibilityStateEnum.VISIBLE,
                ShieldPoints = this.ShieldPoints,
                Summoned = this.Summoned,
                SummonerId = this.SummonerId,
                Erosion = Erosion,
            };
        }
    }
}
