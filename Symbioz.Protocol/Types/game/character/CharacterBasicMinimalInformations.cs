


















// Generated on 04/27/2016 01:13:09
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class CharacterBasicMinimalInformations : AbstractCharacterInformation
{

public const short Id = 503;
public override short TypeId
{
    get { return Id; }
}

public string name;
        

public CharacterBasicMinimalInformations()
{
}

public CharacterBasicMinimalInformations(ulong id, string name)
         : base(id)
        {
            this.name = name;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUTF(name);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            name = reader.ReadUTF();
            

}


}


}