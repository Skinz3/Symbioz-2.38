


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ChatMessageReportMessage : Message
{

public const ushort Id = 821;
public override ushort MessageId
{
    get { return Id; }
}

public string senderName;
        public string content;
        public int timestamp;
        public sbyte channel;
        public string fingerprint;
        public sbyte reason;
        

public ChatMessageReportMessage()
{
}

public ChatMessageReportMessage(string senderName, string content, int timestamp, sbyte channel, string fingerprint, sbyte reason)
        {
            this.senderName = senderName;
            this.content = content;
            this.timestamp = timestamp;
            this.channel = channel;
            this.fingerprint = fingerprint;
            this.reason = reason;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(senderName);
            writer.WriteUTF(content);
            writer.WriteInt(timestamp);
            writer.WriteSByte(channel);
            writer.WriteUTF(fingerprint);
            writer.WriteSByte(reason);
            

}

public override void Deserialize(ICustomDataInput reader)
{

senderName = reader.ReadUTF();
            content = reader.ReadUTF();
            timestamp = reader.ReadInt();
            if (timestamp < 0)
                throw new Exception("Forbidden value on timestamp = " + timestamp + ", it doesn't respect the following condition : timestamp < 0");
            channel = reader.ReadSByte();
            if (channel < 0)
                throw new Exception("Forbidden value on channel = " + channel + ", it doesn't respect the following condition : channel < 0");
            fingerprint = reader.ReadUTF();
            reason = reader.ReadSByte();
            if (reason < 0)
                throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
            

}


}


}