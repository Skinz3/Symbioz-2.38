


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class PaddockInformationsForSell
{

public const short Id = 222;
public virtual short TypeId
{
    get { return Id; }
}

public string guildOwner;
        public short worldX;
        public short worldY;
        public ushort subAreaId;
        public sbyte nbMount;
        public sbyte nbObject;
        public uint price;
        

public PaddockInformationsForSell()
{
}

public PaddockInformationsForSell(string guildOwner, short worldX, short worldY, ushort subAreaId, sbyte nbMount, sbyte nbObject, uint price)
        {
            this.guildOwner = guildOwner;
            this.worldX = worldX;
            this.worldY = worldY;
            this.subAreaId = subAreaId;
            this.nbMount = nbMount;
            this.nbObject = nbObject;
            this.price = price;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(guildOwner);
            writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            writer.WriteVarUhShort(subAreaId);
            writer.WriteSByte(nbMount);
            writer.WriteSByte(nbObject);
            writer.WriteVarUhInt(price);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

guildOwner = reader.ReadUTF();
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            nbMount = reader.ReadSByte();
            nbObject = reader.ReadSByte();
            price = reader.ReadVarUhInt();
            if (price < 0)
                throw new Exception("Forbidden value on price = " + price + ", it doesn't respect the following condition : price < 0");
            

}


}


}