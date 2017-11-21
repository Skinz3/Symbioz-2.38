


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ContactLookMessage : Message
{

public const ushort Id = 5934;
public override ushort MessageId
{
    get { return Id; }
}

public uint requestId;
        public string playerName;
        public ulong playerId;
        public Types.EntityLook look;
        

public ContactLookMessage()
{
}

public ContactLookMessage(uint requestId, string playerName, ulong playerId, Types.EntityLook look)
        {
            this.requestId = requestId;
            this.playerName = playerName;
            this.playerId = playerId;
            this.look = look;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(requestId);
            writer.WriteUTF(playerName);
            writer.WriteVarUhLong(playerId);
            look.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

requestId = reader.ReadVarUhInt();
            if (requestId < 0)
                throw new Exception("Forbidden value on requestId = " + requestId + ", it doesn't respect the following condition : requestId < 0");
            playerName = reader.ReadUTF();
            playerId = reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740990)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0 || playerId > 9007199254740990");
            look = new Types.EntityLook();
            look.Deserialize(reader);
            

}


}


}