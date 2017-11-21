


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class SymbioticObjectAssociateRequestMessage : Message
{

public const ushort Id = 6522;
public override ushort MessageId
{
    get { return Id; }
}

public uint symbioteUID;
        public byte symbiotePos;
        public uint hostUID;
        public byte hostPos;
        

public SymbioticObjectAssociateRequestMessage()
{
}

public SymbioticObjectAssociateRequestMessage(uint symbioteUID, byte symbiotePos, uint hostUID, byte hostPos)
        {
            this.symbioteUID = symbioteUID;
            this.symbiotePos = symbiotePos;
            this.hostUID = hostUID;
            this.hostPos = hostPos;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(symbioteUID);
            writer.WriteByte(symbiotePos);
            writer.WriteVarUhInt(hostUID);
            writer.WriteByte(hostPos);
            

}

public override void Deserialize(ICustomDataInput reader)
{

symbioteUID = reader.ReadVarUhInt();
            if (symbioteUID < 0)
                throw new Exception("Forbidden value on symbioteUID = " + symbioteUID + ", it doesn't respect the following condition : symbioteUID < 0");
            symbiotePos = reader.ReadByte();
            if (symbiotePos < 0 || symbiotePos > 255)
                throw new Exception("Forbidden value on symbiotePos = " + symbiotePos + ", it doesn't respect the following condition : symbiotePos < 0 || symbiotePos > 255");
            hostUID = reader.ReadVarUhInt();
            if (hostUID < 0)
                throw new Exception("Forbidden value on hostUID = " + hostUID + ", it doesn't respect the following condition : hostUID < 0");
            hostPos = reader.ReadByte();
            if (hostPos < 0 || hostPos > 255)
                throw new Exception("Forbidden value on hostPos = " + hostPos + ", it doesn't respect the following condition : hostPos < 0 || hostPos > 255");
            

}


}


}