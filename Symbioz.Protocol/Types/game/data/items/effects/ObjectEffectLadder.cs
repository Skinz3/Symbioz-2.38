


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ObjectEffectLadder : ObjectEffectCreature
{

public const short Id = 81;
public override short TypeId
{
    get { return Id; }
}

public uint monsterCount;
        

public ObjectEffectLadder()
{
}

public ObjectEffectLadder(ushort actionId, ushort monsterFamilyId, uint monsterCount)
         : base(actionId, monsterFamilyId)
        {
            this.monsterCount = monsterCount;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(monsterCount);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            monsterCount = reader.ReadVarUhInt();
            if (monsterCount < 0)
                throw new Exception("Forbidden value on monsterCount = " + monsterCount + ", it doesn't respect the following condition : monsterCount < 0");
            

}


}


}