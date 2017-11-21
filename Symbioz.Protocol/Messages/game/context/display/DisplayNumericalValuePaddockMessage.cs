


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DisplayNumericalValuePaddockMessage : Message
{

public const ushort Id = 6563;
public override ushort MessageId
{
    get { return Id; }
}

public int rideId;
        public int value;
        public sbyte type;
        

public DisplayNumericalValuePaddockMessage()
{
}

public DisplayNumericalValuePaddockMessage(int rideId, int value, sbyte type)
        {
            this.rideId = rideId;
            this.value = value;
            this.type = type;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(rideId);
            writer.WriteInt(value);
            writer.WriteSByte(type);
            

}

public override void Deserialize(ICustomDataInput reader)
{

rideId = reader.ReadInt();
            value = reader.ReadInt();
            type = reader.ReadSByte();
            if (type < 0)
                throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
            

}


}


}