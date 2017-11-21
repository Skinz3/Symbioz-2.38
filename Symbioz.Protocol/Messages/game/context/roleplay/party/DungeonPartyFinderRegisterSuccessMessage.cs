


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DungeonPartyFinderRegisterSuccessMessage : Message
{

public const ushort Id = 6241;
public override ushort MessageId
{
    get { return Id; }
}

public ushort[] dungeonIds;
        

public DungeonPartyFinderRegisterSuccessMessage()
{
}

public DungeonPartyFinderRegisterSuccessMessage(ushort[] dungeonIds)
        {
            this.dungeonIds = dungeonIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)dungeonIds.Length);
            foreach (var entry in dungeonIds)
            {
                 writer.WriteVarUhShort(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            dungeonIds = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 dungeonIds[i] = reader.ReadVarUhShort();
            }
            

}


}


}