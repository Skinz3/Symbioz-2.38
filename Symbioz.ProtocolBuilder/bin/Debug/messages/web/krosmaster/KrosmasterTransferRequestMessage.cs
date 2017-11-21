


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class KrosmasterTransferRequestMessage : Message
{

public const ushort Id = 6349;
public override ushort MessageId
{
    get { return Id; }
}

public string uid;
        

public KrosmasterTransferRequestMessage()
{
}

public KrosmasterTransferRequestMessage(string uid)
        {
            this.uid = uid;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(uid);
            

}

public override void Deserialize(ICustomDataInput reader)
{

uid = reader.ReadUTF();
            

}


}


}