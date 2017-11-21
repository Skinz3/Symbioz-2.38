


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ReloginTokenStatusMessage : Message
{

public const ushort Id = 6539;
public override ushort MessageId
{
    get { return Id; }
}

public bool validToken;
        public sbyte[] ticket;
        

public ReloginTokenStatusMessage()
{
}

public ReloginTokenStatusMessage(bool validToken, sbyte[] ticket)
        {
            this.validToken = validToken;
            this.ticket = ticket;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(validToken);
            writer.WriteUShort((ushort)ticket.Length);
            foreach (var entry in ticket)
            {
                 writer.WriteSByte(entry);
            }
            

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
            

}


}


}