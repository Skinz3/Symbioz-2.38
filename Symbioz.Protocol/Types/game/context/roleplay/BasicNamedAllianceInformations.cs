


















// Generated on 04/27/2016 01:13:13
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class BasicNamedAllianceInformations : BasicAllianceInformations
{

public const short Id = 418;
public override short TypeId
{
    get { return Id; }
}

public string allianceName;
        

public BasicNamedAllianceInformations()
{
}

public BasicNamedAllianceInformations(uint allianceId, string allianceTag, string allianceName)
         : base(allianceId, allianceTag)
        {
            this.allianceName = allianceName;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUTF(allianceName);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            allianceName = reader.ReadUTF();
            

}


}


}