


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildHousesInformationMessage : Message
{

public const ushort Id = 5919;
public override ushort MessageId
{
    get { return Id; }
}

public Types.HouseInformationsForGuild[] housesInformations;
        

public GuildHousesInformationMessage()
{
}

public GuildHousesInformationMessage(Types.HouseInformationsForGuild[] housesInformations)
        {
            this.housesInformations = housesInformations;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)housesInformations.Length);
            foreach (var entry in housesInformations)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            housesInformations = new Types.HouseInformationsForGuild[limit];
            for (int i = 0; i < limit; i++)
            {
                 housesInformations[i] = new Types.HouseInformationsForGuild();
                 housesInformations[i].Deserialize(reader);
            }
            

}


}


}