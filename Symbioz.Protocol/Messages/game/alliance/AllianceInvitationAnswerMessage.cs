


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceInvitationAnswerMessage : Message
{

public const ushort Id = 6401;
public override ushort MessageId
{
    get { return Id; }
}

public bool accept;
        

public AllianceInvitationAnswerMessage()
{
}

public AllianceInvitationAnswerMessage(bool accept)
        {
            this.accept = accept;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(accept);
            

}

public override void Deserialize(ICustomDataInput reader)
{

accept = reader.ReadBoolean();
            

}


}


}