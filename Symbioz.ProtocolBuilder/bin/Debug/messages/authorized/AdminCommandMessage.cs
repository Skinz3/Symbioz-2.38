


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AdminCommandMessage : Message
{

public const ushort Id = 76;
public override ushort MessageId
{
    get { return Id; }
}

public string content;
        

public AdminCommandMessage()
{
}

public AdminCommandMessage(string content)
        {
            this.content = content;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(content);
            

}

public override void Deserialize(ICustomDataInput reader)
{

content = reader.ReadUTF();
            

}


}


}