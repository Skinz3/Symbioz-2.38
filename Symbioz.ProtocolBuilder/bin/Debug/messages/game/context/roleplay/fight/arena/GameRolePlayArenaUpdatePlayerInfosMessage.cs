


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayArenaUpdatePlayerInfosMessage : Message
{

public const ushort Id = 6301;
public override ushort MessageId
{
    get { return Id; }
}

public Types.ArenaRankInfos solo;
        

public GameRolePlayArenaUpdatePlayerInfosMessage()
{
}

public GameRolePlayArenaUpdatePlayerInfosMessage(Types.ArenaRankInfos solo)
        {
            this.solo = solo;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

solo.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

solo = new Types.ArenaRankInfos();
            solo.Deserialize(reader);
            

}


}


}