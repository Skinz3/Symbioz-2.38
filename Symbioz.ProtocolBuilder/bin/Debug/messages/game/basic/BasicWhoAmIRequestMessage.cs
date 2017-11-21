


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class BasicWhoAmIRequestMessage : Message
{

public const ushort Id = 5664;
public override ushort MessageId
{
    get { return Id; }
}

public bool verbose;
        

public BasicWhoAmIRequestMessage()
{
}

public BasicWhoAmIRequestMessage(bool verbose)
        {
            this.verbose = verbose;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(verbose);
            

}

public override void Deserialize(ICustomDataInput reader)
{

verbose = reader.ReadBoolean();
            

}


}


}