


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ConsoleMessage : Message
{

public const ushort Id = 75;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte type;
        public string content;
        

public ConsoleMessage()
{
}

public ConsoleMessage(sbyte type, string content)
        {
            this.type = type;
            this.content = content;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(type);
            writer.WriteUTF(content);
            

}

public override void Deserialize(ICustomDataInput reader)
{

type = reader.ReadSByte();
            if (type < 0)
                throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
            content = reader.ReadUTF();
            

}


}


}