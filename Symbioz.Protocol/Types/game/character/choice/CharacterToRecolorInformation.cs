


















// Generated on 04/27/2016 01:13:10
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class CharacterToRecolorInformation : AbstractCharacterToRefurbishInformation
{

public const short Id = 212;
public override short TypeId
{
    get { return Id; }
}



public CharacterToRecolorInformation()
{
}

public CharacterToRecolorInformation(ulong id, int[] colors, uint cosmeticId)
         : base(id, colors, cosmeticId)
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