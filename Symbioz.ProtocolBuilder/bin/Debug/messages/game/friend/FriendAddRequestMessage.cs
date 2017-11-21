


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class FriendAddRequestMessage : Message
{

public const ushort Id = 4004;
public override ushort MessageId
{
    get { return Id; }
}

public string name;
        

public FriendAddRequestMessage()
{
}

public FriendAddRequestMessage(string name)
        {
            this.name = name;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(name);
            

}

public override void Deserialize(ICustomDataInput reader)
{

name = reader.ReadUTF();
            

}


}


}