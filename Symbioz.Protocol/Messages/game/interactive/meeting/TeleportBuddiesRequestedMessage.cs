


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class TeleportBuddiesRequestedMessage : Message
{

public const ushort Id = 6302;
public override ushort MessageId
{
    get { return Id; }
}

public ushort dungeonId;
        public ulong inviterId;
        public ulong[] invalidBuddiesIds;
        

public TeleportBuddiesRequestedMessage()
{
}

public TeleportBuddiesRequestedMessage(ushort dungeonId, ulong inviterId, ulong[] invalidBuddiesIds)
        {
            this.dungeonId = dungeonId;
            this.inviterId = inviterId;
            this.invalidBuddiesIds = invalidBuddiesIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(dungeonId);
            writer.WriteVarUhLong(inviterId);
            writer.WriteUShort((ushort)invalidBuddiesIds.Length);
            foreach (var entry in invalidBuddiesIds)
            {
                 writer.WriteVarUhLong(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

dungeonId = reader.ReadVarUhShort();
            if (dungeonId < 0)
                throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
            inviterId = reader.ReadVarUhLong();
            if (inviterId < 0 || inviterId > 9007199254740990)
                throw new Exception("Forbidden value on inviterId = " + inviterId + ", it doesn't respect the following condition : inviterId < 0 || inviterId > 9007199254740990");
            var limit = reader.ReadUShort();
            invalidBuddiesIds = new ulong[limit];
            for (int i = 0; i < limit; i++)
            {
                 invalidBuddiesIds[i] = reader.ReadVarUhLong();
            }
            

}


}


}