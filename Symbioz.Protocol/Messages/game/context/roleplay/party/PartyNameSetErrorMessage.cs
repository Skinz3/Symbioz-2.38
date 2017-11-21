


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PartyNameSetErrorMessage : AbstractPartyMessage
{

public const ushort Id = 6501;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte result;
        

public PartyNameSetErrorMessage()
{
}

public PartyNameSetErrorMessage(uint partyId, sbyte result)
         : base(partyId)
        {
            this.result = result;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(result);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            result = reader.ReadSByte();
            if (result < 0)
                throw new Exception("Forbidden value on result = " + result + ", it doesn't respect the following condition : result < 0");
            

}


}


}