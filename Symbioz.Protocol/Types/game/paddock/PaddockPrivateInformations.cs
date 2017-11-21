


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class PaddockPrivateInformations : PaddockAbandonnedInformations
{

public const short Id = 131;
public override short TypeId
{
    get { return Id; }
}

public Types.GuildInformations guildInfo;
        

public PaddockPrivateInformations()
{
}

public PaddockPrivateInformations(ushort maxOutdoorMount, ushort maxItems, uint price, bool locked, int guildId, Types.GuildInformations guildInfo)
         : base(maxOutdoorMount, maxItems, price, locked, guildId)
        {
            this.guildInfo = guildInfo;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            guildInfo.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            guildInfo = new Types.GuildInformations();
            guildInfo.Deserialize(reader);
            

}


}


}