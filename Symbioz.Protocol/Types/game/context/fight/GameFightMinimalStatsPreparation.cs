


















// Generated on 04/27/2016 01:13:12
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameFightMinimalStatsPreparation : GameFightMinimalStats
{

public const short Id = 360;
public override short TypeId
{
    get { return Id; }
}

public uint initiative;
        

public GameFightMinimalStatsPreparation()
{
}

public GameFightMinimalStatsPreparation(uint lifePoints, uint maxLifePoints, uint baseMaxLifePoints, uint permanentDamagePercent, uint shieldPoints, short actionPoints, short maxActionPoints, short movementPoints, short maxMovementPoints, double summoner, bool summoned, short neutralElementResistPercent, short earthElementResistPercent, short waterElementResistPercent, short airElementResistPercent, short fireElementResistPercent, short neutralElementReduction, short earthElementReduction, short waterElementReduction, short airElementReduction, short fireElementReduction, short criticalDamageFixedResist, short pushDamageFixedResist, short pvpNeutralElementResistPercent, short pvpEarthElementResistPercent, short pvpWaterElementResistPercent, short pvpAirElementResistPercent, short pvpFireElementResistPercent, short pvpNeutralElementReduction, short pvpEarthElementReduction, short pvpWaterElementReduction, short pvpAirElementReduction, short pvpFireElementReduction, ushort dodgePALostProbability, ushort dodgePMLostProbability, short tackleBlock, short tackleEvade, short fixedDamageReflection, sbyte invisibilityState, uint initiative)
         : base(lifePoints, maxLifePoints, baseMaxLifePoints, permanentDamagePercent, shieldPoints, actionPoints, maxActionPoints, movementPoints, maxMovementPoints, summoner, summoned, neutralElementResistPercent, earthElementResistPercent, waterElementResistPercent, airElementResistPercent, fireElementResistPercent, neutralElementReduction, earthElementReduction, waterElementReduction, airElementReduction, fireElementReduction, criticalDamageFixedResist, pushDamageFixedResist, pvpNeutralElementResistPercent, pvpEarthElementResistPercent, pvpWaterElementResistPercent, pvpAirElementResistPercent, pvpFireElementResistPercent, pvpNeutralElementReduction, pvpEarthElementReduction, pvpWaterElementReduction, pvpAirElementReduction, pvpFireElementReduction, dodgePALostProbability, dodgePMLostProbability, tackleBlock, tackleEvade, fixedDamageReflection, invisibilityState)
        {
            this.initiative = initiative;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(initiative);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            initiative = reader.ReadVarUhInt();
            if (initiative < 0)
                throw new Exception("Forbidden value on initiative = " + initiative + ", it doesn't respect the following condition : initiative < 0");
            

}


}


}