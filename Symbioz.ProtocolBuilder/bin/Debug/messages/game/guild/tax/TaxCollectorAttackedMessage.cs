


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class TaxCollectorAttackedMessage : Message
{

public const ushort Id = 5918;
public override ushort MessageId
{
    get { return Id; }
}

public ushort firstNameId;
        public ushort lastNameId;
        public short worldX;
        public short worldY;
        public int mapId;
        public ushort subAreaId;
        public Types.BasicGuildInformations guild;
        

public TaxCollectorAttackedMessage()
{
}

public TaxCollectorAttackedMessage(ushort firstNameId, ushort lastNameId, short worldX, short worldY, int mapId, ushort subAreaId, Types.BasicGuildInformations guild)
        {
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.guild = guild;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(firstNameId);
            writer.WriteVarUhShort(lastNameId);
            writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            writer.WriteInt(mapId);
            writer.WriteVarUhShort(subAreaId);
            guild.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

firstNameId = reader.ReadVarUhShort();
            if (firstNameId < 0)
                throw new Exception("Forbidden value on firstNameId = " + firstNameId + ", it doesn't respect the following condition : firstNameId < 0");
            lastNameId = reader.ReadVarUhShort();
            if (lastNameId < 0)
                throw new Exception("Forbidden value on lastNameId = " + lastNameId + ", it doesn't respect the following condition : lastNameId < 0");
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
            guild = new Types.BasicGuildInformations();
            guild.Deserialize(reader);
            

}


}


}