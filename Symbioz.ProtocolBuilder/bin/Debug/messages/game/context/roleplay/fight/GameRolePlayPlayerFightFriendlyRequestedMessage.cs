


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayPlayerFightFriendlyRequestedMessage : Message
{

public const ushort Id = 5937;
public override ushort MessageId
{
    get { return Id; }
}

public int fightId;
        public ulong sourceId;
        public ulong targetId;
        

public GameRolePlayPlayerFightFriendlyRequestedMessage()
{
}

public GameRolePlayPlayerFightFriendlyRequestedMessage(int fightId, ulong sourceId, ulong targetId)
        {
            this.fightId = fightId;
            this.sourceId = sourceId;
            this.targetId = targetId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(fightId);
            writer.WriteVarUhLong(sourceId);
            writer.WriteVarUhLong(targetId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

fightId = reader.ReadInt();
            if (fightId < 0)
                throw new Exception("Forbidden value on fightId = " + fightId + ", it doesn't respect the following condition : fightId < 0");
            sourceId = reader.ReadVarUhLong();
            if (sourceId < 0 || sourceId > 9007199254740990)
                throw new Exception("Forbidden value on sourceId = " + sourceId + ", it doesn't respect the following condition : sourceId < 0 || sourceId > 9007199254740990");
            targetId = reader.ReadVarUhLong();
            if (targetId < 0 || targetId > 9007199254740990)
                throw new Exception("Forbidden value on targetId = " + targetId + ", it doesn't respect the following condition : targetId < 0 || targetId > 9007199254740990");
            

}


}


}