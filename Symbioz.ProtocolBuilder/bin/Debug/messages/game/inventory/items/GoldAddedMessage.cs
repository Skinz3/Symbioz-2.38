


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GoldAddedMessage : Message
{

public const ushort Id = 6030;
public override ushort MessageId
{
    get { return Id; }
}

public Types.GoldItem gold;
        

public GoldAddedMessage()
{
}

public GoldAddedMessage(Types.GoldItem gold)
        {
            this.gold = gold;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

gold.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

gold = new Types.GoldItem();
            gold.Deserialize(reader);
            

}


}


}