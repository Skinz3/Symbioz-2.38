


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class InventoryPresetItemUpdateRequestMessage : Message
{

public const ushort Id = 6210;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte presetId;
        public byte position;
        public uint objUid;
        

public InventoryPresetItemUpdateRequestMessage()
{
}

public InventoryPresetItemUpdateRequestMessage(sbyte presetId, byte position, uint objUid)
        {
            this.presetId = presetId;
            this.position = position;
            this.objUid = objUid;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(presetId);
            writer.WriteByte(position);
            writer.WriteVarUhInt(objUid);
            

}

public override void Deserialize(ICustomDataInput reader)
{

presetId = reader.ReadSByte();
            if (presetId < 0)
                throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
            position = reader.ReadByte();
            if (position < 0 || position > 255)
                throw new Exception("Forbidden value on position = " + position + ", it doesn't respect the following condition : position < 0 || position > 255");
            objUid = reader.ReadVarUhInt();
            if (objUid < 0)
                throw new Exception("Forbidden value on objUid = " + objUid + ", it doesn't respect the following condition : objUid < 0");
            

}


}


}