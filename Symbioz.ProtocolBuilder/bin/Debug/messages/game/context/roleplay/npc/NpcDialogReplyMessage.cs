


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class NpcDialogReplyMessage : Message
{

public const ushort Id = 5616;
public override ushort MessageId
{
    get { return Id; }
}

public ushort replyId;
        

public NpcDialogReplyMessage()
{
}

public NpcDialogReplyMessage(ushort replyId)
        {
            this.replyId = replyId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(replyId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

replyId = reader.ReadVarUhShort();
            if (replyId < 0)
                throw new Exception("Forbidden value on replyId = " + replyId + ", it doesn't respect the following condition : replyId < 0");
            

}


}


}