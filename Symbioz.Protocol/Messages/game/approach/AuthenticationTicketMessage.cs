


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AuthenticationTicketMessage : Message
{

public const ushort Id = 110;
public override ushort MessageId
{
    get { return Id; }
}

public string lang;
        public string ticket;
        

public AuthenticationTicketMessage()
{
}

public AuthenticationTicketMessage(string lang, string ticket)
        {
            this.lang = lang;
            this.ticket = ticket;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(lang);
            writer.WriteUTF(ticket);
            

}

public override void Deserialize(ICustomDataInput reader)
{

lang = reader.ReadUTF();
            ticket = reader.ReadUTF();
            

}


}


}