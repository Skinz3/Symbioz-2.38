


















// Generated on 04/27/2016 01:13:17
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class HouseInformations
{

public const short Id = 111;
public virtual short TypeId
{
    get { return Id; }
}

public uint houseId;
        public int[] doorsOnMap;
        public string ownerName;
        public ushort modelId;
        

public HouseInformations()
{
}

public HouseInformations(uint houseId, int[] doorsOnMap, string ownerName, ushort modelId)
        {
            this.houseId = houseId;
            this.doorsOnMap = doorsOnMap;
            this.ownerName = ownerName;
            this.modelId = modelId;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(houseId);
            writer.WriteUShort((ushort)doorsOnMap.Length);
            foreach (var entry in doorsOnMap)
            {
                 writer.WriteInt(entry);
            }
            writer.WriteUTF(ownerName);
            writer.WriteVarUhShort(modelId);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

houseId = reader.ReadVarUhInt();
            if (houseId < 0)
                throw new Exception("Forbidden value on houseId = " + houseId + ", it doesn't respect the following condition : houseId < 0");
            var limit = reader.ReadUShort();
            doorsOnMap = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 doorsOnMap[i] = reader.ReadInt();
            }
            ownerName = reader.ReadUTF();
            modelId = reader.ReadVarUhShort();
            if (modelId < 0)
                throw new Exception("Forbidden value on modelId = " + modelId + ", it doesn't respect the following condition : modelId < 0");
            

}


}


}