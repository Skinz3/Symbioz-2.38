


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class FriendsListMessage : Message
{

public const ushort Id = 4002;
public override ushort MessageId
{
    get { return Id; }
}

public Types.FriendInformations[] friendsList;
        

public FriendsListMessage()
{
}

public FriendsListMessage(Types.FriendInformations[] friendsList)
        {
            this.friendsList = friendsList;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)friendsList.Length);
            foreach (var entry in friendsList)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            friendsList = new Types.FriendInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 friendsList[i] = Types.ProtocolTypeManager.GetInstance<Types.FriendInformations>(reader.ReadShort());
                 friendsList[i].Deserialize(reader);
            }
            

}


}


}