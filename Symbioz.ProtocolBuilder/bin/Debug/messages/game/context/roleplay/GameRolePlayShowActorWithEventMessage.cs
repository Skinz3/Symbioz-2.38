


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayShowActorWithEventMessage : GameRolePlayShowActorMessage
{

public const ushort Id = 6407;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte actorEventId;
        

public GameRolePlayShowActorWithEventMessage()
{
}

public GameRolePlayShowActorWithEventMessage(Types.GameRolePlayActorInformations informations, sbyte actorEventId)
         : base(informations)
        {
            this.actorEventId = actorEventId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(actorEventId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            actorEventId = reader.ReadSByte();
            if (actorEventId < 0)
                throw new Exception("Forbidden value on actorEventId = " + actorEventId + ", it doesn't respect the following condition : actorEventId < 0");
            

}


}


}