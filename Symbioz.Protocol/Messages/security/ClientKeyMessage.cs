


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ClientKeyMessage : Message
{

public const ushort Id = 5607;
public override ushort MessageId
{
    get { return Id; }
}

public string key;
        

public ClientKeyMessage()
{
}

public ClientKeyMessage(string key)
        {
            this.key = key;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(key);
            

}

public override void Deserialize(ICustomDataInput reader)
{

key = reader.ReadUTF();
            

}


}


}