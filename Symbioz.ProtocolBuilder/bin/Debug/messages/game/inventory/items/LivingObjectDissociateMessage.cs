


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class LivingObjectDissociateMessage : Message
{

public const ushort Id = 5723;
public override ushort MessageId
{
    get { return Id; }
}

public uint livingUID;
        public byte livingPosition;
        

public LivingObjectDissociateMessage()
{
}

public LivingObjectDissociateMessage(uint livingUID, byte livingPosition)
        {
            this.livingUID = livingUID;
            this.livingPosition = livingPosition;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(livingUID);
            writer.WriteByte(livingPosition);
            

}

public override void Deserialize(ICustomDataInput reader)
{

livingUID = reader.ReadVarUhInt();
            if (livingUID < 0)
                throw new Exception("Forbidden value on livingUID = " + livingUID + ", it doesn't respect the following condition : livingUID < 0");
            livingPosition = reader.ReadByte();
            if (livingPosition < 0 || livingPosition > 255)
                throw new Exception("Forbidden value on livingPosition = " + livingPosition + ", it doesn't respect the following condition : livingPosition < 0 || livingPosition > 255");
            

}


}


}