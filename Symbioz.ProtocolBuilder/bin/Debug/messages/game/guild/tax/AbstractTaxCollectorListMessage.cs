


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AbstractTaxCollectorListMessage : Message
{

public const ushort Id = 6568;
public override ushort MessageId
{
    get { return Id; }
}

public Types.TaxCollectorInformations[] informations;
        

public AbstractTaxCollectorListMessage()
{
}

public AbstractTaxCollectorListMessage(Types.TaxCollectorInformations[] informations)
        {
            this.informations = informations;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)informations.Length);
            foreach (var entry in informations)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            informations = new Types.TaxCollectorInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 informations[i] = ProtocolTypeManager.GetInstance<Types.TaxCollectorInformations>(reader.ReadShort());
                 informations[i].Deserialize(reader);
            }
            

}


}


}