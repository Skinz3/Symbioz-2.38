


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class InventoryPresetUpdateMessage : Message
{

public const ushort Id = 6171;
public override ushort MessageId
{
    get { return Id; }
}

public Types.Preset preset;
        

public InventoryPresetUpdateMessage()
{
}

public InventoryPresetUpdateMessage(Types.Preset preset)
        {
            this.preset = preset;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

preset.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

preset = new Types.Preset();
            preset.Deserialize(reader);
            

}


}


}