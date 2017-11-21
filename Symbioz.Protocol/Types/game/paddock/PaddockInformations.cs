


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class PaddockInformations
{

public const short Id = 132;
public virtual short TypeId
{
    get { return Id; }
}

public ushort maxOutdoorMount;
        public ushort maxItems;
        

public PaddockInformations()
{
}

public PaddockInformations(ushort maxOutdoorMount, ushort maxItems)
        {
            this.maxOutdoorMount = maxOutdoorMount;
            this.maxItems = maxItems;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(maxOutdoorMount);
            writer.WriteVarUhShort(maxItems);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

maxOutdoorMount = reader.ReadVarUhShort();
            if (maxOutdoorMount < 0)
                throw new Exception("Forbidden value on maxOutdoorMount = " + maxOutdoorMount + ", it doesn't respect the following condition : maxOutdoorMount < 0");
            maxItems = reader.ReadVarUhShort();
            if (maxItems < 0)
                throw new Exception("Forbidden value on maxItems = " + maxItems + ", it doesn't respect the following condition : maxItems < 0");
            

}


}


}