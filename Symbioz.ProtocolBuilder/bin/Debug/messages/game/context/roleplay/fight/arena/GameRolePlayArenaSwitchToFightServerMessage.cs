


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayArenaSwitchToFightServerMessage : Message
{

public const ushort Id = 6575;
public override ushort MessageId
{
    get { return Id; }
}

public string address;
        public ushort port;
        public sbyte[] ticket;
        

public GameRolePlayArenaSwitchToFightServerMessage()
{
}

public GameRolePlayArenaSwitchToFightServerMessage(string address, ushort port, sbyte[] ticket)
        {
            this.address = address;
            this.port = port;
            this.ticket = ticket;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(address);
            writer.WriteUShort(port);
            writer.WriteUShort((ushort)ticket.Length);
            foreach (var entry in ticket)
            {
                 writer.WriteSByte(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

address = reader.ReadUTF();
            port = reader.ReadUShort();
            if (port < 0 || port > 65535)
                throw new Exception("Forbidden value on port = " + port + ", it doesn't respect the following condition : port < 0 || port > 65535");
            var limit = reader.ReadUShort();
            ticket = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 ticket[i] = reader.ReadSByte();
            }
            

}


}


}