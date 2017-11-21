


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameActionFightActivateGlyphTrapMessage : AbstractGameActionMessage
{

public const ushort Id = 6545;
public override ushort MessageId
{
    get { return Id; }
}

public short markId;
        public bool active;
        

public GameActionFightActivateGlyphTrapMessage()
{
}

public GameActionFightActivateGlyphTrapMessage(ushort actionId, double sourceId, short markId, bool active)
         : base(actionId, sourceId)
        {
            this.markId = markId;
            this.active = active;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteShort(markId);
            writer.WriteBoolean(active);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            markId = reader.ReadShort();
            active = reader.ReadBoolean();
            

}


}


}