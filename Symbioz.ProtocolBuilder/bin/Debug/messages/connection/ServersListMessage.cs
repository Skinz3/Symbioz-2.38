


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ServersListMessage : Message
{

public const ushort Id = 30;
public override ushort MessageId
{
    get { return Id; }
}

public Types.GameServerInformations[] servers;
        public ushort alreadyConnectedToServerId;
        public bool canCreateNewCharacter;
        

public ServersListMessage()
{
}

public ServersListMessage(Types.GameServerInformations[] servers, ushort alreadyConnectedToServerId, bool canCreateNewCharacter)
        {
            this.servers = servers;
            this.alreadyConnectedToServerId = alreadyConnectedToServerId;
            this.canCreateNewCharacter = canCreateNewCharacter;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)servers.Length);
            foreach (var entry in servers)
            {
                 entry.Serialize(writer);
            }
            writer.WriteVarUhShort(alreadyConnectedToServerId);
            writer.WriteBoolean(canCreateNewCharacter);
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            servers = new Types.GameServerInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 servers[i] = new Types.GameServerInformations();
                 servers[i].Deserialize(reader);
            }
            alreadyConnectedToServerId = reader.ReadVarUhShort();
            if (alreadyConnectedToServerId < 0)
                throw new Exception("Forbidden value on alreadyConnectedToServerId = " + alreadyConnectedToServerId + ", it doesn't respect the following condition : alreadyConnectedToServerId < 0");
            canCreateNewCharacter = reader.ReadBoolean();
            

}


}


}