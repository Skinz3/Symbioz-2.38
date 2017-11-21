


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildVersatileInfoListMessage : Message
{

public const ushort Id = 6435;
public override ushort MessageId
{
    get { return Id; }
}

public Types.GuildVersatileInformations[] guilds;
        

public GuildVersatileInfoListMessage()
{
}

public GuildVersatileInfoListMessage(Types.GuildVersatileInformations[] guilds)
        {
            this.guilds = guilds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)guilds.Length);
            foreach (var entry in guilds)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            guilds = new Types.GuildVersatileInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 guilds[i] = Types.ProtocolTypeManager.GetInstance<Types.GuildVersatileInformations>(reader.ReadShort());
                 guilds[i].Deserialize(reader);
            }
            

}


}


}