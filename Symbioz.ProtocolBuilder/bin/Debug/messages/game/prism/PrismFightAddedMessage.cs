


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class PrismFightAddedMessage : Message
{

public const ushort Id = 6452;
public override ushort MessageId
{
    get { return Id; }
}

public Types.PrismFightersInformation fight;
        

public PrismFightAddedMessage()
{
}

public PrismFightAddedMessage(Types.PrismFightersInformation fight)
        {
            this.fight = fight;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

fight.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

fight = new Types.PrismFightersInformation();
            fight.Deserialize(reader);
            

}


}


}