


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AreaFightModificatorUpdateMessage : Message
{

public const ushort Id = 6493;
public override ushort MessageId
{
    get { return Id; }
}

public int spellPairId;
        

public AreaFightModificatorUpdateMessage()
{
}

public AreaFightModificatorUpdateMessage(int spellPairId)
        {
            this.spellPairId = spellPairId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(spellPairId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

spellPairId = reader.ReadInt();
            

}


}


}