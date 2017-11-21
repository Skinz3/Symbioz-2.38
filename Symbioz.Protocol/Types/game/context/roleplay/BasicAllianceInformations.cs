


















// Generated on 04/27/2016 01:13:12
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class BasicAllianceInformations : AbstractSocialGroupInfos
{

public const short Id = 419;
public override short TypeId
{
    get { return Id; }
}

public uint allianceId;
        public string allianceTag;
        

public BasicAllianceInformations()
{
}

public BasicAllianceInformations(uint allianceId, string allianceTag)
        {
            this.allianceId = allianceId;
            this.allianceTag = allianceTag;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(allianceId);
            writer.WriteUTF(allianceTag);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            allianceId = reader.ReadVarUhInt();
            if (allianceId < 0)
                throw new Exception("Forbidden value on allianceId = " + allianceId + ", it doesn't respect the following condition : allianceId < 0");
            allianceTag = reader.ReadUTF();
            

}


}


}