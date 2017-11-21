


















// Generated on 04/27/2016 01:13:08
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class StatisticDataShort : StatisticData
{

public const short Id = 488;
public override short TypeId
{
    get { return Id; }
}

public short value;
        

public StatisticDataShort()
{
}

public StatisticDataShort(short value)
        {
            this.value = value;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteShort(value);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            value = reader.ReadShort();
            

}


}


}