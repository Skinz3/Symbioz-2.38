


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeObjectTransfertListFromInvMessage : Message
{

public const ushort Id = 6183;
public override ushort MessageId
{
    get { return Id; }
}

public uint[] ids;
        

public ExchangeObjectTransfertListFromInvMessage()
{
}

public ExchangeObjectTransfertListFromInvMessage(uint[] ids)
        {
            this.ids = ids;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)ids.Length);
            foreach (var entry in ids)
            {
                 writer.WriteVarUhInt(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            ids = new uint[limit];
            for (int i = 0; i < limit; i++)
            {
                 ids[i] = reader.ReadVarUhInt();
            }
            

}


}


}