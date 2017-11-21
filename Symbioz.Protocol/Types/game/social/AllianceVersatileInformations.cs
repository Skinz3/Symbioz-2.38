


















// Generated on 04/27/2016 01:13:19
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class AllianceVersatileInformations
{

public const short Id = 432;
public virtual short TypeId
{
    get { return Id; }
}

public uint allianceId;
        public ushort nbGuilds;
        public ushort nbMembers;
        public ushort nbSubarea;
        

public AllianceVersatileInformations()
{
}

public AllianceVersatileInformations(uint allianceId, ushort nbGuilds, ushort nbMembers, ushort nbSubarea)
        {
            this.allianceId = allianceId;
            this.nbGuilds = nbGuilds;
            this.nbMembers = nbMembers;
            this.nbSubarea = nbSubarea;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(allianceId);
            writer.WriteVarUhShort(nbGuilds);
            writer.WriteVarUhShort(nbMembers);
            writer.WriteVarUhShort(nbSubarea);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

allianceId = reader.ReadVarUhInt();
            if (allianceId < 0)
                throw new Exception("Forbidden value on allianceId = " + allianceId + ", it doesn't respect the following condition : allianceId < 0");
            nbGuilds = reader.ReadVarUhShort();
            if (nbGuilds < 0)
                throw new Exception("Forbidden value on nbGuilds = " + nbGuilds + ", it doesn't respect the following condition : nbGuilds < 0");
            nbMembers = reader.ReadVarUhShort();
            if (nbMembers < 0)
                throw new Exception("Forbidden value on nbMembers = " + nbMembers + ", it doesn't respect the following condition : nbMembers < 0");
            nbSubarea = reader.ReadVarUhShort();
            if (nbSubarea < 0)
                throw new Exception("Forbidden value on nbSubarea = " + nbSubarea + ", it doesn't respect the following condition : nbSubarea < 0");
            

}


}


}