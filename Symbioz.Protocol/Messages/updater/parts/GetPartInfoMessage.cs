


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GetPartInfoMessage : Message
{

public const ushort Id = 1506;
public override ushort MessageId
{
    get { return Id; }
}

public string id;
        

public GetPartInfoMessage()
{
}

public GetPartInfoMessage(string id)
        {
            this.id = id;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(id);
            

}

public override void Deserialize(ICustomDataInput reader)
{

id = reader.ReadUTF();
            

}


}


}