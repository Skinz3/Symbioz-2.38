


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class MonsterInGroupInformations : MonsterInGroupLightInformations
{

public const short Id = 144;
public override short TypeId
{
    get { return Id; }
}

public Types.EntityLook look;
        

public MonsterInGroupInformations()
{
}

public MonsterInGroupInformations(int creatureGenericId, sbyte grade, Types.EntityLook look)
         : base(creatureGenericId, grade)
        {
            this.look = look;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            look.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            look = new Types.EntityLook();
            look.Deserialize(reader);
            

}


}


}