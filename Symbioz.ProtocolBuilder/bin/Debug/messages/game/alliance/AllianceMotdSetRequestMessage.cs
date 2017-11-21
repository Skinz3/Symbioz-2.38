


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceMotdSetRequestMessage : SocialNoticeSetRequestMessage
{

public const ushort Id = 6687;
public override ushort MessageId
{
    get { return Id; }
}

public string content;
        

public AllianceMotdSetRequestMessage()
{
}

public AllianceMotdSetRequestMessage(string content)
        {
            this.content = content;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUTF(content);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            content = reader.ReadUTF();
            

}


}


}