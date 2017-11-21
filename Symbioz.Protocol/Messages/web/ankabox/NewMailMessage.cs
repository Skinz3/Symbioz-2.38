


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class NewMailMessage : MailStatusMessage
{

public const ushort Id = 6292;
public override ushort MessageId
{
    get { return Id; }
}

public int[] sendersAccountId;
        

public NewMailMessage()
{
}

public NewMailMessage(ushort unread, ushort total, int[] sendersAccountId)
         : base(unread, total)
        {
            this.sendersAccountId = sendersAccountId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)sendersAccountId.Length);
            foreach (var entry in sendersAccountId)
            {
                 writer.WriteInt(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            sendersAccountId = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 sendersAccountId[i] = reader.ReadInt();
            }
            

}


}


}