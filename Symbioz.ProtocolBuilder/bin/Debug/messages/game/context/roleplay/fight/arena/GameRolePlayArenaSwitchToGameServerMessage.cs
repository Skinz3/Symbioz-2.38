


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayArenaSwitchToGameServerMessage : Message
{

public const ushort Id = 6574;
public override ushort MessageId
{
    get { return Id; }
}

public bool validToken;
        public sbyte[] ticket;
        public short homeServerId;
        

public GameRolePlayArenaSwitchToGameServerMessage()
{
}

public GameRolePlayArenaSwitchToGameServerMessage(bool validToken, sbyte[] ticket, short homeServerId)
        {
            this.validToken = validToken;
            this.ticket = ticket;
            this.homeServerId = homeServerId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(validToken);
            writer.WriteUShort((ushort)ticket.Length);
            foreach (var entry in ticket)
            {
                 writer.WriteSByte(entry);
            }
            writer.WriteShort(homeServerId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

validToken = reader.ReadBoolean();
            var limit = reader.ReadUShort();
            ticket = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 ticket[i] = reader.ReadSByte();
            }
            homeServerId = reader.ReadShort();
            

}


}


}