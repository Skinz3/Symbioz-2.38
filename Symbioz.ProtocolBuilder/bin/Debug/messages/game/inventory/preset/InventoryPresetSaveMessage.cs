


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class InventoryPresetSaveMessage : Message
{

public const ushort Id = 6165;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte presetId;
        public sbyte symbolId;
        public bool saveEquipment;
        

public InventoryPresetSaveMessage()
{
}

public InventoryPresetSaveMessage(sbyte presetId, sbyte symbolId, bool saveEquipment)
        {
            this.presetId = presetId;
            this.symbolId = symbolId;
            this.saveEquipment = saveEquipment;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(presetId);
            writer.WriteSByte(symbolId);
            writer.WriteBoolean(saveEquipment);
            

}

public override void Deserialize(ICustomDataInput reader)
{

presetId = reader.ReadSByte();
            if (presetId < 0)
                throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
            symbolId = reader.ReadSByte();
            if (symbolId < 0)
                throw new Exception("Forbidden value on symbolId = " + symbolId + ", it doesn't respect the following condition : symbolId < 0");
            saveEquipment = reader.ReadBoolean();
            

}


}


}