


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyNameUpdateMessage : AbstractPartyMessage
{

public const ushort Id = 6502;
public override ushort MessageId
{
    get { return Id; }
}

public string partyName;
        

public PartyNameUpdateMessage()
{
}

public PartyNameUpdateMessage(uint partyId, string partyName)
         : base(partyId)
        {
            this.partyName = partyName;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUTF(partyName);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            partyName = reader.ReadUTF();
            

}


}


}