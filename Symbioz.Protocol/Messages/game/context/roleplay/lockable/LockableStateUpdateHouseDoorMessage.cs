


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class LockableStateUpdateHouseDoorMessage : LockableStateUpdateAbstractMessage
{

public const ushort Id = 5668;
public override ushort MessageId
{
    get { return Id; }
}

public uint houseId;
        

public LockableStateUpdateHouseDoorMessage()
{
}

public LockableStateUpdateHouseDoorMessage(bool locked, uint houseId)
         : base(locked)
        {
            this.houseId = houseId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(houseId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            houseId = reader.ReadVarUhInt();
            if (houseId < 0)
                throw new Exception("Forbidden value on houseId = " + houseId + ", it doesn't respect the following condition : houseId < 0");
            

}


}


}