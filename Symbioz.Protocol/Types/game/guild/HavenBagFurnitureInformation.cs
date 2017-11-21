


















// Generated on 04/27/2016 01:13:17
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class HavenBagFurnitureInformation
{

public const short Id = 498;
public virtual short TypeId
{
    get { return Id; }
}

public ushort cellId;
        public int funitureId;
        public sbyte orientation;
        

public HavenBagFurnitureInformation()
{
}

public HavenBagFurnitureInformation(ushort cellId, int funitureId, sbyte orientation)
        {
            this.cellId = cellId;
            this.funitureId = funitureId;
            this.orientation = orientation;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(cellId);
            writer.WriteInt(funitureId);
            writer.WriteSByte(orientation);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

cellId = reader.ReadVarUhShort();
            if (cellId < 0)
                throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < 0");
            funitureId = reader.ReadInt();
            orientation = reader.ReadSByte();
            if (orientation < 0)
                throw new Exception("Forbidden value on orientation = " + orientation + ", it doesn't respect the following condition : orientation < 0");
            

}


}


}