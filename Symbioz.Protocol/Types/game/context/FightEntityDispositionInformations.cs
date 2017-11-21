


















// Generated on 04/27/2016 01:13:10
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class FightEntityDispositionInformations : EntityDispositionInformations
{

public const short Id = 217;
public override short TypeId
{
    get { return Id; }
}

public double carryingCharacterId;
        

public FightEntityDispositionInformations()
{
}

public FightEntityDispositionInformations(short cellId, sbyte direction, double carryingCharacterId)
         : base(cellId, direction)
        {
            this.carryingCharacterId = carryingCharacterId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteDouble(carryingCharacterId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            carryingCharacterId = reader.ReadDouble();
            if (carryingCharacterId < -9007199254740990 || carryingCharacterId > 9007199254740990)
                throw new Exception("Forbidden value on carryingCharacterId = " + carryingCharacterId + ", it doesn't respect the following condition : carryingCharacterId < -9007199254740990 || carryingCharacterId > 9007199254740990");
            

}


}


}