


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class TeleportToBuddyAnswerMessage : Message
{

public const ushort Id = 6293;
public override ushort MessageId
{
    get { return Id; }
}

public ushort dungeonId;
        public ulong buddyId;
        public bool accept;
        

public TeleportToBuddyAnswerMessage()
{
}

public TeleportToBuddyAnswerMessage(ushort dungeonId, ulong buddyId, bool accept)
        {
            this.dungeonId = dungeonId;
            this.buddyId = buddyId;
            this.accept = accept;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(dungeonId);
            writer.WriteVarUhLong(buddyId);
            writer.WriteBoolean(accept);
            

}

public override void Deserialize(ICustomDataInput reader)
{

dungeonId = reader.ReadVarUhShort();
            if (dungeonId < 0)
                throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
            buddyId = reader.ReadVarUhLong();
            if (buddyId < 0 || buddyId > 9007199254740990)
                throw new Exception("Forbidden value on buddyId = " + buddyId + ", it doesn't respect the following condition : buddyId < 0 || buddyId > 9007199254740990");
            accept = reader.ReadBoolean();
            

}


}


}