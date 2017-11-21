


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class HumanOptionAlliance : HumanOption
{

public const short Id = 425;
public override short TypeId
{
    get { return Id; }
}

public AllianceInformations allianceInformations;
        public sbyte aggressable;
        

public HumanOptionAlliance()
{
}

public HumanOptionAlliance(AllianceInformations allianceInformations, sbyte aggressable)
        {
            this.allianceInformations = allianceInformations;
            this.aggressable = aggressable;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            allianceInformations.Serialize(writer);
            writer.WriteSByte(aggressable);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            allianceInformations = new AllianceInformations();
            allianceInformations.Deserialize(reader);
            aggressable = reader.ReadSByte();
            if (aggressable < 0)
                throw new Exception("Forbidden value on aggressable = " + aggressable + ", it doesn't respect the following condition : aggressable < 0");
            

}


}


}