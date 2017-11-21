


















// Generated on 04/27/2016 01:13:09
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ServerSessionConstantInteger : ServerSessionConstant
{

public const short Id = 433;
public override short TypeId
{
    get { return Id; }
}

public int value;
        

public ServerSessionConstantInteger()
{
}

public ServerSessionConstantInteger(ushort id, int value)
         : base(id)
        {
            this.value = value;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteInt(value);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            value = reader.ReadInt();
            

}


}


}