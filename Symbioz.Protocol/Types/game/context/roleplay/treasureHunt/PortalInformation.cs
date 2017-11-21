


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class PortalInformation
{

public const short Id = 466;
public virtual short TypeId
{
    get { return Id; }
}

public int portalId;
        public short areaId;
        

public PortalInformation()
{
}

public PortalInformation(int portalId, short areaId)
        {
            this.portalId = portalId;
            this.areaId = areaId;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(portalId);
            writer.WriteShort(areaId);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

portalId = reader.ReadInt();
            areaId = reader.ReadShort();
            

}


}


}