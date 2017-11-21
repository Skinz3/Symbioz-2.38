


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ObjectEffectMinMax : ObjectEffect
{

public const short Id = 82;
public override short TypeId
{
    get { return Id; }
}

public uint min;
        public uint max;
        

public ObjectEffectMinMax()
{
}

public ObjectEffectMinMax(ushort actionId, uint min, uint max)
         : base(actionId)
        {
            this.min = min;
            this.max = max;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(min);
            writer.WriteVarUhInt(max);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            min = reader.ReadVarUhInt();
            if (min < 0)
                throw new Exception("Forbidden value on min = " + min + ", it doesn't respect the following condition : min < 0");
            max = reader.ReadVarUhInt();
            if (max < 0)
                throw new Exception("Forbidden value on max = " + max + ", it doesn't respect the following condition : max < 0");
            

}


}


}