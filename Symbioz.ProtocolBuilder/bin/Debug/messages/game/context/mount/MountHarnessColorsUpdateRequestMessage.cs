


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MountHarnessColorsUpdateRequestMessage : Message
{

public const ushort Id = 6697;
public override ushort MessageId
{
    get { return Id; }
}

public bool useHarnessColors;
        

public MountHarnessColorsUpdateRequestMessage()
{
}

public MountHarnessColorsUpdateRequestMessage(bool useHarnessColors)
        {
            this.useHarnessColors = useHarnessColors;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(useHarnessColors);
            

}

public override void Deserialize(ICustomDataInput reader)
{

useHarnessColors = reader.ReadBoolean();
            

}


}


}