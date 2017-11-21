


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ChatClientMultiMessage : ChatAbstractClientMessage
{

public const ushort Id = 861;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte channel;
        

public ChatClientMultiMessage()
{
}

public ChatClientMultiMessage(string content, sbyte channel)
         : base(content)
        {
            this.channel = channel;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(channel);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            channel = reader.ReadSByte();
            if (channel < 0)
                throw new Exception("Forbidden value on channel = " + channel + ", it doesn't respect the following condition : channel < 0");
            

}


}


}