


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class IdolSelectRequestMessage : Message
{

public const ushort Id = 6587;
public override ushort MessageId
{
    get { return Id; }
}

public bool activate;
        public bool party;
        public ushort idolId;
        

public IdolSelectRequestMessage()
{
}

public IdolSelectRequestMessage(bool activate, bool party, ushort idolId)
        {
            this.activate = activate;
            this.party = party;
            this.idolId = idolId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, activate);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, party);
            writer.WriteByte(flag1);
            writer.WriteVarUhShort(idolId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

byte flag1 = reader.ReadByte();
            activate = BooleanByteWrapper.GetFlag(flag1, 0);
            party = BooleanByteWrapper.GetFlag(flag1, 1);
            idolId = reader.ReadVarUhShort();
            if (idolId < 0)
                throw new Exception("Forbidden value on idolId = " + idolId + ", it doesn't respect the following condition : idolId < 0");
            

}


}


}