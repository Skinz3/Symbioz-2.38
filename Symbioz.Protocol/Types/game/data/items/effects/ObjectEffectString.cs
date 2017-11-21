


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ObjectEffectString : ObjectEffect
{

public const short Id = 74;
public override short TypeId
{
    get { return Id; }
}

public string value;
        

public ObjectEffectString()
{
}

public ObjectEffectString(ushort actionId, string value)
         : base(actionId)
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