


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class EntityLook
{

public const short Id = 55;
public virtual short TypeId
{
    get { return Id; }
}

public ushort bonesId;
        public ushort[] skins;
        public int[] indexedColors;
        public short[] scales;
        public SubEntity[] subentities;
        

public EntityLook()
{
}

public EntityLook(ushort bonesId, ushort[] skins, int[] indexedColors, short[] scales, SubEntity[] subentities)
        {
            this.bonesId = bonesId;
            this.skins = skins;
            this.indexedColors = indexedColors;
            this.scales = scales;
            this.subentities = subentities;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(bonesId);
            writer.WriteUShort((ushort)skins.Length);
            foreach (var entry in skins)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)indexedColors.Length);
            foreach (var entry in indexedColors)
            {
                 writer.WriteInt(entry);
            }
            writer.WriteUShort((ushort)scales.Length);
            foreach (var entry in scales)
            {
                 writer.WriteVarShort(entry);
            }
            writer.WriteUShort((ushort)subentities.Length);
            foreach (var entry in subentities)
            {
                 entry.Serialize(writer);
            }
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

bonesId = reader.ReadVarUhShort();
            if (bonesId < 0)
                throw new Exception("Forbidden value on bonesId = " + bonesId + ", it doesn't respect the following condition : bonesId < 0");
            var limit = reader.ReadUShort();
            skins = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 skins[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            indexedColors = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 indexedColors[i] = reader.ReadInt();
            }
            limit = reader.ReadUShort();
            scales = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 scales[i] = reader.ReadVarShort();
            }
            limit = reader.ReadUShort();
            subentities = new SubEntity[limit];
            for (int i = 0; i < limit; i++)
            {
                 subentities[i] = new SubEntity();
                 subentities[i].Deserialize(reader);
            }
            

}


}


}