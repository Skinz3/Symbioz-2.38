


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class TreasureHuntStepDig : TreasureHuntStep
{

public const short Id = 465;
public override short TypeId
{
    get { return Id; }
}



public TreasureHuntStepDig()
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