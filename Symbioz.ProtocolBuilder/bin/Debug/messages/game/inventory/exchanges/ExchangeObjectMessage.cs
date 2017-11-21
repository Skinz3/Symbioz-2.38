


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeObjectMessage : Message
{

public const ushort Id = 5515;
public override ushort MessageId
{
    get { return Id; }
}

public bool remote;
        

public ExchangeObjectMessage()
{
}

public ExchangeObjectMessage(bool remote)
        {
            this.remote = remote;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(remote);
            

}

public override void Deserialize(ICustomDataInput reader)
{

remote = reader.ReadBoolean();
            

}


}


}