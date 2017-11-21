


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class PartyMemberArenaInformations : PartyMemberInformations
{

public const short Id = 391;
public override short TypeId
{
    get { return Id; }
}

public ushort rank;
        

public PartyMemberArenaInformations()
{
}

public PartyMemberArenaInformations(ulong id, string name, byte level, Types.EntityLook entityLook, sbyte breed, bool sex, uint lifePoints, uint maxLifePoints, ushort prospecting, byte regenRate, ushort initiative, sbyte alignmentSide, short worldX, short worldY, int mapId, ushort subAreaId, Types.PlayerStatus status, Types.PartyCompanionMemberInformations[] companions, ushort rank)
         : base(id, name, level, entityLook, breed, sex, lifePoints, maxLifePoints, prospecting, regenRate, initiative, alignmentSide, worldX, worldY, mapId, subAreaId, status, companions)
        {
            this.rank = rank;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(rank);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            rank = reader.ReadVarUhShort();
            if (rank < 0 || rank > 2300)
                throw new Exception("Forbidden value on rank = " + rank + ", it doesn't respect the following condition : rank < 0 || rank > 2300");
            

}


}


}