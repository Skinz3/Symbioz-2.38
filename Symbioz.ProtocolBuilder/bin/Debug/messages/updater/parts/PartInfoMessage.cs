


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartInfoMessage : Message
{

public const ushort Id = 1508;
public override ushort MessageId
{
    get { return Id; }
}

public Types.ContentPart part;
        public float installationPercent;
        

public PartInfoMessage()
{
}

public PartInfoMessage(Types.ContentPart part, float installationPercent)
        {
            this.part = part;
            this.installationPercent = installationPercent;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

part.Serialize(writer);
            writer.WriteFloat(installationPercent);
            

}

public override void Deserialize(ICustomDataInput reader)
{

part = new Types.ContentPart();
            part.Deserialize(reader);
            installationPercent = reader.ReadFloat();
            

}


}


}