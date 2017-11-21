


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class FriendUpdateMessage : Message
{

public const ushort Id = 5924;
public override ushort MessageId
{
    get { return Id; }
}

public Types.FriendInformations friendUpdated;
        

public FriendUpdateMessage()
{
}

public FriendUpdateMessage(Types.FriendInformations friendUpdated)
        {
            this.friendUpdated = friendUpdated;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteShort(friendUpdated.TypeId);
            friendUpdated.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

friendUpdated = Types.ProtocolTypeManager.GetInstance<Types.FriendInformations>(reader.ReadShort());
            friendUpdated.Deserialize(reader);
            

}


}


}