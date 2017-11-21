


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AtlasPointInformationsMessage : Message
{

public const ushort Id = 5956;
public override ushort MessageId
{
    get { return Id; }
}

public Types.AtlasPointsInformations type;
        

public AtlasPointInformationsMessage()
{
}

public AtlasPointInformationsMessage(Types.AtlasPointsInformations type)
        {
            this.type = type;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

type.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

type = new Types.AtlasPointsInformations();
            type.Deserialize(reader);
            

}


}


}