


















// Generated on 04/27/2016 01:13:12
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameFightFighterNamedLightInformations : GameFightFighterLightInformations
{

public const short Id = 456;
public override short TypeId
{
    get { return Id; }
}

public string name;
        

public GameFightFighterNamedLightInformations()
{
}

public GameFightFighterNamedLightInformations(double id, sbyte wave, ushort level, sbyte breed, string name)
         : base(id, wave, level, breed)
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