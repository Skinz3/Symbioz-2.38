


















// Generated on 12/20/2016 16:23:28
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class StatisticDataBoolean : StatisticData
{

public const short Id = 482;
public override short TypeId
{
    get { return Id; }
}

public bool value;
        

public StatisticDataBoolean()
{
}

public StatisticDataBoolean(bool value)
        {
            this.value = value;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteBoolean(value);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            value = reader.ReadBoolean();
            

}


}


}