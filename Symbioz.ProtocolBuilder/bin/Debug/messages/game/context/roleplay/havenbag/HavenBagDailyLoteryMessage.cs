


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class HavenBagDailyLoteryMessage : Message
{

public const ushort Id = 6644;
public override ushort MessageId
{
    get { return Id; }
}

public string tokenId;
        

public HavenBagDailyLoteryMessage()
{
}

public HavenBagDailyLoteryMessage(string tokenId)
        {
            this.tokenId = tokenId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(tokenId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

tokenId = reader.ReadUTF();
            

}


}


}