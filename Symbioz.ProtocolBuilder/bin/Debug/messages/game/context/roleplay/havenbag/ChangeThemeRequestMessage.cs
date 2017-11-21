


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ChangeThemeRequestMessage : Message
{

public const ushort Id = 6639;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte theme;
        

public ChangeThemeRequestMessage()
{
}

public ChangeThemeRequestMessage(sbyte theme)
        {
            this.theme = theme;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(theme);
            

}

public override void Deserialize(ICustomDataInput reader)
{

theme = reader.ReadSByte();
            

}


}


}