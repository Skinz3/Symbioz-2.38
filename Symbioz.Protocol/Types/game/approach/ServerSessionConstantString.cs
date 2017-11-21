


















// Generated on 04/27/2016 01:13:09
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ServerSessionConstantString : ServerSessionConstant
{

public const short Id = 436;
public override short TypeId
{
    get { return Id; }
}

public string value;
        

public ServerSessionConstantString()
{
}

public ServerSessionConstantString(ushort id, string value)
         : base(id)
        {
            this.value = value;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUTF(value);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            value = reader.ReadUTF();
            

}


}


}