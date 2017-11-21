


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ObjectEffectCreature : ObjectEffect
{

public const short Id = 71;
public override short TypeId
{
    get { return Id; }
}

public ushort monsterFamilyId;
        

public ObjectEffectCreature()
{
}

public ObjectEffectCreature(ushort actionId, ushort monsterFamilyId)
         : base(actionId)
        {
            this.monsterFamilyId = monsterFamilyId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(monsterFamilyId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            monsterFamilyId = reader.ReadVarUhShort();
            if (monsterFamilyId < 0)
                throw new Exception("Forbidden value on monsterFamilyId = " + monsterFamilyId + ", it doesn't respect the following condition : monsterFamilyId < 0");
            

}


}


}