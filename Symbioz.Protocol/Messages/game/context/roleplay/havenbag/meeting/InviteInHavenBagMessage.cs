


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class InviteInHavenBagMessage : Message
{

public const ushort Id = 6642;
public override ushort MessageId
{
    get { return Id; }
}

public Types.CharacterMinimalInformations guestInformations;
        public bool accept;
        

public InviteInHavenBagMessage()
{
}

public InviteInHavenBagMessage(Types.CharacterMinimalInformations guestInformations, bool accept)
        {
            this.guestInformations = guestInformations;
            this.accept = accept;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

guestInformations.Serialize(writer);
            writer.WriteBoolean(accept);
            

}

public override void Deserialize(ICustomDataInput reader)
{

guestInformations = new Types.CharacterMinimalInformations();
            guestInformations.Deserialize(reader);
            accept = reader.ReadBoolean();
            

}


}


}