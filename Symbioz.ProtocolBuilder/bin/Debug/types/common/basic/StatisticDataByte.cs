


















// Generated on 12/20/2016 16:23:28
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class StatisticDataByte : StatisticData
{

public const short Id = 486;
public override short TypeId
{
    get { return Id; }
}

public sbyte value;
        

public StatisticDataByte()
{
}

public StatisticDataByte(sbyte value)
        {
            this.value = value;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(value);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            value = reader.ReadSByte();
            

}


}


}