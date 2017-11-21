


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayArenaFighterStatusMessage : Message
{

public const ushort Id = 6281;
public override ushort MessageId
{
    get { return Id; }
}

public int fightId;
        public int playerId;
        public bool accepted;
        

public GameRolePlayArenaFighterStatusMessage()
{
}

public GameRolePlayArenaFighterStatusMessage(int fightId, int playerId, bool accepted)
        {
            this.fightId = fightId;
            this.playerId = playerId;
            this.accepted = accepted;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(fightId);
            writer.WriteInt(playerId);
            writer.WriteBoolean(accepted);
            

}

public override void Deserialize(ICustomDataInput reader)
{

fightId = reader.ReadInt();
            playerId = reader.ReadInt();
            accepted = reader.ReadBoolean();
            

}


}


}