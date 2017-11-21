


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ObjectAveragePricesMessage : Message
{

public const ushort Id = 6335;
public override ushort MessageId
{
    get { return Id; }
}

public ushort[] ids;
        public uint[] avgPrices;
        

public ObjectAveragePricesMessage()
{
}

public ObjectAveragePricesMessage(ushort[] ids, uint[] avgPrices)
        {
            this.ids = ids;
            this.avgPrices = avgPrices;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)ids.Length);
            foreach (var entry in ids)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)avgPrices.Length);
            foreach (var entry in avgPrices)
            {
                 writer.WriteVarUhInt(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            ids = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 ids[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            avgPrices = new uint[limit];
            for (int i = 0; i < limit; i++)
            {
                 avgPrices[i] = reader.ReadVarUhInt();
            }
            

}


}


}