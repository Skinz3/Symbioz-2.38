


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildListMessage : Message
{

public const ushort Id = 6413;
public override ushort MessageId
{
    get { return Id; }
}

public Types.GuildInformations[] guilds;
        

public GuildListMessage()
{
}

public GuildListMessage(Types.GuildInformations[] guilds)
        {
            this.guilds = guilds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)guilds.Length);
            foreach (var entry in guilds)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            guilds = new Types.GuildInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 guilds[i] = new Types.GuildInformations();
                 guilds[i].Deserialize(reader);
            }
            

}


}


}