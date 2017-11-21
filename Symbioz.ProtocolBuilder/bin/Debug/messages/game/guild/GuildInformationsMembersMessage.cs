


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildInformationsMembersMessage : Message
{

public const ushort Id = 5558;
public override ushort MessageId
{
    get { return Id; }
}

public Types.GuildMember[] members;
        

public GuildInformationsMembersMessage()
{
}

public GuildInformationsMembersMessage(Types.GuildMember[] members)
        {
            this.members = members;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)members.Length);
            foreach (var entry in members)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            members = new Types.GuildMember[limit];
            for (int i = 0; i < limit; i++)
            {
                 members[i] = new Types.GuildMember();
                 members[i].Deserialize(reader);
            }
            

}


}


}