


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ContactLookRequestByNameMessage : ContactLookRequestMessage
{

public const ushort Id = 5933;
public override ushort MessageId
{
    get { return Id; }
}

public string playerName;
        

public ContactLookRequestByNameMessage()
{
}

public ContactLookRequestByNameMessage(byte requestId, sbyte contactType, string playerName)
         : base(requestId, contactType)
        {
            this.playerName = playerName;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUTF(playerName);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            playerName = reader.ReadUTF();
            

}


}


}