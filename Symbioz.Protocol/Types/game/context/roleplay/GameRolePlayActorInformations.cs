


















// Generated on 04/27/2016 01:13:13
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameRolePlayActorInformations : GameContextActorInformations
{

public const short Id = 141;
public override short TypeId
{
    get { return Id; }
}



public GameRolePlayActorInformations()
{
}

public GameRolePlayActorInformations(double contextualId, Types.EntityLook look, EntityDispositionInformations disposition)
         : base(contextualId, look, disposition)
        {
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            

}


}


}