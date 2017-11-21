


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ServerStatusUpdateMessage : Message
{

public const ushort Id = 50;
public override ushort MessageId
{
    get { return Id; }
}

public Types.GameServerInformations server;
        

public ServerStatusUpdateMessage()
{
}

public ServerStatusUpdateMessage(Types.GameServerInformations server)
        {
            this.server = server;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

server.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

server = new Types.GameServerInformations();
            server.Deserialize(reader);
            

}


}


}