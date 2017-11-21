


















// Generated on 04/27/2016 01:13:17
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class TaxCollectorInformations
{

public const short Id = 167;
public virtual short TypeId
{
    get { return Id; }
}

public int uniqueId;
        public ushort firtNameId;
        public ushort lastNameId;
        public AdditionalTaxCollectorInformations additionalInfos;
        public short worldX;
        public short worldY;
        public ushort subAreaId;
        public sbyte state;
        public Types.EntityLook look;
        public TaxCollectorComplementaryInformations[] complements;
        

public TaxCollectorInformations()
{
}

public TaxCollectorInformations(int uniqueId, ushort firtNameId, ushort lastNameId, AdditionalTaxCollectorInformations additionalInfos, short worldX, short worldY, ushort subAreaId, sbyte state, Types.EntityLook look, TaxCollectorComplementaryInformations[] complements)
        {
            this.uniqueId = uniqueId;
            this.firtNameId = firtNameId;
            this.lastNameId = lastNameId;
            this.additionalInfos = additionalInfos;
            this.worldX = worldX;
            this.worldY = worldY;
            this.subAreaId = subAreaId;
            this.state = state;
            this.look = look;
            this.complements = complements;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(uniqueId);
            writer.WriteVarUhShort(firtNameId);
            writer.WriteVarUhShort(lastNameId);
            additionalInfos.Serialize(writer);
            writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            writer.WriteVarUhShort(subAreaId);
            writer.WriteSByte(state);
            look.Serialize(writer);
            writer.WriteUShort((ushort)complements.Length);
            foreach (var entry in complements)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

uniqueId = reader.ReadInt();
            firtNameId = reader.ReadVarUhShort();
            if (firtNameId < 0)
                throw new Exception("Forbidden value on firtNameId = " + firtNameId + ", it doesn't respect the following condition : firtNameId < 0");
            lastNameId = reader.ReadVarUhShort();
            if (lastNameId < 0)
                throw new Exception("Forbidden value on lastNameId = " + lastNameId + ", it doesn't respect the following condition : lastNameId < 0");
            additionalInfos = new AdditionalTaxCollectorInformations();
            additionalInfos.Deserialize(reader);
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            state = reader.ReadSByte();
            if (state < 0)
                throw new Exception("Forbidden value on state = " + state + ", it doesn't respect the following condition : state < 0");
            look = new Types.EntityLook();
            look.Deserialize(reader);
            var limit = reader.ReadUShort();
            complements = new TaxCollectorComplementaryInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 complements[i] = Types.ProtocolTypeManager.GetInstance<TaxCollectorComplementaryInformations>(reader.ReadShort());
                 complements[i].Deserialize(reader);
            }
            

}


}


}