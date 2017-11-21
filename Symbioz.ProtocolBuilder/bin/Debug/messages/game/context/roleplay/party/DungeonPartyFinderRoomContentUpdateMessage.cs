


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DungeonPartyFinderRoomContentUpdateMessage : Message
{

public const ushort Id = 6250;
public override ushort MessageId
{
    get { return Id; }
}

public ushort dungeonId;
        public Types.DungeonPartyFinderPlayer[] addedPlayers;
        public ulong[] removedPlayersIds;
        

public DungeonPartyFinderRoomContentUpdateMessage()
{
}

public DungeonPartyFinderRoomContentUpdateMessage(ushort dungeonId, Types.DungeonPartyFinderPlayer[] addedPlayers, ulong[] removedPlayersIds)
        {
            this.dungeonId = dungeonId;
            this.addedPlayers = addedPlayers;
            this.removedPlayersIds = removedPlayersIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(dungeonId);
            writer.WriteUShort((ushort)addedPlayers.Length);
            foreach (var entry in addedPlayers)
            {
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)removedPlayersIds.Length);
            foreach (var entry in removedPlayersIds)
            {
                 writer.WriteVarUhLong(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

dungeonId = reader.ReadVarUhShort();
            if (dungeonId < 0)
                throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
            var limit = reader.ReadUShort();
            addedPlayers = new Types.DungeonPartyFinderPlayer[limit];
            for (int i = 0; i < limit; i++)
            {
                 addedPlayers[i] = new Types.DungeonPartyFinderPlayer();
                 addedPlayers[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            removedPlayersIds = new ulong[limit];
            for (int i = 0; i < limit; i++)
            {
                 removedPlayersIds[i] = reader.ReadVarUhLong();
            }
            

}


}


}