


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildInvitationByNameMessage : Message
{

public const ushort Id = 6115;
public override ushort MessageId
{
    get { return Id; }
}

public string name;
        

public GuildInvitationByNameMessage()
{
}

public GuildInvitationByNameMessage(string name)
        {
            this.name = name;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(name);
            

}

public override void Deserialize(ICustomDataInput reader)
{

name = reader.ReadUTF();
            

}


}


}