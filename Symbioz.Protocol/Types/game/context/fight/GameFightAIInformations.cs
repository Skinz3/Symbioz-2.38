


















// Generated on 04/27/2016 01:13:11
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameFightAIInformations : GameFightFighterInformations
{

public const short Id = 151;
public override short TypeId
{
    get { return Id; }
}



public GameFightAIInformations()
{
}

public GameFightAIInformations(double contextualId, Types.EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions)
         : base(contextualId, look, disposition, teamId, wave, alive, stats, previousPositions)
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