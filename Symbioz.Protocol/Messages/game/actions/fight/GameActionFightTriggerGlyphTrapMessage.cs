


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightTriggerGlyphTrapMessage : AbstractGameActionMessage
{

public const ushort Id = 5741;
public override ushort MessageId
{
    get { return Id; }
}

public short markId;
        public double triggeringCharacterId;
        public ushort triggeredSpellId;
        

public GameActionFightTriggerGlyphTrapMessage()
{
}

public GameActionFightTriggerGlyphTrapMessage(ushort actionId, double sourceId, short markId, double triggeringCharacterId, ushort triggeredSpellId)
         : base(actionId, sourceId)
        {
            this.markId = markId;
            this.triggeringCharacterId = triggeringCharacterId;
            this.triggeredSpellId = triggeredSpellId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteShort(markId);
            writer.WriteDouble(triggeringCharacterId);
            writer.WriteVarUhShort(triggeredSpellId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            markId = reader.ReadShort();
            triggeringCharacterId = reader.ReadDouble();
            if (triggeringCharacterId < -9007199254740990 || triggeringCharacterId > 9007199254740990)
                throw new Exception("Forbidden value on triggeringCharacterId = " + triggeringCharacterId + ", it doesn't respect the following condition : triggeringCharacterId < -9007199254740990 || triggeringCharacterId > 9007199254740990");
            triggeredSpellId = reader.ReadVarUhShort();
            if (triggeredSpellId < 0)
                throw new Exception("Forbidden value on triggeredSpellId = " + triggeredSpellId + ", it doesn't respect the following condition : triggeredSpellId < 0");
            

}


}


}