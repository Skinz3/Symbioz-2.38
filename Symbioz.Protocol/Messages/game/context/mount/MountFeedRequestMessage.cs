


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MountFeedRequestMessage : Message
{

public const ushort Id = 6189;
public override ushort MessageId
{
    get { return Id; }
}

public uint mountUid;
        public sbyte mountLocation;
        public uint mountFoodUid;
        public uint quantity;
        

public MountFeedRequestMessage()
{
}

public MountFeedRequestMessage(uint mountUid, sbyte mountLocation, uint mountFoodUid, uint quantity)
        {
            this.mountUid = mountUid;
            this.mountLocation = mountLocation;
            this.mountFoodUid = mountFoodUid;
            this.quantity = quantity;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(mountUid);
            writer.WriteSByte(mountLocation);
            writer.WriteVarUhInt(mountFoodUid);
            writer.WriteVarUhInt(quantity);
            

}

public override void Deserialize(ICustomDataInput reader)
{

mountUid = reader.ReadVarUhInt();
            if (mountUid < 0)
                throw new Exception("Forbidden value on mountUid = " + mountUid + ", it doesn't respect the following condition : mountUid < 0");
            mountLocation = reader.ReadSByte();
            mountFoodUid = reader.ReadVarUhInt();
            if (mountFoodUid < 0)
                throw new Exception("Forbidden value on mountFoodUid = " + mountFoodUid + ", it doesn't respect the following condition : mountFoodUid < 0");
            quantity = reader.ReadVarUhInt();
            if (quantity < 0)
                throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
            

}


}


}