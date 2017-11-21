


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildCreationValidMessage : Message
{

public const ushort Id = 5546;
public override ushort MessageId
{
    get { return Id; }
}

public string guildName;
        public Types.GuildEmblem guildEmblem;
        

public GuildCreationValidMessage()
{
}

public GuildCreationValidMessage(string guildName, Types.GuildEmblem guildEmblem)
        {
            this.guildName = guildName;
            this.guildEmblem = guildEmblem;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(guildName);
            guildEmblem.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

guildName = reader.ReadUTF();
            guildEmblem = new Types.GuildEmblem();
            guildEmblem.Deserialize(reader);
            

}


}


}