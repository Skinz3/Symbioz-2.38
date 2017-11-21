


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class IdolsPresetDeleteMessage : Message
{

public const ushort Id = 6602;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte presetId;
        

public IdolsPresetDeleteMessage()
{
}

public IdolsPresetDeleteMessage(sbyte presetId)
        {
            this.presetId = presetId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(presetId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

presetId = reader.ReadSByte();
            if (presetId < 0)
                throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
            

}


}


}