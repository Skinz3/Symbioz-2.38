


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class TreasureHuntShowLegendaryUIMessage : Message
{

public const ushort Id = 6498;
public override ushort MessageId
{
    get { return Id; }
}

public ushort[] availableLegendaryIds;
        

public TreasureHuntShowLegendaryUIMessage()
{
}

public TreasureHuntShowLegendaryUIMessage(ushort[] availableLegendaryIds)
        {
            this.availableLegendaryIds = availableLegendaryIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)availableLegendaryIds.Length);
            foreach (var entry in availableLegendaryIds)
            {
                 writer.WriteVarUhShort(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            availableLegendaryIds = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 availableLegendaryIds[i] = reader.ReadVarUhShort();
            }
            

}


}


}