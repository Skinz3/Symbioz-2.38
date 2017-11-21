


















// Generated on 04/27/2016 01:13:19
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ShortcutObjectPreset : ShortcutObject
{

public const short Id = 370;
public override short TypeId
{
    get { return Id; }
}

public sbyte presetId;
        

public ShortcutObjectPreset()
{
}

public ShortcutObjectPreset(sbyte slot, sbyte presetId)
         : base(slot)
        {
            this.presetId = presetId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(presetId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            presetId = reader.ReadSByte();
            if (presetId < 0)
                throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
            

}


}


}