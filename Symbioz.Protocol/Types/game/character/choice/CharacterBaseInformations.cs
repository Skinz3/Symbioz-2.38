


















// Generated on 04/27/2016 01:13:10
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class CharacterBaseInformations : CharacterMinimalPlusLookInformations
{

public const short Id = 45;
public override short TypeId
{
    get { return Id; }
}

public sbyte breed;
        public bool sex;
        

public CharacterBaseInformations()
{
}

public CharacterBaseInformations(ulong id, string name, byte level, Types.EntityLook entityLook, sbyte breed, bool sex)
         : base(id, name, level, entityLook)
        {
            this.breed = breed;
            this.sex = sex;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(breed);
            writer.WriteBoolean(sex);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            breed = reader.ReadSByte();
            sex = reader.ReadBoolean();
            

}


}


}