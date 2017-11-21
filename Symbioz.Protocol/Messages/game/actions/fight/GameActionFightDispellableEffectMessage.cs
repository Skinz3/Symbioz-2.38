


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightDispellableEffectMessage : AbstractGameActionMessage
{

public const ushort Id = 6070;
public override ushort MessageId
{
    get { return Id; }
}

public Types.AbstractFightDispellableEffect effect;
        

public GameActionFightDispellableEffectMessage()
{
}

public GameActionFightDispellableEffectMessage(ushort actionId, double sourceId, Types.AbstractFightDispellableEffect effect)
         : base(actionId, sourceId)
        {
            this.effect = effect;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteShort(effect.TypeId);
            effect.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            effect = Types.ProtocolTypeManager.GetInstance<Types.AbstractFightDispellableEffect>(reader.ReadShort());
            effect.Deserialize(reader);
            

}


}


}