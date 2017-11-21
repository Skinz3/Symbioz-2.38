


















// Generated on 04/27/2016 01:13:17
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class TaxCollectorGuildInformations : TaxCollectorComplementaryInformations
{

public const short Id = 446;
public override short TypeId
{
    get { return Id; }
}

public Types.BasicGuildInformations guild;
        

public TaxCollectorGuildInformations()
{
}

public TaxCollectorGuildInformations(Types.BasicGuildInformations guild)
        {
            this.guild = guild;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            guild.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            guild = new Types.BasicGuildInformations();
            guild.Deserialize(reader);
            

}


}


}