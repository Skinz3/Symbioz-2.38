


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildMotdSetRequestMessage : SocialNoticeSetRequestMessage
{

public const ushort Id = 6588;
public override ushort MessageId
{
    get { return Id; }
}

public string content;
        

public GuildMotdSetRequestMessage()
{
}

public GuildMotdSetRequestMessage(string content)
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