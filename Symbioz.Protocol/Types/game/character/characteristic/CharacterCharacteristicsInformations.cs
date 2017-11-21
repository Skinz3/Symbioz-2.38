


















// Generated on 04/27/2016 01:13:09
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class CharacterCharacteristicsInformations
{

public const short Id = 8;
public virtual short TypeId
{
    get { return Id; }
}

public ulong experience;
        public ulong experienceLevelFloor;
        public ulong experienceNextLevelFloor;
        public int kamas;
        public ushort statsPoints;
        public ushort additionnalPoints;
        public ushort spellsPoints;
        public Types.ActorExtendedAlignmentInformations alignmentInfos;
        public uint lifePoints;
        public uint maxLifePoints;
        public ushort energyPoints;
        public ushort maxEnergyPoints;
        public short actionPointsCurrent;
        public short movementPointsCurrent;
        public CharacterBaseCharacteristic initiative;
        public CharacterBaseCharacteristic prospecting;
        public CharacterBaseCharacteristic actionPoints;
        public CharacterBaseCharacteristic movementPoints;
        public CharacterBaseCharacteristic strength;
        public CharacterBaseCharacteristic vitality;
        public CharacterBaseCharacteristic wisdom;
        public CharacterBaseCharacteristic chance;
        public CharacterBaseCharacteristic agility;
        public CharacterBaseCharacteristic intelligence;
        public CharacterBaseCharacteristic range;
        public CharacterBaseCharacteristic summonableCreaturesBoost;
        public CharacterBaseCharacteristic reflect;
        public CharacterBaseCharacteristic criticalHit;
        public ushort criticalHitWeapon;
        public CharacterBaseCharacteristic criticalMiss;
        public CharacterBaseCharacteristic healBonus;
        public CharacterBaseCharacteristic allDamagesBonus;
        public CharacterBaseCharacteristic weaponDamagesBonusPercent;
        public CharacterBaseCharacteristic damagesBonusPercent;
        public CharacterBaseCharacteristic trapBonus;
        public CharacterBaseCharacteristic trapBonusPercent;
        public CharacterBaseCharacteristic glyphBonusPercent;
        public CharacterBaseCharacteristic runeBonusPercent;
        public CharacterBaseCharacteristic permanentDamagePercent;
        public CharacterBaseCharacteristic tackleBlock;
        public CharacterBaseCharacteristic tackleEvade;
        public CharacterBaseCharacteristic PAAttack;
        public CharacterBaseCharacteristic PMAttack;
        public CharacterBaseCharacteristic pushDamageBonus;
        public CharacterBaseCharacteristic criticalDamageBonus;
        public CharacterBaseCharacteristic neutralDamageBonus;
        public CharacterBaseCharacteristic earthDamageBonus;
        public CharacterBaseCharacteristic waterDamageBonus;
        public CharacterBaseCharacteristic airDamageBonus;
        public CharacterBaseCharacteristic fireDamageBonus;
        public CharacterBaseCharacteristic dodgePALostProbability;
        public CharacterBaseCharacteristic dodgePMLostProbability;
        public CharacterBaseCharacteristic neutralElementResistPercent;
        public CharacterBaseCharacteristic earthElementResistPercent;
        public CharacterBaseCharacteristic waterElementResistPercent;
        public CharacterBaseCharacteristic airElementResistPercent;
        public CharacterBaseCharacteristic fireElementResistPercent;
        public CharacterBaseCharacteristic neutralElementReduction;
        public CharacterBaseCharacteristic earthElementReduction;
        public CharacterBaseCharacteristic waterElementReduction;
        public CharacterBaseCharacteristic airElementReduction;
        public CharacterBaseCharacteristic fireElementReduction;
        public CharacterBaseCharacteristic pushDamageReduction;
        public CharacterBaseCharacteristic criticalDamageReduction;
        public CharacterBaseCharacteristic pvpNeutralElementResistPercent;
        public CharacterBaseCharacteristic pvpEarthElementResistPercent;
        public CharacterBaseCharacteristic pvpWaterElementResistPercent;
        public CharacterBaseCharacteristic pvpAirElementResistPercent;
        public CharacterBaseCharacteristic pvpFireElementResistPercent;
        public CharacterBaseCharacteristic pvpNeutralElementReduction;
        public CharacterBaseCharacteristic pvpEarthElementReduction;
        public CharacterBaseCharacteristic pvpWaterElementReduction;
        public CharacterBaseCharacteristic pvpAirElementReduction;
        public CharacterBaseCharacteristic pvpFireElementReduction;
        public CharacterSpellModification[] spellModifications;
        public int probationTime;
        

public CharacterCharacteristicsInformations()
{
}

public CharacterCharacteristicsInformations(ulong experience, ulong experienceLevelFloor, ulong experienceNextLevelFloor, int kamas, ushort statsPoints, ushort additionnalPoints, ushort spellsPoints, Types.ActorExtendedAlignmentInformations alignmentInfos, uint lifePoints, uint maxLifePoints, ushort energyPoints, ushort maxEnergyPoints, short actionPointsCurrent, short movementPointsCurrent, CharacterBaseCharacteristic initiative, CharacterBaseCharacteristic prospecting, CharacterBaseCharacteristic actionPoints, CharacterBaseCharacteristic movementPoints, CharacterBaseCharacteristic strength, CharacterBaseCharacteristic vitality, CharacterBaseCharacteristic wisdom, CharacterBaseCharacteristic chance, CharacterBaseCharacteristic agility, CharacterBaseCharacteristic intelligence, CharacterBaseCharacteristic range, CharacterBaseCharacteristic summonableCreaturesBoost, CharacterBaseCharacteristic reflect, CharacterBaseCharacteristic criticalHit, ushort criticalHitWeapon, CharacterBaseCharacteristic criticalMiss, CharacterBaseCharacteristic healBonus, CharacterBaseCharacteristic allDamagesBonus, CharacterBaseCharacteristic weaponDamagesBonusPercent, CharacterBaseCharacteristic damagesBonusPercent, CharacterBaseCharacteristic trapBonus, CharacterBaseCharacteristic trapBonusPercent, CharacterBaseCharacteristic glyphBonusPercent, CharacterBaseCharacteristic runeBonusPercent, CharacterBaseCharacteristic permanentDamagePercent, CharacterBaseCharacteristic tackleBlock, CharacterBaseCharacteristic tackleEvade, CharacterBaseCharacteristic PAAttack, CharacterBaseCharacteristic PMAttack, CharacterBaseCharacteristic pushDamageBonus, CharacterBaseCharacteristic criticalDamageBonus, CharacterBaseCharacteristic neutralDamageBonus, CharacterBaseCharacteristic earthDamageBonus, CharacterBaseCharacteristic waterDamageBonus, CharacterBaseCharacteristic airDamageBonus, CharacterBaseCharacteristic fireDamageBonus, CharacterBaseCharacteristic dodgePALostProbability, CharacterBaseCharacteristic dodgePMLostProbability, CharacterBaseCharacteristic neutralElementResistPercent, CharacterBaseCharacteristic earthElementResistPercent, CharacterBaseCharacteristic waterElementResistPercent, CharacterBaseCharacteristic airElementResistPercent, CharacterBaseCharacteristic fireElementResistPercent, CharacterBaseCharacteristic neutralElementReduction, CharacterBaseCharacteristic earthElementReduction, CharacterBaseCharacteristic waterElementReduction, CharacterBaseCharacteristic airElementReduction, CharacterBaseCharacteristic fireElementReduction, CharacterBaseCharacteristic pushDamageReduction, CharacterBaseCharacteristic criticalDamageReduction, CharacterBaseCharacteristic pvpNeutralElementResistPercent, CharacterBaseCharacteristic pvpEarthElementResistPercent, CharacterBaseCharacteristic pvpWaterElementResistPercent, CharacterBaseCharacteristic pvpAirElementResistPercent, CharacterBaseCharacteristic pvpFireElementResistPercent, CharacterBaseCharacteristic pvpNeutralElementReduction, CharacterBaseCharacteristic pvpEarthElementReduction, CharacterBaseCharacteristic pvpWaterElementReduction, CharacterBaseCharacteristic pvpAirElementReduction, CharacterBaseCharacteristic pvpFireElementReduction, CharacterSpellModification[] spellModifications, int probationTime)
        {
            this.experience = experience;
            this.experienceLevelFloor = experienceLevelFloor;
            this.experienceNextLevelFloor = experienceNextLevelFloor;
            this.kamas = kamas;
            this.statsPoints = statsPoints;
            this.additionnalPoints = additionnalPoints;
            this.spellsPoints = spellsPoints;
            this.alignmentInfos = alignmentInfos;
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
            this.energyPoints = energyPoints;
            this.maxEnergyPoints = maxEnergyPoints;
            this.actionPointsCurrent = actionPointsCurrent;
            this.movementPointsCurrent = movementPointsCurrent;
            this.initiative = initiative;
            this.prospecting = prospecting;
            this.actionPoints = actionPoints;
            this.movementPoints = movementPoints;
            this.strength = strength;
            this.vitality = vitality;
            this.wisdom = wisdom;
            this.chance = chance;
            this.agility = agility;
            this.intelligence = intelligence;
            this.range = range;
            this.summonableCreaturesBoost = summonableCreaturesBoost;
            this.reflect = reflect;
            this.criticalHit = criticalHit;
            this.criticalHitWeapon = criticalHitWeapon;
            this.criticalMiss = criticalMiss;
            this.healBonus = healBonus;
            this.allDamagesBonus = allDamagesBonus;
            this.weaponDamagesBonusPercent = weaponDamagesBonusPercent;
            this.damagesBonusPercent = damagesBonusPercent;
            this.trapBonus = trapBonus;
            this.trapBonusPercent = trapBonusPercent;
            this.glyphBonusPercent = glyphBonusPercent;
            this.runeBonusPercent = runeBonusPercent;
            this.permanentDamagePercent = permanentDamagePercent;
            this.tackleBlock = tackleBlock;
            this.tackleEvade = tackleEvade;
            this.PAAttack = PAAttack;
            this.PMAttack = PMAttack;
            this.pushDamageBonus = pushDamageBonus;
            this.criticalDamageBonus = criticalDamageBonus;
            this.neutralDamageBonus = neutralDamageBonus;
            this.earthDamageBonus = earthDamageBonus;
            this.waterDamageBonus = waterDamageBonus;
            this.airDamageBonus = airDamageBonus;
            this.fireDamageBonus = fireDamageBonus;
            this.dodgePALostProbability = dodgePALostProbability;
            this.dodgePMLostProbability = dodgePMLostProbability;
            this.neutralElementResistPercent = neutralElementResistPercent;
            this.earthElementResistPercent = earthElementResistPercent;
            this.waterElementResistPercent = waterElementResistPercent;
            this.airElementResistPercent = airElementResistPercent;
            this.fireElementResistPercent = fireElementResistPercent;
            this.neutralElementReduction = neutralElementReduction;
            this.earthElementReduction = earthElementReduction;
            this.waterElementReduction = waterElementReduction;
            this.airElementReduction = airElementReduction;
            this.fireElementReduction = fireElementReduction;
            this.pushDamageReduction = pushDamageReduction;
            this.criticalDamageReduction = criticalDamageReduction;
            this.pvpNeutralElementResistPercent = pvpNeutralElementResistPercent;
            this.pvpEarthElementResistPercent = pvpEarthElementResistPercent;
            this.pvpWaterElementResistPercent = pvpWaterElementResistPercent;
            this.pvpAirElementResistPercent = pvpAirElementResistPercent;
            this.pvpFireElementResistPercent = pvpFireElementResistPercent;
            this.pvpNeutralElementReduction = pvpNeutralElementReduction;
            this.pvpEarthElementReduction = pvpEarthElementReduction;
            this.pvpWaterElementReduction = pvpWaterElementReduction;
            this.pvpAirElementReduction = pvpAirElementReduction;
            this.pvpFireElementReduction = pvpFireElementReduction;
            this.spellModifications = spellModifications;
            this.probationTime = probationTime;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(experience);
            writer.WriteVarUhLong(experienceLevelFloor);
            writer.WriteVarUhLong(experienceNextLevelFloor);
            writer.WriteInt(kamas);
            writer.WriteVarUhShort(statsPoints);
            writer.WriteVarUhShort(additionnalPoints);
            writer.WriteVarUhShort(spellsPoints);
            alignmentInfos.Serialize(writer);
            writer.WriteVarUhInt(lifePoints);
            writer.WriteVarUhInt(maxLifePoints);
            writer.WriteVarUhShort(energyPoints);
            writer.WriteVarUhShort(maxEnergyPoints);
            writer.WriteVarShort(actionPointsCurrent);
            writer.WriteVarShort(movementPointsCurrent);
            initiative.Serialize(writer);
            prospecting.Serialize(writer);
            actionPoints.Serialize(writer);
            movementPoints.Serialize(writer);
            strength.Serialize(writer);
            vitality.Serialize(writer);
            wisdom.Serialize(writer);
            chance.Serialize(writer);
            agility.Serialize(writer);
            intelligence.Serialize(writer);
            range.Serialize(writer);
            summonableCreaturesBoost.Serialize(writer);
            reflect.Serialize(writer);
            criticalHit.Serialize(writer);
            writer.WriteVarUhShort(criticalHitWeapon);
            criticalMiss.Serialize(writer);
            healBonus.Serialize(writer);
            allDamagesBonus.Serialize(writer);
            weaponDamagesBonusPercent.Serialize(writer);
            damagesBonusPercent.Serialize(writer);
            trapBonus.Serialize(writer);
            trapBonusPercent.Serialize(writer);
            glyphBonusPercent.Serialize(writer);
            runeBonusPercent.Serialize(writer);
            permanentDamagePercent.Serialize(writer);
            tackleBlock.Serialize(writer);
            tackleEvade.Serialize(writer);
            PAAttack.Serialize(writer);
            PMAttack.Serialize(writer);
            pushDamageBonus.Serialize(writer);
            criticalDamageBonus.Serialize(writer);
            neutralDamageBonus.Serialize(writer);
            earthDamageBonus.Serialize(writer);
            waterDamageBonus.Serialize(writer);
            airDamageBonus.Serialize(writer);
            fireDamageBonus.Serialize(writer);
            dodgePALostProbability.Serialize(writer);
            dodgePMLostProbability.Serialize(writer);
            neutralElementResistPercent.Serialize(writer);
            earthElementResistPercent.Serialize(writer);
            waterElementResistPercent.Serialize(writer);
            airElementResistPercent.Serialize(writer);
            fireElementResistPercent.Serialize(writer);
            neutralElementReduction.Serialize(writer);
            earthElementReduction.Serialize(writer);
            waterElementReduction.Serialize(writer);
            airElementReduction.Serialize(writer);
            fireElementReduction.Serialize(writer);
            pushDamageReduction.Serialize(writer);
            criticalDamageReduction.Serialize(writer);
            pvpNeutralElementResistPercent.Serialize(writer);
            pvpEarthElementResistPercent.Serialize(writer);
            pvpWaterElementResistPercent.Serialize(writer);
            pvpAirElementResistPercent.Serialize(writer);
            pvpFireElementResistPercent.Serialize(writer);
            pvpNeutralElementReduction.Serialize(writer);
            pvpEarthElementReduction.Serialize(writer);
            pvpWaterElementReduction.Serialize(writer);
            pvpAirElementReduction.Serialize(writer);
            pvpFireElementReduction.Serialize(writer);
            writer.WriteUShort((ushort)spellModifications.Length);
            foreach (var entry in spellModifications)
            {
                 entry.Serialize(writer);
            }
            writer.WriteInt(probationTime);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

experience = reader.ReadVarUhLong();
            if (experience < 0 || experience > 9007199254740990)
                throw new Exception("Forbidden value on experience = " + experience + ", it doesn't respect the following condition : experience < 0 || experience > 9007199254740990");
            experienceLevelFloor = reader.ReadVarUhLong();
            if (experienceLevelFloor < 0 || experienceLevelFloor > 9007199254740990)
                throw new Exception("Forbidden value on experienceLevelFloor = " + experienceLevelFloor + ", it doesn't respect the following condition : experienceLevelFloor < 0 || experienceLevelFloor > 9007199254740990");
            experienceNextLevelFloor = reader.ReadVarUhLong();
            if (experienceNextLevelFloor < 0 || experienceNextLevelFloor > 9007199254740990)
                throw new Exception("Forbidden value on experienceNextLevelFloor = " + experienceNextLevelFloor + ", it doesn't respect the following condition : experienceNextLevelFloor < 0 || experienceNextLevelFloor > 9007199254740990");
            kamas = reader.ReadInt();
            if (kamas < 0)
                throw new Exception("Forbidden value on kamas = " + kamas + ", it doesn't respect the following condition : kamas < 0");
            statsPoints = reader.ReadVarUhShort();
            if (statsPoints < 0)
                throw new Exception("Forbidden value on statsPoints = " + statsPoints + ", it doesn't respect the following condition : statsPoints < 0");
            additionnalPoints = reader.ReadVarUhShort();
            if (additionnalPoints < 0)
                throw new Exception("Forbidden value on additionnalPoints = " + additionnalPoints + ", it doesn't respect the following condition : additionnalPoints < 0");
            spellsPoints = reader.ReadVarUhShort();
            if (spellsPoints < 0)
                throw new Exception("Forbidden value on spellsPoints = " + spellsPoints + ", it doesn't respect the following condition : spellsPoints < 0");
            alignmentInfos = new Types.ActorExtendedAlignmentInformations();
            alignmentInfos.Deserialize(reader);
            lifePoints = reader.ReadVarUhInt();
            if (lifePoints < 0)
                throw new Exception("Forbidden value on lifePoints = " + lifePoints + ", it doesn't respect the following condition : lifePoints < 0");
            maxLifePoints = reader.ReadVarUhInt();
            if (maxLifePoints < 0)
                throw new Exception("Forbidden value on maxLifePoints = " + maxLifePoints + ", it doesn't respect the following condition : maxLifePoints < 0");
            energyPoints = reader.ReadVarUhShort();
            if (energyPoints < 0)
                throw new Exception("Forbidden value on energyPoints = " + energyPoints + ", it doesn't respect the following condition : energyPoints < 0");
            maxEnergyPoints = reader.ReadVarUhShort();
            if (maxEnergyPoints < 0)
                throw new Exception("Forbidden value on maxEnergyPoints = " + maxEnergyPoints + ", it doesn't respect the following condition : maxEnergyPoints < 0");
            actionPointsCurrent = reader.ReadVarShort();
            movementPointsCurrent = reader.ReadVarShort();
            initiative = new CharacterBaseCharacteristic();
            initiative.Deserialize(reader);
            prospecting = new CharacterBaseCharacteristic();
            prospecting.Deserialize(reader);
            actionPoints = new CharacterBaseCharacteristic();
            actionPoints.Deserialize(reader);
            movementPoints = new CharacterBaseCharacteristic();
            movementPoints.Deserialize(reader);
            strength = new CharacterBaseCharacteristic();
            strength.Deserialize(reader);
            vitality = new CharacterBaseCharacteristic();
            vitality.Deserialize(reader);
            wisdom = new CharacterBaseCharacteristic();
            wisdom.Deserialize(reader);
            chance = new CharacterBaseCharacteristic();
            chance.Deserialize(reader);
            agility = new CharacterBaseCharacteristic();
            agility.Deserialize(reader);
            intelligence = new CharacterBaseCharacteristic();
            intelligence.Deserialize(reader);
            range = new CharacterBaseCharacteristic();
            range.Deserialize(reader);
            summonableCreaturesBoost = new CharacterBaseCharacteristic();
            summonableCreaturesBoost.Deserialize(reader);
            reflect = new CharacterBaseCharacteristic();
            reflect.Deserialize(reader);
            criticalHit = new CharacterBaseCharacteristic();
            criticalHit.Deserialize(reader);
            criticalHitWeapon = reader.ReadVarUhShort();
            if (criticalHitWeapon < 0)
                throw new Exception("Forbidden value on criticalHitWeapon = " + criticalHitWeapon + ", it doesn't respect the following condition : criticalHitWeapon < 0");
            criticalMiss = new CharacterBaseCharacteristic();
            criticalMiss.Deserialize(reader);
            healBonus = new CharacterBaseCharacteristic();
            healBonus.Deserialize(reader);
            allDamagesBonus = new CharacterBaseCharacteristic();
            allDamagesBonus.Deserialize(reader);
            weaponDamagesBonusPercent = new CharacterBaseCharacteristic();
            weaponDamagesBonusPercent.Deserialize(reader);
            damagesBonusPercent = new CharacterBaseCharacteristic();
            damagesBonusPercent.Deserialize(reader);
            trapBonus = new CharacterBaseCharacteristic();
            trapBonus.Deserialize(reader);
            trapBonusPercent = new CharacterBaseCharacteristic();
            trapBonusPercent.Deserialize(reader);
            glyphBonusPercent = new CharacterBaseCharacteristic();
            glyphBonusPercent.Deserialize(reader);
            runeBonusPercent = new CharacterBaseCharacteristic();
            runeBonusPercent.Deserialize(reader);
            permanentDamagePercent = new CharacterBaseCharacteristic();
            permanentDamagePercent.Deserialize(reader);
            tackleBlock = new CharacterBaseCharacteristic();
            tackleBlock.Deserialize(reader);
            tackleEvade = new CharacterBaseCharacteristic();
            tackleEvade.Deserialize(reader);
            PAAttack = new CharacterBaseCharacteristic();
            PAAttack.Deserialize(reader);
            PMAttack = new CharacterBaseCharacteristic();
            PMAttack.Deserialize(reader);
            pushDamageBonus = new CharacterBaseCharacteristic();
            pushDamageBonus.Deserialize(reader);
            criticalDamageBonus = new CharacterBaseCharacteristic();
            criticalDamageBonus.Deserialize(reader);
            neutralDamageBonus = new CharacterBaseCharacteristic();
            neutralDamageBonus.Deserialize(reader);
            earthDamageBonus = new CharacterBaseCharacteristic();
            earthDamageBonus.Deserialize(reader);
            waterDamageBonus = new CharacterBaseCharacteristic();
            waterDamageBonus.Deserialize(reader);
            airDamageBonus = new CharacterBaseCharacteristic();
            airDamageBonus.Deserialize(reader);
            fireDamageBonus = new CharacterBaseCharacteristic();
            fireDamageBonus.Deserialize(reader);
            dodgePALostProbability = new CharacterBaseCharacteristic();
            dodgePALostProbability.Deserialize(reader);
            dodgePMLostProbability = new CharacterBaseCharacteristic();
            dodgePMLostProbability.Deserialize(reader);
            neutralElementResistPercent = new CharacterBaseCharacteristic();
            neutralElementResistPercent.Deserialize(reader);
            earthElementResistPercent = new CharacterBaseCharacteristic();
            earthElementResistPercent.Deserialize(reader);
            waterElementResistPercent = new CharacterBaseCharacteristic();
            waterElementResistPercent.Deserialize(reader);
            airElementResistPercent = new CharacterBaseCharacteristic();
            airElementResistPercent.Deserialize(reader);
            fireElementResistPercent = new CharacterBaseCharacteristic();
            fireElementResistPercent.Deserialize(reader);
            neutralElementReduction = new CharacterBaseCharacteristic();
            neutralElementReduction.Deserialize(reader);
            earthElementReduction = new CharacterBaseCharacteristic();
            earthElementReduction.Deserialize(reader);
            waterElementReduction = new CharacterBaseCharacteristic();
            waterElementReduction.Deserialize(reader);
            airElementReduction = new CharacterBaseCharacteristic();
            airElementReduction.Deserialize(reader);
            fireElementReduction = new CharacterBaseCharacteristic();
            fireElementReduction.Deserialize(reader);
            pushDamageReduction = new CharacterBaseCharacteristic();
            pushDamageReduction.Deserialize(reader);
            criticalDamageReduction = new CharacterBaseCharacteristic();
            criticalDamageReduction.Deserialize(reader);
            pvpNeutralElementResistPercent = new CharacterBaseCharacteristic();
            pvpNeutralElementResistPercent.Deserialize(reader);
            pvpEarthElementResistPercent = new CharacterBaseCharacteristic();
            pvpEarthElementResistPercent.Deserialize(reader);
            pvpWaterElementResistPercent = new CharacterBaseCharacteristic();
            pvpWaterElementResistPercent.Deserialize(reader);
            pvpAirElementResistPercent = new CharacterBaseCharacteristic();
            pvpAirElementResistPercent.Deserialize(reader);
            pvpFireElementResistPercent = new CharacterBaseCharacteristic();
            pvpFireElementResistPercent.Deserialize(reader);
            pvpNeutralElementReduction = new CharacterBaseCharacteristic();
            pvpNeutralElementReduction.Deserialize(reader);
            pvpEarthElementReduction = new CharacterBaseCharacteristic();
            pvpEarthElementReduction.Deserialize(reader);
            pvpWaterElementReduction = new CharacterBaseCharacteristic();
            pvpWaterElementReduction.Deserialize(reader);
            pvpAirElementReduction = new CharacterBaseCharacteristic();
            pvpAirElementReduction.Deserialize(reader);
            pvpFireElementReduction = new CharacterBaseCharacteristic();
            pvpFireElementReduction.Deserialize(reader);
            var limit = reader.ReadUShort();
            spellModifications = new CharacterSpellModification[limit];
            for (int i = 0; i < limit; i++)
            {
                 spellModifications[i] = new CharacterSpellModification();
                 spellModifications[i].Deserialize(reader);
            }
            probationTime = reader.ReadInt();
            if (probationTime < 0)
                throw new Exception("Forbidden value on probationTime = " + probationTime + ", it doesn't respect the following condition : probationTime < 0");
            

}


}


}