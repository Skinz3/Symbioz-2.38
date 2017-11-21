


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ChallengeFightJoinRefusedMessage : Message
{

public const ushort Id = 5908;
public override ushort MessageId
{
    get { return Id; }
}

public ulong playerId;
        public sbyte reason;
        

public ChallengeFightJoinRefusedMessage()
{
}

public ChallengeFightJoinRefusedMessage(ulong playerId, sbyte reason)
        {
            this.playerId = playerId;
            this.reason = reason;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(playerId);
            writer.WriteSByte(reason);
            

}

public override void Deserialize(ICustomDataInput reader)
{

playerId = reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740990)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0 || playerId > 9007199254740990");
            reason = reader.ReadSByte();
            

}


}


}