


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class HouseKickRequestMessage : Message
{

public const ushort Id = 5698;
public override ushort MessageId
{
    get { return Id; }
}

public ulong id;
        

public HouseKickRequestMessage()
{
}

public HouseKickRequestMessage(ulong id)
        {
            this.id = id;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(id);
            

}

public override void Deserialize(ICustomDataInput reader)
{

id = reader.ReadVarUhLong();
            if (id < 0 || id > 9007199254740990)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0 || id > 9007199254740990");
            

}


}


}