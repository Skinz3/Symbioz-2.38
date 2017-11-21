


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class HumanOptionGuild : HumanOption
{

public const short Id = 409;
public override short TypeId
{
    get { return Id; }
}

public GuildInformations guildInformations;
        

public HumanOptionGuild()
{
}

public HumanOptionGuild(GuildInformations guildInformations)
        {
            this.guildInformations = guildInformations;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            guildInformations.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            guildInformations = new GuildInformations();
            guildInformations.Deserialize(reader);
            

}


}


}