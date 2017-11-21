


















// Generated on 04/27/2016 01:13:10
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class IdentifiedEntityDispositionInformations : EntityDispositionInformations
{

public const short Id = 107;
public override short TypeId
{
    get { return Id; }
}

public double id;
        

public IdentifiedEntityDispositionInformations()
{
}

public IdentifiedEntityDispositionInformations(short cellId, sbyte direction, double id)
         : base(cellId, direction)
        {
            this.id = id;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteDouble(id);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            id = reader.ReadDouble();
            if (id < -9007199254740990 || id > 9007199254740990)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < -9007199254740990 || id > 9007199254740990");
            

}


}


}