


















// Generated on 04/27/2016 01:13:09
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ServerSessionConstantLong : ServerSessionConstant
{

public const short Id = 429;
public override short TypeId
{
    get { return Id; }
}

public double value;
        

public ServerSessionConstantLong()
{
}

public ServerSessionConstantLong(ushort id, double value)
         : base(id)
        {
            this.value = value;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteDouble(value);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            value = reader.ReadDouble();
            if (value < -9007199254740990 || value > 9007199254740990)
                throw new Exception("Forbidden value on value = " + value + ", it doesn't respect the following condition : value < -9007199254740990 || value > 9007199254740990");
            

}


}


}