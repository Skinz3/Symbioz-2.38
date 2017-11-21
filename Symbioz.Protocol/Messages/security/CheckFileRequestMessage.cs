


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CheckFileRequestMessage : Message
{

public const ushort Id = 6154;
public override ushort MessageId
{
    get { return Id; }
}

public string filename;
        public sbyte type;
        

public CheckFileRequestMessage()
{
}

public CheckFileRequestMessage(string filename, sbyte type)
        {
            this.filename = filename;
            this.type = type;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(filename);
            writer.WriteSByte(type);
            

}

public override void Deserialize(ICustomDataInput reader)
{

filename = reader.ReadUTF();
            type = reader.ReadSByte();
            if (type < 0)
                throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
            

}


}


}