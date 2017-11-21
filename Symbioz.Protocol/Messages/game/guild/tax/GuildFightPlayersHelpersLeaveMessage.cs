


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildFightPlayersHelpersLeaveMessage : Message
{

public const ushort Id = 5719;
public override ushort MessageId
{
    get { return Id; }
}

public int fightId;
        public ulong playerId;
        

public GuildFightPlayersHelpersLeaveMessage()
{
}

public GuildFightPlayersHelpersLeaveMessage(int fightId, ulong playerId)
        {
            this.fightId = fightId;
            this.playerId = playerId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(fightId);
            writer.WriteVarUhLong(playerId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

fightId = reader.ReadInt();
            if (fightId < 0)
                throw new Exception("Forbidden value on fightId = " + fightId + ", it doesn't respect the following condition : fightId < 0");
            playerId = reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740990)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0 || playerId > 9007199254740990");
            

}


}


}