


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class WrapperObjectDissociateRequestMessage : Message
{

public const ushort Id = 6524;
public override ushort MessageId
{
    get { return Id; }
}

public uint hostUID;
        public byte hostPos;
        

public WrapperObjectDissociateRequestMessage()
{
}

public WrapperObjectDissociateRequestMessage(uint hostUID, byte hostPos)
        {
            this.hostUID = hostUID;
            this.hostPos = hostPos;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(hostUID);
            writer.WriteByte(hostPos);
            

}

public override void Deserialize(ICustomDataInput reader)
{

hostUID = reader.ReadVarUhInt();
            if (hostUID < 0)
                throw new Exception("Forbidden value on hostUID = " + hostUID + ", it doesn't respect the following condition : hostUID < 0");
            hostPos = reader.ReadByte();
            if (hostPos < 0 || hostPos > 255)
                throw new Exception("Forbidden value on hostPos = " + hostPos + ", it doesn't respect the following condition : hostPos < 0 || hostPos > 255");
            

}


}


}