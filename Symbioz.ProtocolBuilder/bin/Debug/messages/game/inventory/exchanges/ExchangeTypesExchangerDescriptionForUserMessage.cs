


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeTypesExchangerDescriptionForUserMessage : Message
{

public const ushort Id = 5765;
public override ushort MessageId
{
    get { return Id; }
}

public uint[] typeDescription;
        

public ExchangeTypesExchangerDescriptionForUserMessage()
{
}

public ExchangeTypesExchangerDescriptionForUserMessage(uint[] typeDescription)
        {
            this.typeDescription = typeDescription;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)typeDescription.Length);
            foreach (var entry in typeDescription)
            {
                 writer.WriteVarUhInt(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            typeDescription = new uint[limit];
            for (int i = 0; i < limit; i++)
            {
                 typeDescription[i] = reader.ReadVarUhInt();
            }
            

}


}


}