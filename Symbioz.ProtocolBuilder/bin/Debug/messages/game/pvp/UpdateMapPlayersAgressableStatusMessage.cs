


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class UpdateMapPlayersAgressableStatusMessage : Message
{

public const ushort Id = 6454;
public override ushort MessageId
{
    get { return Id; }
}

public ulong[] playerIds;
        public sbyte[] enable;
        

public UpdateMapPlayersAgressableStatusMessage()
{
}

public UpdateMapPlayersAgressableStatusMessage(ulong[] playerIds, sbyte[] enable)
        {
            this.playerIds = playerIds;
            this.enable = enable;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)playerIds.Length);
            foreach (var entry in playerIds)
            {
                 writer.WriteVarUhLong(entry);
            }
            writer.WriteUShort((ushort)enable.Length);
            foreach (var entry in enable)
            {
                 writer.WriteSByte(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            playerIds = new ulong[limit];
            for (int i = 0; i < limit; i++)
            {
                 playerIds[i] = reader.ReadVarUhLong();
            }
            limit = reader.ReadUShort();
            enable = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 enable[i] = reader.ReadSByte();
            }
            

}


}


}