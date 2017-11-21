


















// Generated on 04/27/2016 01:13:08
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class AchievementStartedObjective : AchievementObjective
{

public const short Id = 402;
public override short TypeId
{
    get { return Id; }
}

public ushort value;
        

public AchievementStartedObjective()
{
}

public AchievementStartedObjective(uint id, ushort maxValue, ushort value)
         : base(id, maxValue)
        {
            this.value = value;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(value);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            value = reader.ReadVarUhShort();
            if (value < 0)
                throw new Exception("Forbidden value on value = " + value + ", it doesn't respect the following condition : value < 0");
            

}


}


}