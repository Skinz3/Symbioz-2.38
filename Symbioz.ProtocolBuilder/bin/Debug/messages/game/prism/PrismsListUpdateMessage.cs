


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PrismsListUpdateMessage : PrismsListMessage
{

public const ushort Id = 6438;
public override ushort MessageId
{
    get { return Id; }
}



public PrismsListUpdateMessage()
{
}

public PrismsListUpdateMessage(Types.PrismSubareaEmptyInfo[] prisms)
         : base(prisms)
        {
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            

}


}


}