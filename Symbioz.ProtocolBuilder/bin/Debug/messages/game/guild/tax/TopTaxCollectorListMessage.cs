


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class TopTaxCollectorListMessage : AbstractTaxCollectorListMessage
{

public const ushort Id = 6565;
public override ushort MessageId
{
    get { return Id; }
}

public bool isDungeon;
        

public TopTaxCollectorListMessage()
{
}

public TopTaxCollectorListMessage(Types.TaxCollectorInformations[] informations, bool isDungeon)
         : base(informations)
        {
            this.isDungeon = isDungeon;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteBoolean(isDungeon);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            isDungeon = reader.ReadBoolean();
            

}


}


}