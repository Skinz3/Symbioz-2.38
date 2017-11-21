


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildInvitationStateRecrutedMessage : Message
{

public const ushort Id = 5548;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte invitationState;
        

public GuildInvitationStateRecrutedMessage()
{
}

public GuildInvitationStateRecrutedMessage(sbyte invitationState)
        {
            this.invitationState = invitationState;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(invitationState);
            

}

public override void Deserialize(ICustomDataInput reader)
{

invitationState = reader.ReadSByte();
            if (invitationState < 0)
                throw new Exception("Forbidden value on invitationState = " + invitationState + ", it doesn't respect the following condition : invitationState < 0");
            

}


}


}