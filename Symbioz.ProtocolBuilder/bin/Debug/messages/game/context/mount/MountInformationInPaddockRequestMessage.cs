


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MountInformationInPaddockRequestMessage : Message
{

public const ushort Id = 5975;
public override ushort MessageId
{
    get { return Id; }
}

public int mapRideId;
        

public MountInformationInPaddockRequestMessage()
{
}

public MountInformationInPaddockRequestMessage(int mapRideId)
        {
            this.mapRideId = mapRideId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarInt(mapRideId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

mapRideId = reader.ReadVarInt();
            

}


}


}