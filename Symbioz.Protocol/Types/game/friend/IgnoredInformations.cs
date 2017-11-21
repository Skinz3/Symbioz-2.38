


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class IgnoredInformations : AbstractContactInformations
{

public const short Id = 106;
public override short TypeId
{
    get { return Id; }
}



public IgnoredInformations()
{
}

public IgnoredInformations(int accountId, string accountName)
         : base(accountId, accountName)
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