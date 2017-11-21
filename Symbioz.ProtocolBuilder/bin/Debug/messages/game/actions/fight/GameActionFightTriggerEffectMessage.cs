


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightTriggerEffectMessage : GameActionFightDispellEffectMessage
{

public const ushort Id = 6147;
public override ushort MessageId
{
    get { return Id; }
}



public GameActionFightTriggerEffectMessage()
{
}

public GameActionFightTriggerEffectMessage(ushort actionId, double sourceId, double targetId, int boostUID)
         : base(actionId, sourceId, targetId, boostUID)
        {
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            

}


}


}