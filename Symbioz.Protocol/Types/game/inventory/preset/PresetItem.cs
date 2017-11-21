


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class PresetItem
{

public const short Id = 354;
public virtual short TypeId
{
    get { return Id; }
}

public byte position;
        public ushort objGid;
        public uint objUid;
        

public PresetItem()
{
}

public PresetItem(byte position, ushort objGid, uint objUid)
        {
            this.position = position;
            this.objGid = objGid;
            this.objUid = objUid;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteByte(position);
            writer.WriteVarUhShort(objGid);
            writer.WriteVarUhInt(objUid);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

position = reader.ReadByte();
            if (position < 0 || position > 255)
                throw new Exception("Forbidden value on position = " + position + ", it doesn't respect the following condition : position < 0 || position > 255");
            objGid = reader.ReadVarUhShort();
            if (objGid < 0)
                throw new Exception("Forbidden value on objGid = " + objGid + ", it doesn't respect the following condition : objGid < 0");
            objUid = reader.ReadVarUhInt();
            if (objUid < 0)
                throw new Exception("Forbidden value on objUid = " + objUid + ", it doesn't respect the following condition : objUid < 0");
            

}


}


}