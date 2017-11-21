


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class HaapiApiKeyRequestMessage : Message
{

public const ushort Id = 6648;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte keyType;
        

public HaapiApiKeyRequestMessage()
{
}

public HaapiApiKeyRequestMessage(sbyte keyType)
        {
            this.keyType = keyType;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(keyType);
            

}

public override void Deserialize(ICustomDataInput reader)
{

keyType = reader.ReadSByte();
            if (keyType < 0)
                throw new Exception("Forbidden value on keyType = " + keyType + ", it doesn't respect the following condition : keyType < 0");
            

}


}


}