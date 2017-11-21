


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class PartyInvitationMemberInformations : CharacterBaseInformations
{

public const short Id = 376;
public override short TypeId
{
    get { return Id; }
}

public short worldX;
        public short worldY;
        public int mapId;
        public ushort subAreaId;
        public Types.PartyCompanionBaseInformations[] companions;
        

public PartyInvitationMemberInformations()
{
}

public PartyInvitationMemberInformations(ulong id, string name, byte level, Types.EntityLook entityLook, sbyte breed, bool sex, short worldX, short worldY, int mapId, ushort subAreaId, Types.PartyCompanionBaseInformations[] companions)
         : base(id, name, level, entityLook, breed, sex)
        {
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.companions = companions;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            writer.WriteInt(mapId);
            writer.WriteVarUhShort(subAreaId);
            writer.WriteUShort((ushort)companions.Length);
            foreach (var entry in companions)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            mapId = reader.ReadInt();
            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            var limit = reader.ReadUShort();
            companions = new Types.PartyCompanionBaseInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 companions[i] = new Types.PartyCompanionBaseInformations();
                 companions[i].Deserialize(reader);
            }
            

}


}


}