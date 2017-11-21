


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class KrosmasterTransferMessage : Message
{

public const ushort Id = 6348;
public override ushort MessageId
{
    get { return Id; }
}

public string uid;
        public sbyte failure;
        

public KrosmasterTransferMessage()
{
}

public KrosmasterTransferMessage(string uid, sbyte failure)
        {
            this.uid = uid;
            this.failure = failure;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(uid);
            writer.WriteSByte(failure);
            

}

public override void Deserialize(ICustomDataInput reader)
{

uid = reader.ReadUTF();
            failure = reader.ReadSByte();
            if (failure < 0)
                throw new Exception("Forbidden value on failure = " + failure + ", it doesn't respect the following condition : failure < 0");
            

}


}


}