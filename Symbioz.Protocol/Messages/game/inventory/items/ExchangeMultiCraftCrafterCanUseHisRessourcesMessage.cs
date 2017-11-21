


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeMultiCraftCrafterCanUseHisRessourcesMessage : Message
{

public const ushort Id = 6020;
public override ushort MessageId
{
    get { return Id; }
}

public bool allowed;
        

public ExchangeMultiCraftCrafterCanUseHisRessourcesMessage()
{
}

public ExchangeMultiCraftCrafterCanUseHisRessourcesMessage(bool allowed)
        {
            this.allowed = allowed;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(allowed);
            

}

public override void Deserialize(ICustomDataInput reader)
{

allowed = reader.ReadBoolean();
            

}


}


}