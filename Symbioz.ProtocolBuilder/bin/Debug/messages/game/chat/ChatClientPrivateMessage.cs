


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ChatClientPrivateMessage : ChatAbstractClientMessage
{

public const ushort Id = 851;
public override ushort MessageId
{
    get { return Id; }
}

public string receiver;
        

public ChatClientPrivateMessage()
{
}

public ChatClientPrivateMessage(string content, string receiver)
         : base(content)
        {
            this.receiver = receiver;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUTF(receiver);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            receiver = reader.ReadUTF();
            

}


}


}